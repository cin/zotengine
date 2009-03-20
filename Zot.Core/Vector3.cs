using System;

namespace Zot.Core
{
   /// <summary>
   /// Vector3
   /// </summary>
   public class Vector3 : ICloneable
   {
      /// <summary>
      /// x, y, z
      /// </summary>
      protected double x, y, z;

      public static readonly double DEG2RAD = Math.PI / 180.0;

      #region Constructors
      /// <summary>
      /// Vector3 default constructor
      /// </summary>
      public Vector3()
      {
         Set(0.0, 0.0, 0.0);
      }

      /// <summary>
      /// Vector3 constructor
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      public Vector3(double x, double y, double z)
      {
         Set(x, y, z);
      }

      /// <summary>
      /// Vector3 constructor
      /// </summary>
      /// <param name="v"></param>
      public Vector3(Vector3 v)
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
      /// Z property
      /// </summary>
      public double Z { get { return this.z; } set { this.z = value; } }

      /// <summary>
      /// XAxis
      /// </summary>
      public static Vector3 XAxis { get { return new Vector3(1, 0, 0); } }
      /// <summary>
      /// YAxis
      /// </summary>
      public static Vector3 YAxis { get { return new Vector3(0, 1, 0); } }
      /// <summary>
      /// ZAxis
      /// </summary>
      public static Vector3 ZAxis { get { return new Vector3(0, 0, 1); } }

      /// <summary>
      /// Length
      /// </summary>
      public double Length { get { return Math.Sqrt((double)((x * x) + (y * y) + (z * z))); } }
      #endregion

      #region Operators

      /// <summary>
      /// Equality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator ==(Vector3 lhs, Vector3 rhs)
      {
         if (ReferenceEquals(lhs, null) && !ReferenceEquals(rhs, null))
            return false;
         if (ReferenceEquals(rhs, null) && !ReferenceEquals(lhs, null))
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
      public static bool operator !=(Vector3 lhs, Vector3 rhs)
      {
         return !(lhs == rhs);
      }

      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
      {
         return new Vector3(lhs.X + rhs.X,
                               lhs.Y + rhs.Y,
                               lhs.Z + rhs.Z);
      }

      /// <summary>
      /// Subtraction operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
      {
         return new Vector3(lhs.X - rhs.X,
                               lhs.Y - rhs.Y,
                               lhs.Z - rhs.Z);
      }

      /// <summary>
      /// Negation operator
      /// </summary>
      /// <returns></returns>
      public static Vector3 operator -(Vector3 v)
      {
         return new Vector3(-v.X, -v.Y, -v.Z);
      }

      /// <summary>
      /// Multiplication operator (scalar)
      /// </summary>
      /// <param name="v"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Vector3 operator *(Vector3 v, double val)
      {
         return new Vector3(v.X * val,
                           v.Y * val,
                           v.Z * val);
      }

      /// <summary>
      /// Matrix3 multiplication operator
      /// </summary>
      /// <param name="v"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Vector3 operator *(Vector3 v, Matrix3 m)
      {
         return new Vector3(v | m.Row(0), v | m.Row(1), v | m.Row(2));
      }

      /// <summary>
      /// Division operator (scalar)
      /// </summary>
      /// <param name="v"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Vector3 operator /(Vector3 v, double val)
      {
         return new Vector3(v.X / val,
                               v.Y / val,
                               v.Z / val);
      }

      /// <summary>
      /// Dot Product operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static double operator |(Vector3 lhs, Vector3 rhs)
      {
         return (lhs.X * rhs.X) +
                (lhs.Y * rhs.Y) +
                (lhs.Z * rhs.Z);
      }

      /// <summary>
      /// Cross Product operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Vector3 operator ^(Vector3 lhs, Vector3 rhs)
      {
         return new Vector3(
            (lhs.Y * rhs.Z) - (lhs.Z * rhs.Y),
            (lhs.Z * rhs.X) - (lhs.X * rhs.Z),
            (lhs.X * rhs.Y) - (lhs.Y * rhs.X));
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
         if (obj == null)
            return false;
         Vector3 v = (Vector3)obj;
         return Math.Abs(this.x - v.X) < Types.EPSILON &&
                Math.Abs(this.y - v.Y) < Types.EPSILON &&
                Math.Abs(this.z - v.Z) < Types.EPSILON;
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
      /// Normalize
      /// </summary>
      /// <param name="v"></param>
      /// <returns></returns>
      public static Vector3 Normalize(Vector3 v)
      {
         Vector3 ret = new Vector3(v);
         ret.Normalize();
         return ret;
      }

      /// <summary>
      /// Set
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      public void Set(double x, double y, double z)
      {
         this.x = x; this.y = y; this.z = z;
      }

      /// <summary>
      /// Set
      /// </summary>
      /// <param name="v"></param>
      public void Set(Vector3 v)
      {
         Set(v.X, v.Y, v.Z);
      }

      public virtual object Clone()
      {
         return new Vector3(x, y, z);
      }
   }
}
