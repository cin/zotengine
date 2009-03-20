using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using gl = Tao.OpenGl.Gl;
using glu = Tao.OpenGl.Glu;
using gdi = Tao.Platform.Windows.Gdi;
using user = Tao.Platform.Windows.User;
using wgl = Tao.Platform.Windows.Wgl;
using ode = Tao.Ode.Ode;

namespace Zot.Core
{
   public class GLWindow : Form
   {
      private IntPtr hdc;
      private IntPtr hrc;

      private bool done;
      private bool fullscreen;
      private bool[] keys;

      private Timer physTimer;

      Camera camera = new Camera();
      Point center;
      Point mouse;
      Point last;
      bool warping = false;

      int DIM = 50;
      int SCALE = 10;

      float[] lamb = new float[4] { 0f, 0f, 0f, 1f };
      float[] ldif = new float[4] { 1f, 1f, 1f, 1f };
      float[] lpos = new float[4] { 0f, 10f, 0f, 1f };
      float[] lspec = new float[4] { 1f, 1f, 1f, 1f };

      public GLWindow()
      {
         // these sections are lifted from the Tao Nehe examples
         this.CreateParams.ClassStyle = this.CreateParams.ClassStyle |       // Redraw On Size, And Own DC For Window.
             user.CS_HREDRAW | user.CS_VREDRAW | user.CS_OWNDC;
         this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);            // No Need To Erase Form Background
         this.SetStyle(ControlStyles.DoubleBuffer, true);                    // Buffer Control
         this.SetStyle(ControlStyles.Opaque, true);                          // No Need To Draw Form Background
         this.SetStyle(ControlStyles.ResizeRedraw, true);                    // Redraw On Resize
         this.SetStyle(ControlStyles.UserPaint, true);                       // We'll Handle Painting Ourselves

         this.Closing += new CancelEventHandler(this.GLWindow_Closing);          // On Closing Event Call Form_Closing
         this.KeyDown += new KeyEventHandler(this.GLWindow_KeyDown);             // On KeyDown Event Call Form_KeyDown
         this.KeyUp += new KeyEventHandler(this.GLWindow_KeyUp);                 // On KeyUp Event Call Form_KeyUp
         this.Resize += new EventHandler(this.GLWindow_Resize);                  // On Resize Event Call Form_Resize
         this.MouseMove += new MouseEventHandler(this.GLWindow_MouseMove);
         this.MouseDown += new MouseEventHandler(GLWindow_MouseDown);

         physTimer = new Timer();
         physTimer.Interval = 10;
         physTimer.Tick += new EventHandler(physTimer_Tick);
         physTimer.Start();

         done = false;
         fullscreen = false;
         keys = new bool[256];
         hdc = hrc = IntPtr.Zero;
      }

      #region Properties
      public bool Done { get { return done; } }

      public bool FSToggle
      {
         get
         {
            if (keys[(int)Keys.F1])
            {
               keys[(int)Keys.F1] = false;
               return true;
            }
            else
               return false;
         }
      }
      #endregion

      #region window management
      public bool Create(int width, int height, int bits, string title, bool full)
      {
         fullscreen = full;
         if (fullscreen)
         {
            gdi.DEVMODE dm = new gdi.DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(dm);
            dm.dmPelsWidth = width;
            dm.dmPelsHeight = height;
            dm.dmBitsPerPel = bits;
            dm.dmFields = gdi.DM_BITSPERPEL | gdi.DM_PELSWIDTH | gdi.DM_PELSHEIGHT;
            if (user.ChangeDisplaySettings(ref dm, user.CDS_FULLSCREEN) != user.DISP_CHANGE_SUCCESSFUL)
            {
               MessageBox.Show("No fullscreen support!");
               return false;
            }
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
         }
         else
         {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(
               (Screen.PrimaryScreen.Bounds.Width / 2) - (width / 2),
               (Screen.PrimaryScreen.Bounds.Height / 2) - (height / 2));
         }

         Width = width;
         Height = height;
         Text = title;

         gdi.PIXELFORMATDESCRIPTOR pfd = new gdi.PIXELFORMATDESCRIPTOR();
         pfd.nSize = (short)Marshal.SizeOf(pfd);
         pfd.nVersion = 1;
         pfd.dwFlags = gdi.PFD_DRAW_TO_WINDOW | gdi.PFD_SUPPORT_OPENGL | gdi.PFD_DOUBLEBUFFER;
         pfd.iPixelType = (byte)gdi.PFD_TYPE_RGBA;
         pfd.cColorBits = (byte)bits;
         pfd.cRedBits = pfd.cRedShift = pfd.cGreenBits = pfd.cGreenShift = pfd.cBlueBits = pfd.cBlueShift = pfd.cAlphaBits = pfd.cAlphaShift =
         pfd.cAccumBits = pfd.cAccumRedBits = pfd.cAccumGreenBits = pfd.cAccumBlueBits = pfd.cAccumAlphaBits = 0;
         pfd.cDepthBits = 16;
         pfd.cStencilBits = 0;
         pfd.iLayerType = (byte)gdi.PFD_MAIN_PLANE;
         pfd.bReserved = 0;
         pfd.dwLayerMask = pfd.dwVisibleMask = pfd.dwDamageMask = 0;

         hdc = user.GetDC(Handle);
         if (hdc == IntPtr.Zero)
         {
            Destroy();
            MessageBox.Show("Can't create device context", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }

         int pfmt = gdi.ChoosePixelFormat(hdc, ref pfd);
         if (pfmt == 0)
         {
            Destroy();
            MessageBox.Show("Can't choose a pixel format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         if (!gdi.SetPixelFormat(hdc, pfmt, ref pfd))
         {
            Destroy();
            MessageBox.Show("Can't set the pixel format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         hrc = wgl.wglCreateContext(hdc);
         if (hrc == IntPtr.Zero)
         {
            Destroy();
            MessageBox.Show("Can't create a rendering context", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }
         if (!wgl.wglMakeCurrent(hdc, hrc))
         {
            Destroy();
            MessageBox.Show("Can't activate rendering context", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }

         Show();
         TopMost = true;
         Focus();

         ResizeWindow(width, height);

         if (!InitGL())
         {
            Destroy();
            MessageBox.Show("Initialization failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
         }

         center = new Point(Location.X + Width / 2, Location.Y + Height / 2);
         mouse = new Point(center.X, center.Y);
         last = new Point(center.X, center.Y);
         Cursor.Position = new Point(center.X, center.Y);

         return true;
      }

      public void Destroy()
      {
         if (fullscreen)
         {
            user.ChangeDisplaySettings(IntPtr.Zero, 0);
         }

         if (hrc != IntPtr.Zero)
         {
            wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            wgl.wglDeleteContext(hrc);
            hrc = IntPtr.Zero;
         }

         if (hdc != IntPtr.Zero && !IsDisposed)
         {
            user.ReleaseDC(Handle, hdc);
            hdc = IntPtr.Zero;
         }
      }

      public void ResizeWindow(int w, int h)
      {
         gl.glViewport(0, 0, w, h);
         gl.glMatrixMode(gl.GL_PROJECTION);
         gl.glLoadIdentity();
         glu.gluPerspective(45.0, (double)w / (double)h, 1.0, 1000.0);
         gl.glMatrixMode(gl.GL_MODELVIEW);
      }
      #endregion

      #region events
      private void GLWindow_Closing(object sender, EventArgs e)
      {
         done = true;
      }

      private void GLWindow_KeyDown(object sender, KeyEventArgs e)
      {
         keys[e.KeyValue] = true;
      }

      private void GLWindow_KeyUp(object sender, KeyEventArgs e)
      {
         keys[e.KeyValue] = false;
      }

      private void GLWindow_MouseMove(object sender, MouseEventArgs e)
      {
         mouse.X = center.X - (Width / 2) + e.X;
         mouse.Y = center.Y - (Height / 2) + e.Y;
         if (warping)
         {
            warping = false;
            return;
         }

         camera.Yaw += (mouse.X - center.X) * 0.1;
         camera.Pitch += (mouse.Y - center.Y) * 0.1;

         warping = true;
         Cursor.Position = new Point(center.X, center.Y);
      }

      private void GLWindow_Resize(object sender, EventArgs e)
      {
         ResizeWindow(Width, Height);
      }

      private void GLWindow_MouseDown(object sender, MouseEventArgs e)
      {
         if (e.Button == MouseButtons.Left)
         {
            Pulse();
         }
      }

      private void physTimer_Tick(object sender, EventArgs e)
      {
         float dt = physTimer.Interval / 1000f * 2f;
         PhysicsSystem.Update(dt);
      }
      #endregion

      private void DrawCube(Vector3 pos, double size, Color color)
      {
         gl.glPolygonMode(gl.GL_FRONT_AND_BACK, gl.GL_FILL);
         gl.glBegin(gl.GL_QUADS);
         gl.glColor3ub(color.R, color.G, color.B);
         gl.glNormal3f(1f, 0f, 0f);
         gl.glVertex3d(pos.X + size, pos.Y + size, pos.Z + size);
         gl.glVertex3d(pos.X + size, pos.Y - size, pos.Z + size);
         gl.glVertex3d(pos.X + size, pos.Y - size, pos.Z - size);
         gl.glVertex3d(pos.X + size, pos.Y + size, pos.Z - size);

         gl.glNormal3f(0f, 0f, -1f);
         gl.glVertex3d(pos.X + size, pos.Y + size, pos.Z - size);
         gl.glVertex3d(pos.X + size, pos.Y - size, pos.Z - size);
         gl.glVertex3d(pos.X - size, pos.Y - size, pos.Z - size);
         gl.glVertex3d(pos.X - size, pos.Y + size, pos.Z - size);

         gl.glNormal3f(-1f, 0f, 0f);
         gl.glVertex3d(pos.X - size, pos.Y + size, pos.Z - size);
         gl.glVertex3d(pos.X - size, pos.Y - size, pos.Z - size);
         gl.glVertex3d(pos.X - size, pos.Y - size, pos.Z + size);
         gl.glVertex3d(pos.X - size, pos.Y + size, pos.Z + size);

         gl.glNormal3f(0f, 0f, 1f);
         gl.glVertex3d(pos.X - size, pos.Y + size, pos.Z + size);
         gl.glVertex3d(pos.X - size, pos.Y - size, pos.Z + size);
         gl.glVertex3d(pos.X + size, pos.Y - size, pos.Z + size);
         gl.glVertex3d(pos.X + size, pos.Y + size, pos.Z + size);

         gl.glNormal3f(0f, 1f, 0f);
         gl.glVertex3d(pos.X + size, pos.Y + size, pos.Z + size);
         gl.glVertex3d(pos.X + size, pos.Y + size, pos.Z - size);
         gl.glVertex3d(pos.X - size, pos.Y + size, pos.Z - size);
         gl.glVertex3d(pos.X - size, pos.Y + size, pos.Z + size);

         gl.glNormal3f(0f, -1f, 0f);
         gl.glVertex3d(pos.X + size, pos.Y - size, pos.Z + size);
         gl.glVertex3d(pos.X - size, pos.Y - size, pos.Z + size);
         gl.glVertex3d(pos.X - size, pos.Y - size, pos.Z - size);
         gl.glVertex3d(pos.X + size, pos.Y - size, pos.Z - size);
         gl.glEnd();
      }

      private void DrawGrid()
      {
         gl.glPolygonMode(gl.GL_FRONT_AND_BACK, gl.GL_FILL);
         gl.glColor3f(0.7f, 0.7f, 0.7f);
         for (int x = -DIM; x < DIM; x++)
         {
            gl.glNormal3f(0f, 1f, 0f);
            gl.glBegin(gl.GL_TRIANGLE_STRIP);
            for (int z = -DIM; z < DIM; z++)
            {
               gl.glVertex3d(x * SCALE, 0, -z * SCALE);
               gl.glVertex3d((x + 1) * SCALE, 0, -z * SCALE);
            }
            gl.glEnd();
         }
      }

      public void DrawScene()
      {
         gl.glClear(gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);
         gl.glLoadIdentity();
         camera.Setup();
         DrawGrid();
         DrawCube(new Vector3(0, 1, -20), 1, Color.Red);
         DrawCube(new Vector3(20, 4, 0), 4, Color.Blue);
         RenderSystem.Render();
         gdi.SwapBuffers(hdc);
      }

      private bool InitGL()
      {
         gl.glClearColor(0f, 0f, 0f, 0f);
         gl.glClearDepth(1.0);
         gl.glEnable(gl.GL_DEPTH_TEST);
         gl.glEnable(gl.GL_LIGHTING);
         gl.glHint(gl.GL_PERSPECTIVE_CORRECTION_HINT, gl.GL_NICEST);

         gl.glLightfv(gl.GL_LIGHT0, gl.GL_AMBIENT, lamb);
         gl.glLightfv(gl.GL_LIGHT0, gl.GL_DIFFUSE, ldif);
         gl.glLightfv(gl.GL_LIGHT0, gl.GL_SPECULAR, lspec);
         gl.glLightfv(gl.GL_LIGHT0, gl.GL_POSITION, lpos);
         gl.glEnable(gl.GL_LIGHT0);
         gl.glEnable(gl.GL_COLOR_MATERIAL);

         PhysicsSystem.Init();
         RenderSystem.Init();

         InitSpheres();

         camera.PlaceAt(0, 10, 30);

         return true;
      }

      private void InitSpheres()
      {
         PhysicsSystem.AddPhysicable(0, new ode.dVector3(0, 1, 0), new ode.dVector3(0, 0, 0), 1, 1);
         RenderSystem.AddRenderable(0, Color.Green);
      }

      public void ParseInput()
      {
         if (keys[(int)Keys.Escape])
         {
            done = true;
         }
         if (keys[(int)Keys.W]) camera.MovingForward = true; else camera.MovingForward = false;
         if (keys[(int)Keys.S]) camera.MovingBackward = true; else camera.MovingBackward = false;
         if (keys[(int)Keys.A]) camera.MovingLeft = true; else camera.MovingLeft = false;
         if (keys[(int)Keys.D]) camera.MovingRight = true; else camera.MovingRight = false;
         if (keys[(int)Keys.Space]) camera.MovingUp = true; else camera.MovingUp = false;
         if (keys[(int)Keys.X]) camera.MovingDown = true; else camera.MovingDown = false;
         if (keys[(int)Keys.R]) camera.Reset();
      }

      private void Pulse()
      {
         PhysicsSystem.NudgePhysicable(0, new ode.dVector3(0, 500, 0));
      }
   }
}
