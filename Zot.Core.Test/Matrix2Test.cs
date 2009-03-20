using System;

using NUnit.Framework;

using Zot.Core;

namespace Zot.Core.Test
{
   /// <summary>
   /// MatrixTest
   /// </summary>
   [TestFixture]
   public class Matrix2Test
   {
      /// <summary>
      /// TestMatrix2Equality
      /// </summary>
      [Test]
      public void TestMatrix2Equality()
      {
         Matrix2 m1 = new Matrix2(1, 2, 3, 4);
         Matrix2 m2 = new Matrix2(1, 2, 3, 4);
         Assert.IsTrue(m1 == m2);
      }

      /// <summary>
      /// TestMatrix2Inequality
      /// </summary>
      [Test]
      public void TestMatrix2Inequality()
      {
         Matrix2 m1 = new Matrix2(1, 2, 3, 4);
         Matrix2 m2 = new Matrix2(1, 2, 3, 40);
         Assert.IsTrue(m1 != m2);
      }

      /// <summary>
      /// TestMatrix2Assignment
      /// </summary>
      [Test]
      public void TestMatrix2Assignment()
      {
         Matrix2 m1 = new Matrix2(1, 2, 3, 4);
         Matrix2 m2 = m1;
         for (int i = 0; i < 4; i++)
         {
            Assert.IsTrue(m2[i] == m1[i]);
         }
      }

      /// <summary>
      /// TestMatrix2Assignment
      /// </summary>
      [Test]
      public void TestMatrix2Accessors()
      {
         Matrix2 m = new Matrix2(1, 2, 3, 4);
         Vector2 row = m.Row(1);
         Assert.IsTrue(row == new Vector2(3, 4));
         Vector2 col = m.Col(0);
         Assert.IsTrue(col == new Vector2(1, 3));
      }

      /// <summary>
      /// TestMatrix2Addition
      /// </summary>
      [Test]
      public void TestMatrix2Addition()
      {
         Matrix2 m1 = new Matrix2(1, 2, 3, 4);
         Matrix2 m2 = new Matrix2(2, 5, 4, 3);
         Assert.IsTrue((m1 + m2) == new Matrix2(3, 7, 7, 7));
      }

      /// <summary>
      /// TestMatrix2ScalarMultiplication
      /// </summary>
      [Test]
      public void TestMatrix2ScalarMultiplication()
      {
         Matrix2 m = new Matrix2(1, 2, 3, 4);
         Assert.IsTrue((m * 3.0) == new Matrix2(3, 6, 9, 12));
      }

      /// <summary>
      /// TestMatrix2ScalarDivision
      /// </summary>
      [Test]
      public void TestMatrix2ScalarDivision()
      {
         Matrix2 m = new Matrix2(2, 4, 6, 8);
         Assert.IsTrue((m / 2.0) == new Matrix2(1, 2, 3, 4));
      }

      /// <summary>
      /// TestMatrix2MatrixMultiplication
      /// </summary>
      [Test]
      public void TestMatrix2MatrixMultiplication()
      {
         Matrix2 m1 = new Matrix2(1, 2, 3, 4);
         Matrix2 m2 = new Matrix2(2, 4, 6, 3);
         Assert.IsTrue((m1 * m2) == new Matrix2(14, 10, 30, 24));
         Assert.IsTrue((m2 * m1) == new Matrix2(14, 20, 15, 24));
      }

      /// <summary>
      /// TestMatrix2Transpose
      /// </summary>
      [Test]
      public void TestMatrix2Transpose()
      {
         Matrix2 m = new Matrix2(1, 2, 3, 4);
         Assert.IsTrue(Matrix2.Transpose(m) == new Matrix2(1, 3, 2, 4));
      }

      /// <summary>
      /// TestMatrixDeterminant
      /// </summary>
      [Test]
      public void TestMatrixDeterminant()
      {
         Matrix2 m = new Matrix2(2, 4, 6, 3);
         Assert.IsTrue(m.Determinant == -18);
      }

      /// <summary>
      /// TestMatrix2Inverse
      /// </summary>
      [Test]
      public void TestMatrix2Inverse()
      {
         Matrix2 m = new Matrix2(1, 2, 3, 0);
         Matrix2 inv = Matrix2.Inverse(m);
         Assert.IsTrue(inv != Matrix2.Identity);
      }

      /// <summary>
      /// TestMatrix2Vector2Multiplication
      /// </summary>
      [Test]
      public void TestMatrix2Vector2Multiplication()
      {
         Vector2 v = new Vector2(1, 2);
         Matrix2 m = new Matrix2(1, 2, 3, 4);
         Assert.IsTrue((m * v) == new Vector2(5, 11));
      }
   }
}
