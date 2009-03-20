using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using gl = Tao.OpenGl.Gl;
using glu = Tao.OpenGl.Glu;
using ode = Tao.Ode.Ode;

using Zot.Core;

namespace Zot.Main
{
   /// <summary>
   /// MainForm
   /// </summary>
   public partial class MainForm : Form
   {
      bool fullscreen = true;

      Camera camera;

      Point lastM;
      Point diffM = new Point(0, 0);
      bool wrapping = false;
      Point wrapTo = new Point(0, 0);
      int WRAP_MARGIN = 5;

      #region impl
      /// <summary>
      /// MainForm constructor
      /// </summary>
      public MainForm()
      {
         InitializeComponent();
         glControl.InitializeContexts();
         Cursor.Hide();
         Init();
      }

      private void Init()
      {
         gl.glClearColor(0f, 0f, 0f, 0f);
         gl.glShadeModel(gl.GL_SMOOTH);

         gl.glEnable(gl.GL_LIGHTING);
         gl.glEnable(gl.GL_LIGHT0);
         gl.glEnable(gl.GL_CULL_FACE);
         gl.glEnable(gl.GL_DEPTH_TEST);
         gl.glPolygonMode(gl.GL_FRONT_AND_BACK, gl.GL_LINE);
         gl.glColorMaterial(gl.GL_FRONT_AND_BACK, gl.GL_AMBIENT_AND_DIFFUSE);

         gl.glPointSize(10.0f);

         PhysicsSystem.Init();
         RenderSystem.Init();

         InitSpheres();

         camera = new Camera();
         camera.Position = new Vector3(0, 5, 20);
         camera.Clamp(-70, 70);

         Cursor.Position = new Point(this.Width / 2, this.Height / 2);
         lastM = Cursor.Position;
      }

      private void InitSpheres()
      {
         //spheres.Add(new Sphere(oWorld, oSpace, new Vector3(-2, 4, 0), new Vector3( 100000, 400000, 0), 1));
         //spheres.Add(new Sphere(oWorld, oSpace, new Vector3(2, 4, 0), new Vector3(-100000, 0, 0), 1));
         //spheres.Add(new Sphere(oWorld, oSpace, new Vector3(1, 5, 1), new Vector3(50000, 100000, 0), 1));
         //spheres.Add(new Sphere(oWorld, oSpace, new Vector3(0, 1, 0), new Vector3(0, 500, 0), 1));
         PhysicsSystem.AddPhysicable(0, new ode.dVector3(0, 1, 0), new ode.dVector3(0, 500, 0), 1, 1);
         RenderSystem.AddRenderable(0, new Zot.Core.Color(255, 0, 0));
      }

      private void RenderScene()
      {
         gl.glClear(gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);
         gl.glLoadIdentity();

         // place camera
         SetupCamera();

         // draw floor
         gl.glBegin(gl.GL_QUADS);
         gl.glNormal3f(0f, 1f, 0f);
         gl.glVertex3f(-15f, 0f, 15f);
         gl.glVertex3f( 15f, 0f, 15f);
         gl.glVertex3f( 15f, 0f,-15f);
         gl.glVertex3f(-15f, 0f,-15f);
         gl.glEnd();

         RenderSystem.Render();
      }

      private void SetupView(int width, int height)
      {
         height = (0 == height) ? 1 : height;
         gl.glViewport(0, 0, width, height);
         gl.glMatrixMode(gl.GL_PROJECTION);
         gl.glLoadIdentity();
         glu.gluPerspective(45f, (float)width / (float)height, 1f, 1000f);
         gl.glMatrixMode(gl.GL_MODELVIEW);
         gl.glLoadIdentity();
      }

      private void SetupCamera()
      {
         gl.glRotated(camera.Pitch, 1, 0, 0);
         gl.glRotated(camera.Yaw, 0, 1, 0);
         gl.glTranslated(-camera.Position.X, -camera.Position.Y, -camera.Position.Z);
      }
      #endregion

      #region overrides
      /// <summary>
      /// OnResize override
      /// </summary>
      /// <param name="e"></param>
      protected override void OnResize(EventArgs e)
      {
         base.OnResize(e);
         SetupView(this.Width, this.Height);
      }
      #endregion

      #region keyboard
      private void glControl_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyCode == Keys.Escape)
         {
            this.Close();
         }
         if (e.KeyCode == Keys.W)
         {
            camera.MoveForward(1f);
         }
         if (e.KeyCode == Keys.S)
         {
            camera.MoveForward(-1f);
         }
         if (e.KeyCode == Keys.A)
         {
         }
         if (e.KeyCode == Keys.D)
         {
         }
         if (e.KeyCode == Keys.Space)
         {
         }
         if (e.KeyCode == Keys.X)
         {
         }
      }

      private void glControl_KeyUp(object sender, KeyEventArgs e)
      {
         double yawR = camera.Yaw / 180.0 * Math.PI;
         double pitchR = camera.Pitch / 180.0 * Math.PI;
         if (e.KeyCode == Keys.W)
         {
         }
         if (e.KeyCode == Keys.S)
         {
         }
         if (e.KeyCode == Keys.A)
         {
         }
         if (e.KeyCode == Keys.D)
         {
         }
         if (e.KeyCode == Keys.Space)
         {
         }
         if (e.KeyCode == Keys.X)
         {
         }
         if (e.KeyCode == Keys.F1)
         {
            if (fullscreen)
            {
               this.Width = 800;
               this.Height = 600;
               this.FormBorderStyle = FormBorderStyle.FixedSingle;
               SetupView(this.Width, this.Height);
               fullscreen = !fullscreen;
            }
            else
            {
               this.Location = new Point(0, 0);
               this.Width = Screen.PrimaryScreen.Bounds.Width;
               this.Height = Screen.PrimaryScreen.Bounds.Height;
               this.FormBorderStyle = FormBorderStyle.None;
               SetupView(this.Width, this.Height);
               fullscreen = !fullscreen;
            }
         }
      }
      #endregion

      #region mouse
      private void glControl_MouseDown(object sender, MouseEventArgs e)
      {
      }

      private void glControl_MouseUp(object sender, MouseEventArgs e)
      {
      }

      private void glControl_MouseMove(object sender, MouseEventArgs e)
      {
         if (wrapping)
         {
            wrapping = false;
            Cursor.Position = new Point(wrapTo.X, wrapTo.Y);
            return;
         }
         if (e.Location.X >= this.Width - WRAP_MARGIN)
         {
            wrapping = true;
            lastM.X = 0;
            wrapTo = new Point(WRAP_MARGIN + 1, e.Location.Y);
            return;
         }
         if (e.Location.X <= WRAP_MARGIN)
         {
            wrapping = true;
            lastM.X = this.Width - 1;
            wrapTo = new Point(this.Width - WRAP_MARGIN - 1, e.Location.Y);
            return;
         }
         diffM.X = e.Location.X - lastM.X;
         diffM.Y = e.Location.Y - lastM.Y;
         lastM.X = e.Location.X;
         lastM.Y = e.Location.Y;
         camera.Yaw += (double)diffM.X * 0.1;
         camera.Pitch += (double)diffM.Y * 0.1;
      }
      #endregion

      private void glControl_Paint(object sender, PaintEventArgs e)
      {
         RenderScene();
      }

      private void timer1_Tick(object sender, EventArgs e)
      {
         float dt = ((Timer)sender).Interval / 1000.0f * 2f;
         PhysicsSystem.Update(dt);
         this.Refresh();
      }
   }
}
