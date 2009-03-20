using System;
using System.Collections.Generic;

namespace Zot.Core
{
   public class Polygon
   {
      private List<Vertex> vertices;

      public Polygon()
      {
         vertices = new List<Vertex>();
      }

      public Polygon(List<Vertex> v)
      {
         vertices = new List<Vertex>();
         foreach (Vertex vert in v)
         {
            vertices.Add((Vertex)vert.Clone());
         }
      }

      public List<Vertex> V { get { return vertices; } }

      public Vertex this[int i] { get { return vertices[i]; } }

      public void Split(Plane p, ref Polygon front, ref Polygon back)
      {
      }
   }
}
