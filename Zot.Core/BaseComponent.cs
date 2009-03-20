using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zot.Core
{
   public class BaseComponent
   {
      private int owner;

      public BaseComponent(int entity)
      {
         owner = entity;
      }

      public int Owner { get { return owner; } }
   }
}
