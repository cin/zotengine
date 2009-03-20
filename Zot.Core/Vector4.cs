using System;

namespace Zot.Core
{
   /// <summary>
   /// Vector4
   /// </summary>
   public class Vector4
   {
      /// <summary>
      /// x, y, z, w
      /// </summary>
      protected double x, y, z, w;

      #region Constructors
      /// <summary>
      /// Vector4 default constructor
      /// </summary>
      public Vector4()
      {
         Set(0.0, 0.0, 0.0, 0.0);
      }

      /// <summary>
      /// Vector4 constructor
      /// </summary>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <param name="z"></param>
      /// <param name="w"></param>
      public Vector4(double x, double y, double z, double w)
      {
         Set(x, y, z, w);
      }

      /// <summary>
      /// Vector4 constructor
      /// </summary>
      /// <param name="v"></param>
      public Vector4(Vector4 v)
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
      /// W property
      /// </summary>
      public double W { get { return this.w; } set { this.w = value; } }
      
      /// <summary>
      /// Length
      /// </summary>
      public double Length { get { return Math.Sqrt((double)((x * x) + (y * y) + (z * z) + (w * w))); } }
      #endregion

      #region Operators

      /// <summary>
      /// Equality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator ==(Vector4 lhs, Vector4 rhs)
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
      public static bool operator !=(Vector4 lhs, Vector4 rhs)
      {
         return !(lhs == rhs);
      }

      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
      {
         return new Vector4(lhs.X + rhs.X,
                              lhs.Y + rhs.Y,
                              lhs.Z + rhs.Z,
                              lhs.W + rhs.W);
      }

      /// <summary>
      /// Subtraction operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
      {
         return new Vector4(lhs.X - rhs.X,
                              lhs.Y - rhs.Y,
                              lhs.Z - rhs.Z,
                              lhs.W - rhs.W);
      }

      /// <summary>
      /// Negation operator
      /// </summary>
      /// <returns></returns>
      public static Vector4 operator -(Vector4 v)
      {
         return new Vector4(-v.X, -v.Y, -v.Z, -v.W);
      }

      /// <summary>
      /// Multiplication operator (scalar)
      /// </summary>
      /// <param name="v"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Vector4 operator *(Vector4 v, double val)
      {
         return new Vector4(v.X * val,
                              v.Y * val,
                              v.Z * val,
                              v.W * val);
      }

      /// <summary>
      /// Division operator (scalar)
      /// </summary>
      /// <param name="v"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Vector4 operator /(Vector4 v, double val)
      {
         return new Vector4(v.X / val,
                              v.Y / val,
                              v.Z / val,
                              v.W / val);
      }

      /// <summary>
      /// Dot Product operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static double operator |(Vector4 lhs, Vector4 rhs)
      {
         return (lhs.X * rhs.X) +
                (lhs.Y * rhs.Y) +
                (lhs.Z * rhs.Z) +
                (lhs.W * rhs.W);
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
         Vector4 v = (Vector4)obj;
         return Math.Abs(this.x - v.X) < Types.EPSILON &&
                Math.Abs(this.y - v.Y) < Types.EPSILON &&
                Math.Abs(this.z - v.Z) < Types.EPSILON &&
                Math.Abs(this.w - v.W) < Types.EPSILON;
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
      /// <param name="z"></param>
      /// <param name="w"></param>
      public void Set(double x, double y, double z, double w)
      {
         this.x = x; this.y = y; this.z = z; this.w = w;
      }

      /// <summary>
      /// Set
      /// </summary>
      /// <param name="v"></param>
      public void Set(Vector4 v)
      {
         Set(v.X, v.Y, v.Z, v.W);
      }

      /// <summary>
      /// Quaternion
      /// </summary>
      /// <param name="axis"></param>
      /// <param name="angle"></param>
      /// <returns></returns>
      public static Vector4 Quaternion(Vector3 axis, double angle)
      {
         Vector4 quat = new Vector4();
         double rad = (angle / 180.0) * Math.PI;
         double res = Math.Sin(rad / 2.0);
         quat.W = Math.Cos(rad / 2.0);
         quat.X = axis.X * res;
         quat.Y = axis.Y * res;
         quat.Z = axis.Z * res;
         return quat;
      }
   }
}
