using System;

using gl = Tao.OpenGl.Gl;

namespace Zot.Core
{
   public class Camera
   {
      private Vector3 position;
      private double yaw;
      private double pitch;

      public bool MovingForward;
      public bool MovingBackward;
      public bool MovingLeft;
      public bool MovingRight;
      public bool MovingUp;
      public bool MovingDown;

      public Camera()
      {
         position = new Vector3(0, 0, 0);
         yaw = pitch = 0;
      }

      public Vector3 Position { get { return position; } }

      public double Yaw
      {
         get { return yaw; }
         set
         {
            yaw = value;
            if (Math.Abs(yaw) > 360)
               yaw %= 360;
         }
      }

      public double Pitch
      {
         get { return pitch; }
         set
         {
            pitch = value;
            if (pitch > 75)
               pitch = 75;
            if (pitch < -75)
               pitch = -75;
         }
      }

      public void PlaceAt(double x, double y, double z)
      {
         position.Set(x, y, z);
      }

      public void PlaceAt(Vector3 v)
      {
         position.Set(v);
      }

      public void Reset()
      {
         position.Set(0, 0, 0);
         yaw = pitch = 0;
      }

      public void Setup()
      {
         double yrad = yaw * Vector3.DEG2RAD, prad = pitch * Vector3.DEG2RAD;

         if (MovingForward)
         {
            position.X += Math.Sin(yrad);
            position.Y -= Math.Sin(prad);
            position.Z -= Math.Cos(yrad);
         }
         if (MovingBackward)
         {
            position.X -= Math.Sin(yrad);
            position.Y += Math.Sin(prad);
            position.Z += Math.Cos(yrad);
         }
         if (MovingLeft)
         {
            position.X -= Math.Cos(yrad);
            position.Z -= Math.Sin(yrad);
         }
         if (MovingRight)
         {
            position.X += Math.Cos(yrad);
            position.Z += Math.Sin(yrad);
         }
         if (MovingUp)
         {
            position.Y += 0.5;
         }
         if (MovingDown)
         {
            Position.Y -= 0.5;
         }
         gl.glRotated(Pitch, 1, 0, 0);
         gl.glRotated(Yaw, 0, 1, 0);
         gl.glTranslated(-position.X, -position.Y, -position.Z);
      }
   }
}
