using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using gl = Tao.OpenGl.Gl;
using glu = Tao.OpenGl.Glu;

namespace Zot.Core
{
   /// <summary>
   /// 
   /// </summary>
   public class RenderSystem
   {
      private static Dictionary<int, RenderComponent> renderables;

      /// <summary>
      /// 
      /// </summary>
      public static void Init()
      {
         renderables = new Dictionary<int, RenderComponent>();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="entity"></param>
      /// <param name="color"></param>
      public static void AddRenderable(int entity, Color color)
      {
         RenderComponent c = new RenderComponent(entity);
         c.Color = color;
         c.Quadric = glu.gluNewQuadric();
         renderables.Add(entity, c);
      }

      /// <summary>
      /// 
      /// </summary>
      public static void Render()
      {
         foreach (KeyValuePair<int, RenderComponent> kv in renderables)
         {
            float x = 0f, y = 0f, z = 0f, rad = 0f;
            PhysicsSystem.GetPosition(kv.Key, ref x, ref y, ref z);
            PhysicsSystem.GetRadius(kv.Key, ref rad);
            gl.glPushMatrix();
            gl.glTranslatef(x, y, z);
            glu.gluQuadricNormals(kv.Value.Quadric, glu.GLU_SMOOTH);
            gl.glColor3ub(kv.Value.Color.R, kv.Value.Color.G, kv.Value.Color.B);
            glu.gluSphere(kv.Value.Quadric, rad, 15, 15);
            gl.glPopMatrix();
         }
      }
   }
}
