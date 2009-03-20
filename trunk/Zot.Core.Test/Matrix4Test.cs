using System;

using NUnit.Framework;

using Zot.Core;

namespace Zot.Core.Test
{
   /// <summary>
   /// MatrixTest
   /// </summary>
   [TestFixture]
   public class Matrix4Test
   {
      /// <summary>
      /// TestMatrix4Equality
      /// </summary>
      [Test]
      public void TestMatrix4Equality()
      {
         Matrix4 m1 = new Matrix4(1, 2, 3, 4,
                                  5, 6, 7, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Matrix4 m2 = new Matrix4(1, 2, 3, 4,
                                  5, 6, 7, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Assert.IsTrue(m1 == m2);
      }

      /// <summary>
      /// TestMatrix4Inequality
      /// </summary>
      [Test]
      public void TestMatrix4Inequality()
      {
         Matrix4 m1 = new Matrix4(1, 2, 3, 4,
                                  5, 6, 7, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Matrix4 m2 = new Matrix4(1, 2, 3, 4,
                                  5, 6, 70, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Assert.IsTrue(m1 != m2);
      }

      /// <summary>
      /// TestMatrix4Assignment
      /// </summary>
      [Test]
      public void TestMatrix4Assignment()
      {
         Matrix4 m1 = new Matrix4(1, 2, 3, 4,
                                  5, 6, 7, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Matrix4 m2 = m1;
         for (int i = 0; i < 16; i++)
         {
            Assert.IsTrue(m2[i] == m1[i]);
         }
      }

      /// <summary>
      /// TestMatrix4Assignment
      /// </summary>
      [Test]
      public void TestMatrix4Accessors()
      {
         Matrix4 m = new Matrix4(1, 2, 3, 4,
                                  5, 6, 7, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Vector4 row = m.Row(3);
         Assert.IsTrue(row == new Vector4(13, 14, 15, 16));
         Vector4 col = m.Col(2);
         Assert.IsTrue(col == new Vector4(3, 7, 11, 15));
      }

      /// <summary>
      /// TestMatrix4Addition
      /// </summary>
      [Test]
      public void TestMatrix4Addition()
      {
         Matrix4 m1 = new Matrix4(1, 2, 3, 4,
                                  5, 6, 7, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Matrix4 m2 = new Matrix4(2, 5, 4, 3,
                                  7, 6, 8, 1,
                                  9, 2, -3, 0,
                                  4, -2, 1, 2);
         Assert.IsTrue((m1 + m2) == new Matrix4(3, 7, 7, 7,
                                                12, 12, 15, 9,
                                                18, 12, 8, 12,
                                                17, 12, 16, 18));
      }

      /// <summary>
      /// TestMatrix4ScalarMultiplication
      /// </summary>
      [Test]
      public void TestMatrix4ScalarMultiplication()
      {
         Matrix4 m = new Matrix4(1, 2, 3, 4,
                                 5, 6, 7, 8,
                                 9, 10, 11, 12,
                                 13, 14, 15, 16);
         Assert.IsTrue((m * 3.0) == new Matrix4(3, 6, 9, 12,
                                                15, 18, 21, 24,
                                                27, 30, 33, 36,
                                                39, 42, 45, 48));
      }

      /// <summary>
      /// TestMatrix4ScalarDivision
      /// </summary>
      [Test]
      public void TestMatrix4ScalarDivision()
      {
         Matrix4 m = new Matrix4(2, 4, 6, 8,
                                 10, 12, 14, 16,
                                 18, 20, 22, 24,
                                 26, 28, 30, 32);
         Assert.IsTrue((m / 2.0) == new Matrix4(1, 2, 3, 4,
                                                5, 6, 7, 8,
                                                9, 10, 11, 12,
                                                13, 14, 15, 16));
      }

      /// <summary>
      /// TestMatrix4MatrixMultiplication
      /// </summary>
      [Test]
      public void TestMatrix4MatrixMultiplication()
      {
         Matrix4 m1 = new Matrix4(1, 0, 2, 3,
                                  4, 1, 2, 2,
                                  -1, 3, 4, -2,
                                  1, 2, 0, 3);
         Matrix4 m2 = new Matrix4(1, 2, 4, 3,
                                  2, 2, 0, 1,
                                  -1, 3, -2, 4,
                                  0, 4, -3, 1);
         Assert.IsTrue((m1 * m2) == new Matrix4(-1, 20, -9, 14,
                                                4, 24, 6, 23,
                                                1, 8, -6, 14,
                                                5, 18, -5, 8));
         Assert.IsTrue((m2 * m1) == new Matrix4(8, 20, 22, 8,
                                                11, 4, 8, 13,
                                                17, 5, -4, 19,
                                                20, -3, -4, 17));
      }

      /// <summary>
      /// TestMatrix4Transpose
      /// </summary>
      [Test]
      public void TestMatrix4Transpose()
      {
         Matrix4 m = new Matrix4(1, 2, 3, 4,
                                 5, 6, 7, 8,
                                 9, 10, 11, 12,
                                 13, 14, 15, 16);
         Assert.IsTrue(Matrix4.Transpose(m) == new Matrix4(1, 5, 9, 13,
                                                           2, 6, 10, 14,
                                                           3, 7, 11, 15,
                                                           4, 8, 12, 16));
      }

      /// <summary>
      /// TestMatrixDeterminant
      /// </summary>
      [Test]
      public void TestMatrixDeterminant()
      {
         Matrix4 m = new Matrix4(1, 0, 3, -2,
                                 4, 2, -1, 3,
                                 0, 2, 1, -1,
                                 3, 1, 2, 2);
         Assert.IsTrue(m.Determinant == 58);
      }

      /// <summary>
      /// TestMatrix4Cofactor
      /// </summary>
      [Test]
      public void TestMatrix4Cofactor()
      {
         Matrix4 m = new Matrix4(1, 0, 3, -2,
                                 4, 2, -1, 3,
                                 0, 2, 1, -1,
                                 3, 1, 2, 2);
         Assert.IsTrue(Matrix4.Cofactor(m) == new Matrix4(22, -10, -4, -24,
                                                          21, 1, -17, -15,
                                                          -13, 27, 5, 1,
                                                          -16, 2, 24, 28));
      }

      /// <summary>
      /// TestMatrix4Inverse
      /// </summary>
      [Test]
      public void TestMatrix4Inverse()
      {
         Matrix4 m = new Matrix4(1, 0, 3, -2,
                                 4, 2, -1, 3,
                                 0, 2, 1, -1,
                                 3, 1, 2, 2);
         Matrix4 inv = Matrix4.Inverse(m);
         Assert.IsTrue(inv != Matrix4.Identity);
         Assert.IsTrue((m * inv) == Matrix4.Identity);
         Assert.IsTrue((inv * m) == Matrix4.Identity);
      }

      /// <summary>
      /// TestMatrix4Submatrix
      /// </summary>
      [Test]
      public void TestMatrix4Submatrix()
      {
         Matrix4 m1 = new Matrix4(1, 2, 3, 4,
                                  5, 6, 7, 8,
                                  9, 10, 11, 12,
                                  13, 14, 15, 16);
         Matrix3 m2 = m1.Submatrix(1, 2);
         Assert.IsTrue(m2 == new Matrix3(1, 2, 4, 9, 10, 12, 13, 14, 16));
      }

      /// <summary>
      /// TestMatrix4Vector3Multiplication
      /// </summary>
      [Test]
      public void TestMatrix4Vector3Multiplication()
      {
         Vector4 v = new Vector4(1, 2, 3, 4);
         Matrix4 m = new Matrix4(1, 2, 3, 4,
                                 5, 6, 7, 8,
                                 9, 10, 11, 12,
                                 13, 14, 15, 16);
         Assert.IsTrue((m * v) == new Vector4(30, 70, 110, 150));
      }
   }
}
