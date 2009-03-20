using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zot.Core
{
   /// <summary>
   /// Vertex
   /// </summary>
   public class Vertex : Vector3
   {
      private Vector3 normal;
      private Vector3 color;

      /// <summary>
      /// Vertex default constructor
      /// </summary>
      public Vertex()
         : base()
      {
         normal = new Vector3();
         color = new Vector3(1, 1, 1);
      }

      /// <summary>
      /// Vertex
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      public Vertex(double x, double y, double z)
         : base(x, y, z)
      {
         normal = new Vector3();
         color = new Vector3(1, 1, 1);
      }

      /// <summary>
      /// Vertex
      /// </summary>
      /// <param name="v"></param>
      public Vertex(Vector3 v)
         : base(v)
      {
         if (v.GetType().ToString() == "Vertex")
         {
            normal = new Vector3(((Vertex)v).Normal);
            color = new Vector3(((Vertex)v).Color);
         }
         else
         {
            normal = new Vector3();
            color = new Vector3(1, 1, 1);
         }
      }

      /// <summary>
      /// Vertex
      /// </summary>
      /// <param name="v"></param>
      public Vertex(Vertex v)
      {
         Set(v);
         normal = new Vector3(v.Normal);
         color = new Vector3(v.Color);
      }

      public Vertex(Vector3 v, Vector3 n)
      {
         Set(v);
         normal = new Vector3(n);
      }

      /// <summary>
      /// Normal
      /// </summary>
      public Vector3 Normal { get { return normal; } set { normal = value; } }

      /// <summary>
      /// Color
      /// </summary>
      public Vector3 Color { get { return color; } set { color = value; } }

      public override object Clone()
      {
         Vertex v = new Vertex();
         v.X = x; v.Y = y; v.Z = z;
         v.Normal.Set(normal);
         v.Color = Color;
         return v;
      }
   }
}
