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
   public class PhysicsComponent : BaseComponent
   {
      /// <summary>
      ///
      /// </summary>
      public ode.dVector3 Force;
      private IntPtr geomID;
      private IntPtr bodyID;
      /// <summary>
      /// 
      /// </summary>
      public ode.dMass Mass;
      private double radius;

      public PhysicsComponent(int entity) : base(entity) { }

      /// <summary>
      /// 
      /// </summary>
      public IntPtr GeomID { get { return geomID; } set { geomID = value; } }
      /// <summary>
      /// 
      /// </summary>
      public IntPtr BodyID { get { return bodyID; } set { bodyID = value; } }
      /// <summary>
      /// 
      /// </summary>
      public double Radius { get { return radius; } set { radius = value; } }
   }
}
