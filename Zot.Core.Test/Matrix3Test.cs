using System;

using NUnit.Framework;

using Zot.Core;

namespace Zot.Core.Test
{
   /// <summary>
   /// MatrixTest
   /// </summary>
   [TestFixture]
   public class Matrix3Test
   {
      /// <summary>
      /// TestMatrix3Equality
      /// </summary>
      [Test]
      public void TestMatrix3Equality()
      {
         Matrix3 m1 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Matrix3 m2 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Assert.IsTrue(m1 == m2);
      }

      /// <summary>
      /// TestMatrix3Inequality
      /// </summary>
      [Test]
      public void TestMatrix3Inequality()
      {
         Matrix3 m1 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Matrix3 m2 = new Matrix3(1, 2, 3, 4, 50, 6, 7, 8, 9);
         Assert.IsTrue(m1 != m2);
      }
      
      /// <summary>
      /// TestMatrix3Assignment
      /// </summary>
      [Test]
      public void TestMatrix3Assignment()
      {
         Matrix3 m1 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Matrix3 m2 = m1;
         for (int i = 0; i < 9; i++)
         {
            Assert.IsTrue(m2[i] == m1[i]);
         }
      }

      /// <summary>
      /// TestMatrix3Assignment
      /// </summary>
      [Test]
      public void TestMatrix3Accessors()
      {
         Matrix3 m = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Vector3 row = m.Row(1);
         Assert.IsTrue(row == new Vector3(4, 5, 6));
         Vector3 col = m.Col(2);
         Assert.IsTrue(col == new Vector3(3, 6, 9));
      }

      /// <summary>
      /// TestMatrix3Addition
      /// </summary>
      [Test]
      public void TestMatrix3Addition()
      {
         Matrix3 m1 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Matrix3 m2 = new Matrix3(2, 5, 4, 3, 7, 6, 8, 1, 9);
         Assert.IsTrue((m1 + m2) == new Matrix3(3, 7, 7, 7, 12, 12, 15, 9, 18));
      }

      /// <summary>
      /// TestMatrix3ScalarMultiplication
      /// </summary>
      [Test]
      public void TestMatrix3ScalarMultiplication()
      {
         Matrix3 m = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Assert.IsTrue((m * 3.0) == new Matrix3(3, 6, 9, 12, 15, 18, 21, 24, 27));
      }

      /// <summary>
      /// TestMatrix3ScalarDivision
      /// </summary>
      [Test]
      public void TestMatrix3ScalarDivision()
      {
         Matrix3 m = new Matrix3(2, 4, 6, 8, 10, 12, 14, 16, 18);
         Assert.IsTrue((m / 2.0) == new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9));
      }

      /// <summary>
      /// TestMatrix3MatrixMultiplication
      /// </summary>
      [Test]
      public void TestMatrix3MatrixMultiplication()
      {
         Matrix3 m1 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Matrix3 m2 = new Matrix3(2, 4, 6, 3, 1, -2, 3, 8, -1);
         Assert.IsTrue((m1 * m2) == new Matrix3(17, 30, -1, 41, 69, 8, 65, 108, 17));
         Assert.IsTrue((m2 * m1) == new Matrix3(60, 72, 84, -7, -5, -3, 28, 38, 48));
      }

      /// <summary>
      /// TestMatrix3Transpose
      /// </summary>
      [Test]
      public void TestMatrix3Transpose()
      {
         Matrix3 m = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Assert.IsTrue(Matrix3.Transpose(m) == new Matrix3(1, 4, 7, 2, 5, 8, 3, 6, 9));
      }

      /// <summary>
      /// TestMatrixDeterminant
      /// </summary>
      [Test]
      public void TestMatrixDeterminant()
      {
         Matrix3 m = new Matrix3(2, 4, 6, 3, 1, -2, 3, 8, -1);
         Assert.IsTrue(m.Determinant == 144);
      }

      /// <summary>
      /// TestMatrix3Cofactor
      /// </summary>
      [Test]
      public void TestMatrix3Cofactor()
      {
         Matrix3 m = new Matrix3(1, 2, 3, 0, 4, 5, 1, 0, 6);
         Assert.IsTrue(Matrix3.Cofactor(m) == new Matrix3(24, 5, -4, -12, 3, 2, -2, -5, 4));
      }

      /// <summary>
      /// TestMatrix3Inverse
      /// </summary>
      [Test]
      public void TestMatrix3Inverse()
      {
         Matrix3 m = new Matrix3(1, 2, 3, 0, 4, 5, 1, 0, 6);
         Matrix3 inv = Matrix3.Inverse(m);
         Assert.IsTrue(inv != Matrix3.Identity);
         Assert.IsTrue((m * inv) == Matrix3.Identity);
         Assert.IsTrue((inv * m) == Matrix3.Identity);
      }

      /// <summary>
      /// TestMatrix3Submatrix
      /// </summary>
      [Test]
      public void TestMatrix3Submatrix()
      {
         Matrix3 m1 = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Matrix2 m2 = m1.Submatrix(1, 2);
         Assert.IsTrue(m2 == new Matrix2(1, 2, 7, 8));
      }

      /// <summary>
      /// TestMatrix3Vector3Multiplication
      /// </summary>
      [Test]
      public void TestMatrix3Vector3Multiplication()
      {
         Vector3 v = new Vector3(1, 2, 3);
         Matrix3 m = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Assert.IsTrue((m * v) == new Vector3(14, 32, 50));
      }
   }
}
