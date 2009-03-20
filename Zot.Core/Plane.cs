using System;

namespace Zot.Core
{
   /// <summary>
   /// Plane
   /// </summary>
   public class Plane
   {
      private Vector3 point;
      private Vector3 normal;

      /// <summary>
      /// Plane
      /// </summary>
      public Plane()
      {
         // default to the XZ plane?
         point = new Vector3(0, 0, 0);
         normal = new Vector3(0, 1, 0);
      }

      /// <summary>
      /// Plane
      /// </summary>
      /// <param name="point"></param>
      /// <param name="normal"></param>
      public Plane(Vector3 point, Vector3 normal)
      {
         this.point = new Vector3(point);
         this.normal = new Vector3(normal);
      }

      /// <summary>
      /// Point
      /// </summary>
      public Vector3 Point { get { return point; } set { point = value; } }
      /// <summary>
      /// Normal
      /// </summary>
      public Vector3 Normal { get { return normal; } set { normal = Vector3.Normalize(value); } }
   }
}
