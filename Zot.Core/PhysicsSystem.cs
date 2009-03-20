using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ode = Tao.Ode.Ode;

namespace Zot.Core
{
   /// <summary>
   /// 
   /// </summary>
   public class PhysicsSystem
   {
      private static IntPtr oWorld;
      private static IntPtr oSpace;
      private static IntPtr oGroup;
      private static Dictionary<int, PhysicsComponent> physicables;

      /// <summary>
      /// 
      /// </summary>
      public static void Init()
      {
         oWorld = ode.dWorldCreate();
         ode.dWorldSetERP(oWorld, 0.2f);
         ode.dWorldSetCFM(oWorld, 1e-6f);
         ode.dWorldSetGravity(oWorld, 0.0f, -9.81f, 0.0f);
         oSpace = ode.dSimpleSpaceCreate(oSpace);
         ode.dCreatePlane(oSpace, 0.0f, 1.0f, 0.0f, 0.0f);
         oGroup = ode.dJointGroupCreate(0);
         physicables = new Dictionary<int, PhysicsComponent>();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="entity"></param>
      /// <param name="pos"></param>
      /// <param name="force"></param>
      /// <param name="mass"></param>
      /// <param name="radius"></param>
      public static void AddPhysicable(int entity, ode.dVector3 pos, ode.dVector3 force, double mass, double radius)
      {
         PhysicsComponent c = new PhysicsComponent(entity);
         c.BodyID = ode.dBodyCreate(oWorld);
         c.GeomID = ode.dCreateSphere(oSpace, (float)radius);
         c.Force.X = (float)force.X; c.Force.Y = (float)force.Y; c.Force.Z = (float)force.Z;
         c.Radius = radius;
         c.Mass = new Tao.Ode.Ode.dMass();
         ode.dMassSetSphere(ref c.Mass, 1f, (float)c.Radius);
         c.Mass.mass = 1f;
         ode.dBodySetMass(c.BodyID, ref c.Mass);
         ode.dBodySetPosition(c.BodyID, pos.X, pos.Y, pos.Z);
         ode.dBodyAddForce(c.BodyID, c.Force.X, c.Force.Y, c.Force.Z);
         ode.dGeomSetBody(c.GeomID, c.BodyID);
         physicables.Add(entity, c);
      }

      public static void NudgePhysicable(int entity, ode.dVector3 force)
      {
         PhysicsComponent c = physicables[entity];
         ode.dBodyAddForce(c.BodyID, force.X, force.Y, force.Z);
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="entity"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      /// <returns></returns>
      public static bool GetPosition(int entity, ref float x, ref float y, ref float z)
      {
         PhysicsComponent p = physicables[entity];
         if (p != null)
         {
            ode.dVector3 v = ode.dGeomGetPosition(p.GeomID);
            x = v.X;
            y = v.Y;
            z = v.Z;
            return true;
         }
         return false;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="entity"></param>
      /// <param name="radius"></param>
      /// <returns></returns>
      public static bool GetRadius(int entity, ref float radius)
      {
         PhysicsComponent p = physicables[entity];
         if (p != null)
         {
            radius = (float)p.Radius;
            return true;
         }
         return false;
      }

      private static void NearCallback(IntPtr data, IntPtr geom1, IntPtr geom2)
      {
         int i;
         IntPtr b1 = ode.dGeomGetBody(geom1);
         IntPtr b2 = ode.dGeomGetBody(geom2);
         int MAX_COLL = 20;
         ode.dContactGeom[] cGeoms = new Tao.Ode.Ode.dContactGeom[MAX_COLL];
         int num_coll = ode.dCollide(geom1, geom2, MAX_COLL, cGeoms, 100);
         Console.WriteLine(num_coll);
         ode.dContact[] contact = new Tao.Ode.Ode.dContact[num_coll];
         for (i = 0; i < num_coll; i++)
         {
            ode.dSurfaceParameters surface = contact[i].surface;
            surface.mode = (int)ode.dContactFlags.dContactBounce;
            surface.mu = ode.dInfinity;
            surface.mu2 = 0;
            surface.bounce = 0.4f;
            surface.bounce_vel = 0.9f;
            surface.soft_cfm = 0.9f;
            contact[i].surface = surface;
            contact[i].geom = cGeoms[i];
            IntPtr c = ode.dJointCreateContact(oWorld, oGroup, ref contact[i]);
            ode.dJointAttach(c, b1, b2);
         }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dt"></param>
      public static void Update(float dt)
      {
         ode.dSpaceCollide(oSpace, new IntPtr(), new ode.dNearCallback(NearCallback));
         ode.dWorldStep(oWorld, dt);
         ode.dJointGroupEmpty(oGroup);
      }
   }
}
