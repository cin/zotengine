using System;

using NUnit.Framework;

using Zot.Core;

namespace Zot.Core.Test
{
   /// <summary>
   /// Vector3Test
   /// </summary>
   [TestFixture]
   public class Vector3Test
   {
      /// <summary>
      /// TestVector3Equality
      /// </summary>
      [Test]
      public void TestVector3Equality()
      {
         Vector3 v1 = new Vector3(1.0, 2.0, 3.0);
         Vector3 v2 = new Vector3(1.0, 2.0, 3.0);
         Assert.IsTrue(v1 == v2);
      }

      /// <summary>
      /// TestVector3Assignment
      /// </summary>
      [Test]
      public void TestVector3Assignment()
      {
         Vector3 v1 = new Vector3(1.0, 2.0, 3.0);
         Vector3 v2 = v1;
         Assert.IsTrue(v2.X == 1.0 && v2.Y == 2.0 && v2.Z == 3.0);
      }

      /// <summary>
      /// TestVector3Inequality
      /// </summary>
      [Test]
      public void TestVector3Inequality()
      {
         Vector3 v1 = new Vector3(1.0, 2.0, 3.0);
         Vector3 v2 = new Vector3(1.0, 2.0 + (Types.EPSILON * 2), 3.0);
         Assert.IsTrue(v1 != v2);
      }

      /// <summary>
      /// TestVector3Addition
      /// </summary>
      [Test]
      public void TestVector3Addition()
      {
         Vector3 v1 = new Vector3(1.0, 2.0, 3.0);
         Vector3 v2 = new Vector3(2.0, 4.0, 6.0);
         Assert.IsTrue((v1 + v2) == new Vector3(3.0, 6.0, 9.0));
      }

      /// <summary>
      /// TestVector3Subtraction
      /// </summary>
      [Test]
      public void TestVector3Subtraction()
      {
         Vector3 v1 = new Vector3(1.0, 2.0, 3.0);
         Vector3 v2 = new Vector3(2.0, 4.0, 6.0);
         Assert.IsTrue((v2 - v1) == new Vector3(1.0, 2.0, 3.0));
      }

      /// <summary>
      /// TestVector3Negation
      /// </summary>
      [Test]
      public void TestVector3Negation()
      {
         Vector3 v = new Vector3(1.0, 2.0, 3.0);
         Assert.IsTrue(-v == new Vector3(-1.0, -2.0, -3.0));
      }

      /// <summary>
      /// TestVector3MultiplicationScalar
      /// </summary>
      [Test]
      public void TestVector3MultiplicationScalar()
      {
         Vector3 v = new Vector3(1.0, 2.0, 3.0);
         Assert.IsTrue((v * 2.0) == new Vector3(2.0, 4.0, 6.0));
      }

      /// <summary>
      /// TestVector3DivisionScalar
      /// </summary>
      [Test]
      public void TestVector3DivisionScalar()
      {
         Vector3 v = new Vector3(2.0, 4.0, 6.0);
         Assert.IsTrue((v / 2.0) == new Vector3(1.0, 2.0, 3.0));
      }

      /// <summary>
      /// TestVector3DotProduct
      /// </summary>
      [Test]
      public void TestVector3DotProduct()
      {
         Vector3 v1 = new Vector3(1.0, 2.0, 3.0);
         Vector3 v2 = new Vector3(2.0, -1.0, 4.0);
         Assert.IsTrue((v1 | v2) == 12.0);
      }

      /// <summary>
      /// TestVector3CrossProduct
      /// </summary>
      [Test]
      public void TestVector3CrossProduct()
      {
         Vector3 v1 = new Vector3(1.0, 2.0, 3.0);
         Vector3 v2 = new Vector3(2.0, -3.0, 4.0);
         Assert.IsTrue((v1 ^ v2) == new Vector3(17.0, 2.0, -7.0));
      }

      /// <summary>
      /// TestVector3Length
      /// </summary>
      [Test]
      public void TestVector3Length()
      {
         Vector3 v = new Vector3(1.0, 2.0, 2.0);
         Assert.IsTrue(v.Length == 3.0);
      }

      /// <summary>
      /// TestVector3Normalize
      /// </summary>
      [Test]
      public void TestVector3Normalize()
      {
         Vector3 v = new Vector3(5.0, 0.0, 0.0);
         v.Normalize();
         Assert.IsTrue(v == new Vector3(1.0, 0.0, 0.0));
      }

      /// <summary>
      /// TestVector3Matrix3Multiplication
      /// </summary>
      [Test]
      public void TestVector3Matrix3Multiplication()
      {
         Vector3 v = new Vector3(1, 2, 3);
         Matrix3 m = new Matrix3(1, 2, 3, 4, 5, 6, 7, 8, 9);
         Assert.IsTrue((v * m) == new Vector3(14, 32, 50));
      }

      /// <summary>
      /// TestVectorNullEquality
      /// </summary>
      [Test]
      public void TestVectorNullEquality()
      {
         Vector3 v1 = null;
         Vector3 v2 = new Vector3(1, 2, 3);

         Assert.IsTrue(v1 == null);
         Assert.IsTrue(v2 != null);
      }
   }
}
