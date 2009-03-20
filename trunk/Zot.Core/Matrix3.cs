using System;

namespace Zot.Core
{
   /// <summary>
   /// Matrix3 describes a 3x3 matrix of doubles
   /// </summary>
   public class Matrix3
   {
      private double[] data;

      #region Constructors
      /// <summary>
      /// Matrix3 default constructor
      /// </summary>
      public Matrix3()
      {
         data = Identity.Data;
      }

      /// <summary>
      /// Matrix3 constructor
      /// </summary>
      /// <param name="d0"></param>
      /// <param name="d1"></param>
      /// <param name="d2"></param>
      /// <param name="d3"></param>
      /// <param name="d4"></param>
      /// <param name="d5"></param>
      /// <param name="d6"></param>
      /// <param name="d7"></param>
      /// <param name="d8"></param>
      public Matrix3(double d0, double d1, double d2,
                     double d3, double d4, double d5,
                     double d6, double d7, double d8)
      {
         data = new double[9] { d0, d1, d2, d3, d4, d5, d6, d7, d8 };
      }

      /// <summary>
      /// Matrix3
      /// </summary>
      /// <param name="data"></param>
      public Matrix3(double[] data)
      {
         if (data == null)
            throw new ArgumentNullException("data");

         if (data.Length != 9)
            throw new ArgumentOutOfRangeException("data length must be 9");

         this.data = data;
      }

      /// <summary>
      /// Matrix3
      /// </summary>
      /// <param name="mat"></param>
      public Matrix3(Matrix3 mat)
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
         get { return data[r * 3 + c]; }
         set { data[r * 3 + c] = value; }
      }

      /// <summary>
      /// Internal array
      /// </summary>
      public double[] Data
      {
         get { return data; }
      }

      /// <summary>
      /// Identity
      /// </summary>
      public static Matrix3 Identity
      {
         get { return new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1); }
      }

      /// <summary>
      /// Determinant
      /// </summary>
      public double Determinant
      {
         get
         {
            return (data[0] * (data[4] * data[8] - data[5] * data[7])) -
                   (data[1] * (data[3] * data[8] - data[5] * data[6])) +
                   (data[2] * (data[3] * data[7] - data[4] * data[6]));
         }
      }
      #endregion

      #region Operators
      /// <summary>
      /// Equality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator ==(Matrix3 lhs, Matrix3 rhs)
      {
         return lhs.Equals(rhs);
      }

      /// <summary>
      /// Inequality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator !=(Matrix3 lhs, Matrix3 rhs)
      {
         return !lhs.Equals(rhs);
      }

      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Matrix3 operator +(Matrix3 lhs, Matrix3 rhs)
      {
         Matrix3 ret = new Matrix3();
         for (int i = 0; i < 9; i++)
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
      public static Matrix3 operator *(Matrix3 lhs, double val)
      {
         Matrix3 ret = new Matrix3();
         for (int i = 0; i < 9; i++)
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
      public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
      {
         Matrix3 ret = new Matrix3();
         for (int r = 0; r < 3; r++)
         {
            for (int c = 0; c < 3; c++)
            {
               ret[r, c] = lhs.Row(r) | rhs.Col(c);
            }
         }
         return ret;
      }

      /// <summary>
      /// Vector3 multiplication operator
      /// </summary>
      /// <param name="m"></param>
      /// <param name="v"></param>
      /// <returns></returns>
      public static Vector3 operator *(Matrix3 m, Vector3 v)
      {
         return new Vector3(v | m.Row(0), v | m.Row(1), v | m.Row(2));
      }

      /// <summary>
      /// Scalar division operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Matrix3 operator /(Matrix3 lhs, double val)
      {
         Matrix3 ret = new Matrix3();
         for (int i = 0; i < 9; i++)
         {
            ret[i] = lhs[i] / val;
         }
         return ret;
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
         Matrix3 m = (Matrix3)obj;
         for (int i = 0; i < 9; i++)
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

      /// <summary>
      /// Column accessor
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public Vector3 Col(int c)
      {
         return new Vector3(data[3 * 0 + c], data[3 * 1 + c], data[3 * 2 + c]);
      }

      /// <summary>
      /// Row accessor
      /// </summary>
      /// <param name="r"></param>
      /// <returns></returns>
      public Vector3 Row(int r)
      {
         return new Vector3(data[r * 3 + 0], data[r * 3 + 1], data[r * 3 + 2]);
      }

      /// <summary>
      /// Transpose
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix3 Transpose(Matrix3 m)
      {
         return new Matrix3(m[0], m[3], m[6], m[1], m[4], m[7], m[2], m[5], m[8]);
      }

      /// <summary>
      /// Cofactor
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix3 Cofactor(Matrix3 m)
      {
         return new Matrix3(
             (m[4] * m[8] - m[5] * m[7]),
            -(m[3] * m[8] - m[5] * m[6]),
             (m[3] * m[7] - m[4] * m[6]),
            -(m[1] * m[8] - m[2] * m[7]),
             (m[0] * m[8] - m[2] * m[6]),
            -(m[0] * m[7] - m[1] * m[6]),
             (m[1] * m[5] - m[2] * m[4]),
            -(m[0] * m[5] - m[2] * m[3]),
             (m[0] * m[4] - m[1] * m[3])
         );
      }

      /// <summary>
      /// Inverse
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix3 Inverse(Matrix3 m)
      {
         double det = m.Determinant;
         if (det == 0)
            return Matrix3.Identity;
         return Matrix3.Transpose(Matrix3.Cofactor(m)) / det;
      }

      /// <summary>
      /// Submatrix returns the Matrix2 containing
      /// the values not in the given row and column
      /// </summary>
      /// <param name="row"></param>
      /// <param name="col"></param>
      /// <returns></returns>
      public Matrix2 Submatrix(int row, int col)
      {
         Matrix2 ret = new Matrix2();
         int i = 0;
         for (int r = 0; r < 3; r++)
         {
            for (int c = 0; c < 3; c++)
            {
               if (r == row || c == col)
               {
                  continue;
               }
               ret[i++] = this[r, c];
            }
         }
         return ret;
      }
   }
}
