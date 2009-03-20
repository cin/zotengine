using System;

namespace Zot.Core
{
   /// <summary>
   /// Matrix4 describes a 4x4 matrix of doubles
   /// </summary>
   public class Matrix4
   {
      /// <summary>
      /// data
      /// </summary>
      protected double[] data;

      #region Constructors
      /// <summary>
      /// Matrix4 default constructor
      /// </summary>
      public Matrix4()
      {
         data = Identity.Data;
      }

      /// <summary>
      /// Matrix4 constructor
      /// </summary>
      /// <param name="d0"></param><param name="d1"></param><param name="d2"></param><param name="d3"></param>
      /// <param name="d4"></param><param name="d5"></param><param name="d6"></param><param name="d7"></param>
      /// <param name="d8"></param><param name="d9"></param><param name="d10"></param><param name="d11"></param>
      /// <param name="d12"></param><param name="d13"></param><param name="d14"></param><param name="d15"></param>
      public Matrix4(double d0, double d1, double d2, double d3,
                     double d4, double d5, double d6, double d7,
                     double d8, double d9, double d10, double d11,
                     double d12, double d13, double d14, double d15)
      {
         data = new double[16] { d0, d1, d2, d3,
                            d4, d5, d6, d7,
                            d8, d9, d10, d11,
                            d12, d13, d14, d15 };
      }

      /// <summary>
      /// Matrix4
      /// </summary>
      /// <param name="data"></param>
      public Matrix4(double[] data)
      {
         if (data == null)
            throw new ArgumentNullException("data");

         if (data.Length != 16)
            throw new ArgumentOutOfRangeException("data length must be 16");

         this.data = data;
      }

      /// <summary>
      /// Matrix4
      /// </summary>
      /// <param name="mat"></param>
      public Matrix4(Matrix4 mat)
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
         get { return data[r * 4 + c]; }
         set { data[r * 4 + c] = value; }
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
      public static Matrix4 Identity
      {
         get
         {
            return new Matrix4(1.0, 0.0, 0.0, 0.0,
                                  0.0, 1.0, 0.0, 0.0,
                                  0.0, 0.0, 1.0, 0.0,
                                  0.0, 0.0, 0.0, 1.0); }
      }

      /// <summary>
      /// Determinant
      /// </summary>
      public double Determinant
      {
         get
         {
            return (data[0] * Submatrix(0, 0).Determinant) -
                   (data[1] * Submatrix(0, 1).Determinant) +
                   (data[2] * Submatrix(0, 2).Determinant) -
                   (data[3] * Submatrix(0, 3).Determinant);
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
      public static bool operator ==(Matrix4 lhs, Matrix4 rhs)
      {
         return lhs.Equals(rhs);
      }

      /// <summary>
      /// Inequality operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static bool operator !=(Matrix4 lhs, Matrix4 rhs)
      {
         return !lhs.Equals(rhs);
      }

      /// <summary>
      /// Addition operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="rhs"></param>
      /// <returns></returns>
      public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
      {
         Matrix4 ret = new Matrix4();
         for (int i = 0; i < 16; i++)
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
      public static Matrix4 operator *(Matrix4 lhs, double val)
      {
         Matrix4 ret = new Matrix4();
         for (int i = 0; i < 16; i++)
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
      public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
      {
         Matrix4 ret = new Matrix4();
         for (int r = 0; r < 4; r++)
         {
            for (int c = 0; c < 4; c++)
            {
               ret[r, c] = lhs.Row(r) | rhs.Col(c);
            }
         }
         return ret;
      }

      /// <summary>
      /// Vector4 multiplication operator
      /// </summary>
      /// <param name="m"></param>
      /// <param name="v"></param>
      /// <returns></returns>
      public static Vector4 operator *(Matrix4 m, Vector4 v)
      {
         return new Vector4(v | m.Row(0), v | m.Row(1), v | m.Row(2), v | m.Row(3));
      }

      /// <summary>
      /// Vector3 multiplication operator
      /// </summary>
      /// <param name="m"></param>
      /// <param name="v"></param>
      /// <returns></returns>
      public static Vector3 operator *(Matrix4 m, Vector3 v)
      {
         return new Vector3(v | m.Row3(0), v | m.Row3(1), v | m.Row3(2));
      }

      /// <summary>
      /// Scalar division operator
      /// </summary>
      /// <param name="lhs"></param>
      /// <param name="val"></param>
      /// <returns></returns>
      public static Matrix4 operator /(Matrix4 lhs, double val)
      {
         Matrix4 ret = new Matrix4();
         for (int i = 0; i < 16; i++)
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
         Matrix4 m = (Matrix4)obj;
         for (int i = 0; i < 16; i++)
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

      /// <summary>
      /// ToString
      /// </summary>
      /// <returns></returns>
      public override string ToString()
      {
         return String.Format(
            "{0} {1} {2} {3}\n" +
            "{4} {5} {6} {7}\n" +
            "{8} {9} {10} {11}\n" +
            "{12} {13} {14} {15}",
            data[0], data[1], data[2], data[3],
            data[4], data[5], data[6], data[7],
            data[8], data[9], data[10], data[11],
            data[12], data[13], data[14], data[15]);
      }
      #endregion

      /// <summary>
      /// Column accessor
      /// </summary>
      /// <param name="c"></param>
      /// <returns></returns>
      public Vector4 Col(int c)
      {
         return new Vector4(data[4 * 0 + c], data[4 * 1 + c], data[4 * 2 + c], data[4 * 3 + c]);
      }

      /// <summary>
      /// Row accessor
      /// </summary>
      /// <param name="r"></param>
      /// <returns></returns>
      public Vector4 Row(int r)
      {
         return new Vector4(data[r * 4 + 0], data[r * 4 + 1], data[r * 4 + 2], data[r * 4 + 3]);
      }

      /// <summary>
      /// Row3
      /// </summary>
      /// <param name="r"></param>
      /// <returns></returns>
      public Vector3 Row3(int r)
      {
         return new Vector3(data[r * 4 + 0], data[r * 4 + 1], data[r * 4 + 2]);
      }

      /// <summary>
      /// Transpose
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix4 Transpose(Matrix4 m)
      {
         return new Matrix4(m[0], m[4], m[8], m[12],
                               m[1], m[5], m[9], m[13],
                               m[2], m[6], m[10], m[14],
                               m[3], m[7], m[11], m[15]);
      }

      /// <summary>
      /// Cofactor
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix4 Cofactor(Matrix4 m)
      {
         return new Matrix4(
            m.Submatrix(0, 0).Determinant,
            -m.Submatrix(0, 1).Determinant,
            m.Submatrix(0, 2).Determinant,
            -m.Submatrix(0, 3).Determinant,
            -m.Submatrix(1, 0).Determinant,
            m.Submatrix(1, 1).Determinant,
            -m.Submatrix(1, 2).Determinant,
            m.Submatrix(1, 3).Determinant,
            m.Submatrix(2, 0).Determinant,
            -m.Submatrix(2, 1).Determinant,
            m.Submatrix(2, 2).Determinant,
            -m.Submatrix(2, 3).Determinant,
            -m.Submatrix(3, 0).Determinant,
            m.Submatrix(3, 1).Determinant,
            -m.Submatrix(3, 2).Determinant,
            m.Submatrix(3, 3).Determinant);
      }

      /// <summary>
      /// Inverse
      /// </summary>
      /// <param name="m"></param>
      /// <returns></returns>
      public static Matrix4 Inverse(Matrix4 m)
      {
         double det = m.Determinant;
         if (det == 0)
            return Matrix4.Identity;
         return Matrix4.Transpose(Matrix4.Cofactor(m)) / det;
      }

      /// <summary>
      /// Submatrix returns the Matrix2 containing
      /// the values not in the given row and column
      /// </summary>
      /// <param name="row"></param>
      /// <param name="col"></param>
      /// <returns></returns>
      public Matrix3 Submatrix(int row, int col)
      {
         Matrix3 ret = new Matrix3();
         int i = 0;
         for (int r = 0; r < 4; r++)
         {
            for (int c = 0; c < 4; c++)
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

      /// <summary>
      /// This method returns a 4x4 rotation matrix
      /// </summary>
      /// <param name="axis"></param>
      /// <param name="angle"></param>
      /// <returns></returns>
      public void Rotation(Vector3 axis, double angle)
      {
         double c = Math.Cos((double)angle);
         double s = Math.Sin((double)angle);
         double t = 1.0 - c;
         data[0] = t * axis.X * axis.X + c;
         data[1] = t * axis.X * axis.Y - s * axis.Z;
         data[2] = t * axis.X * axis.Z + s * axis.Y;
         data[4] = t * axis.X * axis.Y + s * axis.Z;
         data[5] = t * axis.Y * axis.Y + c;
         data[6] = t * axis.Y * axis.Z - s * axis.X;
         data[8] = t * axis.X * axis.Z - s * axis.Y;
         data[9] = t * axis.Y * axis.Z + s * axis.X;
         data[10] = t * axis.Z * axis.Z + c;
      }

      /// <summary>
      /// This method adds a translation vector to the matrix
      /// </summary>
      /// <param name="axis"></param>
      public void Translation(Vector3 axis)
      {
         data[12] = axis.X;
         data[13] = axis.Y;
         data[14] = axis.Z;
      }
   }
}
