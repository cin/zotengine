using System;

using NUnit.Framework;

using Zot.Core;

namespace Zot.Core.Test
{
   /// <summary>
   /// Vector2Test
   /// </summary>
   [TestFixture]
   public class Vector2Test
   {
      /// <summary>
      /// TestVector2Equality
      /// </summary>
      [Test]
      public void TestVector2Equality()
      {
         Vector2 v1 = new Vector2(1.0, 2.0);
         Vector2 v2 = new Vector2(1.0, 2.0);
         Assert.IsTrue(v1 == v2);
      }

      /// <summary>
      /// TestVector2Assignment
      /// </summary>
      [Test]
      public void TestVector2Assignment()
      {
         Vector2 v1 = new Vector2(1.0, 2.0);
         Vector2 v2 = v1;
         Assert.IsTrue(v2.X == 1.0 && v2.Y == 2.0);
      }

      /// <summary>
      /// TestVector2Inequality
      /// </summary>
      [Test]
      public void TestVector2Inequality()
      {
         Vector2 v1 = new Vector2(1.0, 2.0);
         Vector2 v2 = new Vector2(1.0, 2.0 + (Types.EPSILON * 2));
         Assert.IsTrue(v1 != v2);
      }

      /// <summary>
      /// TestVector2Addition
      /// </summary>
      [Test]
      public void TestVector2Addition()
      {
         Vector2 v1 = new Vector2(1.0, 2.0);
         Vector2 v2 = new Vector2(2.0, 4.0);
         Assert.IsTrue((v1 + v2) == new Vector2(3.0, 6.0));
      }

      /// <summary>
      /// TestVector2Subtraction
      /// </summary>
      [Test]
      public void TestVector2Subtraction()
      {
         Vector2 v1 = new Vector2(1.0, 2.0);
         Vector2 v2 = new Vector2(2.0, 4.0);
         Assert.IsTrue((v2 - v1) == new Vector2(1.0, 2.0));
      }

      /// <summary>
      /// TestVector2Negation
      /// </summary>
      [Test]
      public void TestVector2Negation()
      {
         Vector2 v = new Vector2(1.0, 2.0);
         Assert.IsTrue(-v == new Vector2(-1.0, -2.0));
      }

      /// <summary>
      /// TestVector2MultiplicationScalar
      /// </summary>
      [Test]
      public void TestVector2MultiplicationScalar()
      {
         Vector2 v = new Vector2(1.0, 2.0);
         Assert.IsTrue((v * 2.0) == new Vector2(2.0, 4.0));
      }

      /// <summary>
      /// TestVector2DivisionScalar
      /// </summary>
      [Test]
      public void TestVector2DivisionScalar()
      {
         Vector2 v = new Vector2(2.0, 4.0);
         Assert.IsTrue((v / 2.0) == new Vector2(1.0, 2.0));
      }

      /// <summary>
      /// TestVector2DotProduct
      /// </summary>
      [Test]
      public void TestVector2DotProduct()
      {
         Vector2 v1 = new Vector2(1.0, 2.0);
         Vector2 v2 = new Vector2(2.0, 4.0);
         Assert.IsTrue((v1 | v2) == 10.0);
      }

      /// <summary>
      /// TestVector2Length
      /// </summary>
      [Test]
      public void TestVector2Length()
      {
         Vector2 v = new Vector2(3.0, 4.0);
         Assert.IsTrue(v.Length == 5.0);
      }

      /// <summary>
      /// TestVector2Normalize
      /// </summary>
      [Test]
      public void TestVector2Normalize()
      {
         Vector2 v1 = new Vector2(5.0, 0.0);
         v1.Normalize();
         Assert.IsTrue(v1 == new Vector2(1.0, 0.0));
      }

      /// <summary>
      /// TestVector2Matrix2Multiplication
      /// </summary>
      [Test]
      public void TestVector2Matrix2Multiplication()
      {
         Vector2 v = new Vector2(1, 2);
         Matrix2 m = new Matrix2(1, 2, 3, 4);
         Assert.IsTrue((v * m) == new Vector2(5, 11));
      }

      /// <summary>
      /// TestVector2NullEquality
      /// </summary>
      [Test]
      public void TestVector2NullEquality()
      {
         Vector2 v1 = null;
         Vector2 v2 = new Vector2(1, 2);

         Assert.IsTrue(v1 == null);
         Assert.IsTrue(v2 != null);
      }
   }
}
