using System;

namespace Zot.Core
{
   /// <summary>
   /// Matrix2 describes a 2x2 matrix of doubles
   /// </summary>
   public class Matrix2
   {
      private double[] data;

      #region Constructors
      /// <summary>
      /// Matrix2 default constructor
      /// </summary>
      public Matrix2()
      {
         data = Identity.Data;
      }

      /// <summary>
      /// Matrix2 constructor
      /// </summary>
      /// <param name="d0"></param>
      /// <param name="d1"></param>
      /// <param name="d2"></param>
      /// <param name="d3"></param>
      public Matrix2(double d0, double d1,
                     double d2, double d3)
      {
         data = new double[4] { d0, d1, d2, d3 };
      }

      /// <summary>
      /// Matrix2
      /// </summary>
      /// <param name="data"></param>
      public Matrix2(double[] data)
      {
         if (data == null)
            throw new ArgumentNullException("data");

         if (data.Length != 4)
            throw new ArgumentOutOfRangeException("data length must be 9");

         this.data = data;
      }

      /// <summary>
      /// Matrix2
      /// </summary>
      /// <param name="mat"></param>
      public Matrix2(Matrix2 mat)
      {
         if (null == mat)
            throw new ArgumentNullException("mat is null");

         data = mat.Data;
      }
      #endregion

      #region Properties
      /// <summary>
      /// Pleasant indexing
      /// </summary>
      /// <param name="i"></param>
      /// <returns></returns>
      public double this[int i]
      {
         get { return data[i]; }
         set { data[i] = value; }
      }

      /// <summary>
      /// More pleasant indexing
      /// </summary>
      /// <param name="r"></param>
      /// <param name="c"></param>
      /// <returns></returns>
      public double this[int r, int c]
      {
         get { return data[r * 2 + c]; }
         set { data[r * 2 + c] = value; }
      }

      /// <summary>
      /// Internal array
      /// </summary>
      public double[] Data
      {
         get { return data; }
      }

      /// <summary>
      /// Determinant
      /// </summary>
      public double Determinant
      {
         get { return (data[0] * data[3] - data[1] * data[2]); }
      }

      /// <summary>
      /// Identity
      /// </summary>
      public static Matrix2 Identity
      {
         get { return new Matrix2(1.0, 0.0, 0.0, 1.0); }
      }
      #endregion

      #region Operators
      /// <summary>
      /// Equality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator ==(Matrix2 lhs, Matrix2 rhs)
      {
         return lhs.Equals(rhs);
      }

      /// <summary>
      /// Inequality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator !=(Matrix2 lhs, Matrix2 rhs)
      {
         return !lhs.Equals(rhs);
      }

      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Matrix2 operator +(Matrix2 lhs, Matrix2 rhs)
      {
         Matrix2 ret = new Matrix2();
         for (int i = 0; i < 4; i++)
         {
            ret[i] = lhs[i] + rhs[i];
         }
         return ret;
      }

      /// <summary>
      /// Scalar multiplication operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Matrix2 operator *(Matrix2 lhs, double val)
      {
         Matrix2 ret = new Matrix2();
         for (int i = 0; i < 4; i++)
         {
            ret[i] = lhs[i] * val;
         }
         return ret;
      }

      /// <summary>
      /// Matrix multiplication operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Matrix2 operator *(Matrix2 lhs, Matrix2 rhs)
      {
         Matrix2 ret = new Matrix2();
         for (int r = 0; r < 2; r++)
         {
            for (int c = 0; c < 2; c++)
            {
               ret[r, c] = lhs.Row(r) | rhs.Col(c);
            }
         }
         return ret;
      }

      /// <summary>
      /// Vector2 multiplication operator
      /// </summary>
      /// <param name="m"></param>
      /// <param name="v"></param>
      /// <returns></returns>
      public static Vector2 operator *(Matrix2 m, Vector2 v)
      {
         return new Vector2(v | m.Row(0), v | m.Row(1));
      }

      /// <summary>
      /// Scalar division operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Matrix2 operator /(Matrix2 lhs, double val)
      {
         Matrix2 ret = new Matrix2();
         for (int i = 0; i < 4; i++)
         {
            ret[i] = lhs[i] / val;
         }
         return ret;
      }
      #endregion

      /// <summary>
      /// Column accessor
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public Vector2 Col(int c)
      {
         return new Vector2(data[2 * 0 + c], data[2 * 1 + c]);
      }

      /// <summary>
      /// Row accessor
      /// </summary>
      /// <param name="r"></param>
      /// <returns></returns>
      public Vector2 Row(int r)
      {
         return new Vector2(data[r * 2 + 0], data[r * 2 + 1]);
      }

      /// <summary>
      /// Transpose
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix2 Transpose(Matrix2 m)
      {
         return new Matrix2(m[0], m[2], m[1], m[3]);
      }

      /// <summary>
      /// Inverse
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix2 Inverse(Matrix2 m)
      {
         return new Matrix2(m[3], -m[1], -m[2], m[0]);
      }

      #region Overrides
      /// <summary>
      /// Equals
      /// </summary>
      /// <param name="obj"></param>
      /// <returns></returns>
      public override bool Equals(object obj)
      {
         Matrix2 m = (Matrix2)obj;
         for (int i = 0; i < 4; i++)
         {
            if (Math.Abs(data[i] - m[i]) > Types.EPSILON)
               return false;
         }
         return true;
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
   }
}
