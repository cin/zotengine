using System;
using System.Windows.Forms;

using Zot.Core;

namespace Zot.Main
{
   class Program
   {
      static void Main(string[] args)
      {
         int w = 1024;
         int h = 768;
         int b = 32;
         string t = "zot";
         bool fs = false;
         GLWindow window = new GLWindow();
         if (!window.Create(w, h, b, t, fs))
         {
            MessageBox.Show("GLWindow.Create() failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
         }
         while (!window.Done)
         {
            Application.DoEvents();
            window.ParseInput();
            window.DrawScene();
            if (window.FSToggle)
            {
               fs = !fs;
               window.Destroy();
               if (!window.Create(w, h, b, t, fs))
               {
                  MessageBox.Show("Toggling fullscreen didn't seem to work, so i'm reverting", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  fs = !fs;
                  window.Destroy();
                  if (!window.Create(w, h, b, t, fs))
                  {
                     MessageBox.Show("I've made a huge mistake", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
               }
            }
         }
      }
   }
}
