using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using glu = Tao.OpenGl.Glu;

namespace Zot.Core
{
   /// <summary>
   ///
   /// </summary>
   public class RenderComponent : BaseComponent
   {
      private Color color;
      private glu.GLUquadric quadric;

      public RenderComponent(int entity) : base(entity) { }

      /// <summary>
      /// 
      /// </summary>
      public Color Color { get { return color; } set { color = value; } }
      /// <summary>
      /// 
      /// </summary>
      public glu.GLUquadric Quadric { get { return quadric; } set { quadric = value; } }
   }
}
