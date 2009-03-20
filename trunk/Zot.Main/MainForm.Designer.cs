namespace Zot.Main
{
   partial class MainForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.glControl = new Tao.Platform.Windows.SimpleOpenGlControl();
         this.timer1 = new System.Windows.Forms.Timer(this.components);
         this.SuspendLayout();
         // 
         // glControl
         // 
         this.glControl.AccumBits = ((byte)(0));
         this.glControl.AutoCheckErrors = false;
         this.glControl.AutoFinish = false;
         this.glControl.AutoMakeCurrent = true;
         this.glControl.AutoSwapBuffers = true;
         this.glControl.BackColor = System.Drawing.Color.Black;
         this.glControl.ColorBits = ((byte)(32));
         this.glControl.DepthBits = ((byte)(16));
         this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
         this.glControl.Location = new System.Drawing.Point(0, 0);
         this.glControl.Name = "glControl";
         this.glControl.Size = new System.Drawing.Size(1400, 1050);
         this.glControl.StencilBits = ((byte)(0));
         this.glControl.TabIndex = 0;
         this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
         this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseMove);
         this.glControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyUp);
         this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDown);
         this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
         this.glControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyDown);
         // 
         // timer1
         // 
         this.timer1.Enabled = true;
         this.timer1.Interval = 10;
         this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1400, 1050);
         this.Controls.Add(this.glControl);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Name = "MainForm";
         this.Text = "Form1";
         this.ResumeLayout(false);

      }

      #endregion

      private Tao.Platform.Windows.SimpleOpenGlControl glControl;
      private System.Windows.Forms.Timer timer1;
   }
}

