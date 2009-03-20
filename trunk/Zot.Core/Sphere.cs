using System;

using gl = Tao.OpenGl.Gl;
using glu = Tao.OpenGl.Glu;
using ode = Tao.Ode.Ode;

namespace Zot.Core
{
   /// <summary>
   /// Sphere
   /// </summary>
   public class Sphere
   {
      /// <summary>
      /// Sphere
      /// </summary>
      /// <param name="world"></param>
      /// <param name="space"></param>
      /// <param name="pos"></param>
      /// <param name="force"></param>
      /// <param name="radius"></param>
      public Sphere(IntPtr world, IntPtr space, Vector3 pos, Vector3 force, double radius)
      {
         this.bodyID = ode.dBodyCreate(world);
         this.geomID = ode.dCreateSphere(space, (float)radius);
         this.Position.X = (float)pos.X; this.Position.Y = (float)pos.Y; this.Position.Z = (float)pos.Z;
         this.force.X = (float)force.X; this.force.Y = (float)force.Y; this.force.Z = (float)force.Z;
         this.radius = radius;
         this.quadric = glu.gluNewQuadric();

         mass = new Tao.Ode.Ode.dMass();
         ode.dMassSetSphere(ref mass, 1f, (float)this.radius);
         mass.mass = 1.0f;
         ode.dBodySetMass(this.bodyID, ref this.mass);
         ode.dBodySetPosition(this.bodyID, this.Position.X, this.Position.Y, this.Position.Z);
         ode.dBodyAddForce(this.bodyID, this.force.X, this.force.Y, this.force.Z);
         ode.dGeomSetBody(this.geomID, this.bodyID);
      }

      /// <summary>
      /// Draw
      /// </summary>
      public void Draw()
      {
         // get the updated the position
         this.Position = ode.dGeomGetPosition(geomID);
         ode.dQuaternion q = new Tao.Ode.Ode.dQuaternion();
         ode.dGeomGetQuaternion(this.geomID, ref q);

         float cosa = q.W;
         float angle = (float)(Math.Acos(cosa) * 2f);
         float sina = (float)(Math.Sqrt(1f - cosa * cosa));
         float rad = ode.dGeomSphereGetRadius(geomID);
         sina = (Math.Abs(sina) < 0.0005f) ? 1f : 1f / sina;
         angle *= 57.295779513082320876798154814105f; // hack lol
         q.X *= sina;
         q.Y *= sina;
         q.Z *= sina;

         gl.glPushMatrix();
         gl.glTranslatef(Position.X, Position.Y, Position.Z);
         gl.glRotatef(angle, q.X, q.Y, q.Z);
         glu.gluSphere(quadric, rad, 15, 15);
         gl.glPopMatrix();
      }
   }
}
