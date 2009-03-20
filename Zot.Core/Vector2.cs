using System;

namespace Zot.Core
{
   /// <summary>
   /// Vector2
   /// </summary>
   public class Vector2
   {
      private double x, y;

      #region Constructors
      /// <summary>
      /// Vector2 default constructor
      /// </summary>
      public Vector2()
      {
         Set(0.0, 0.0);
      }

      /// <summary>
      /// Vector2 constructor
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      public Vector2(double x, double y)
      {
         Set(x, y);
      }

      /// <summary>
      /// Vector2 constructor
      /// </summary>
      /// <param name="v"></param>
      public Vector2(Vector2 v)
      {
         if (null == v)
            throw new ArgumentNullException("Vector(v)");

         Set(v);
      }
      #endregion

      #region Properties
      /// <summary>
      /// X property
      /// </summary>
      public double X { get { return this.x; } set { this.x = value; } }
      /// <summary>
      /// Y property
      /// </summary>
      public double Y { get { return this.y; } set { this.y = value; } }

      /// <summary>
      /// Length
      /// </summary>
      public double Length { get { return Math.Sqrt((x * x) + (y * y)); } }
      #endregion

      #region Operators

      /// <summary>
      /// Equality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator ==(Vector2 lhs, Vector2 rhs)
      {
         if (ReferenceEquals(lhs, null) && !ReferenceEquals(rhs, null))
            return false;
         if (!ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null))
            return false;
         if (ReferenceEquals(lhs, null) && ReferenceEquals(rhs, null))
            return true;
         return lhs.Equals(rhs);
      }

      /// <summary>
      /// Inequality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator !=(Vector2 lhs, Vector2 rhs)
      {
         return !(lhs == rhs);
      }

      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
      {
         return new Vector2(lhs.X + rhs.X,
                           lhs.Y + rhs.Y);
      }

      /// <summary>
      /// Subtraction operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
      {
         return new Vector2(lhs.X - rhs.X,
                           lhs.Y - rhs.Y);
      }

      /// <summary>
      /// Negation operator
      /// </summary>
      /// <returns></returns>
      public static Vector2 operator -(Vector2 v)
      {
         return new Vector2(-v.X, -v.Y);
      }

      /// <summary>
      /// Scalar multiplication operator
      /// </summary>
      /// <param name="v"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Vector2 operator *(Vector2 v, double val)
      {
         return new Vector2(v.X * val,
                           v.Y * val);
      }

      /// <summary>
      /// Matrix2 multiplication operator
      /// </summary>
      /// <param name="v"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Vector2 operator *(Vector2 v, Matrix2 m)
      {
         return new Vector2(v | m.Row(0), v | m.Row(1));
      }

      /// <summary>
      /// Scalar division operator
      /// </summary>
      /// <param name="v"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Vector2 operator /(Vector2 v, double val)
      {
         return new Vector2(v.X / val,
                           v.Y / val);
      }

      /// <summary>
      /// Dot product
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static double operator |(Vector2 lhs, Vector2 rhs)
      {
         return (lhs.X * rhs.X) + (lhs.Y * rhs.Y);
      }
      #endregion

      #region Overrides
      /// <summary>
      /// Equals
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         Vector2 v = (Vector2)obj;
         return Math.Abs(this.x - v.X) < Types.EPSILON &&
                Math.Abs(this.y - v.Y) < Types.EPSILON;
      }

      /// <summary>
      /// GetHashCode
      /// </summary>
      /// <returns></returns>
      public override int GetHashCode()
      {
         return base.GetHashCode();
      }
      #endregion

      /// <summary>
      /// Normalize
      /// </summary>
      public void Normalize()
      {
         Set(this / this.Length);
      }

      /// <summary>
      /// Set
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      public void Set(double x, double y)
      {
         this.x = x; this.y = y;
      }

      /// <summary>
      /// Set
      /// </summary>
      /// <param name="v"></param>
      public void Set(Vector2 v)
      {
         Set(v.X, v.Y);
      }
   }
}
