using System;

using NUnit.Framework;

using Zot.Core;

namespace Zot.Core.Test
{
   /// <summary>
   /// Vector4Test
   /// </summary>
   [TestFixture]
   public class Vector4Test
   {
      /// <summary>
      /// TestVector4Equality
      /// </summary>
      [Test]
      public void TestVector4Equality()
      {
         Vector4 v1 = new Vector4(1.0, 2.0, 3.0, 4.0);
         Vector4 v2 = new Vector4(1.0, 2.0, 3.0, 4.0);
         Assert.IsTrue(v1 == v2);
      }

      /// <summary>
      /// TestVector4Assignment
      /// </summary>
      [Test]
      public void TestVector4Assignment()
      {
         Vector4 v1 = new Vector4(1.0, 2.0, 3.0, 4.0);
         Vector4 v2 = v1;
         Assert.IsTrue(v2.X == 1.0 && v2.Y == 2.0 && v2.Z == 3.0 && v2.W == 4.0);
      }

      /// <summary>
      /// TestVector4Inequality
      /// </summary>
      [Test]
      public void TestVector4Inequality()
      {
         Vector4 v1 = new Vector4(1.0, 2.0, 3.0, 4.0);
         Vector4 v2 = new Vector4(1.0, 2.0 + (Types.EPSILON * 2), 3.0, 4.0);
         Assert.IsTrue(v1 != v2);
      }

      /// <summary>
      /// TestVector4Addition
      /// </summary>
      [Test]
      public void TestVector4Addition()
      {
         Vector4 v1 = new Vector4(1.0, 2.0, 3.0, 4.0);
         Vector4 v2 = new Vector4(2.0, 4.0, 6.0, 8.0);
         Assert.IsTrue((v1 + v2) == new Vector4(3.0, 6.0, 9.0, 12.0));
      }

      /// <summary>
      /// TestVector4Subtraction
      /// </summary>
      [Test]
      public void TestVector4Subtraction()
      {
         Vector4 v1 = new Vector4(1.0, 2.0, 3.0, 4.0);
         Vector4 v2 = new Vector4(2.0, 4.0, 6.0, 8.0);
         Assert.IsTrue((v2 - v1) == new Vector4(1.0, 2.0, 3.0, 4.0));
      }

      /// <summary>
      /// TestVector4Negation
      /// </summary>
      [Test]
      public void TestVector4Negation()
      {
         Vector4 v = new Vector4(1.0, 2.0, 3.0, 4.0);
         Assert.IsTrue(-v == new Vector4(-1.0, -2.0, -3.0, -4.0));
      }

      /// <summary>
      /// TestVector4MultiplicationScalar
      /// </summary>
      [Test]
      public void TestVector4MultiplicationScalar()
      {
         Vector4 v = new Vector4(1.0, 2.0, 3.0, 4.0);
         Assert.IsTrue((v * 2.0) == new Vector4(2.0, 4.0, 6.0, 8.0));
      }

      /// <summary>
      /// TestVector4DivisionScalar
      /// </summary>
      [Test]
      public void TestVector4DivisionScalar()
      {
         Vector4 v = new Vector4(2.0, 4.0, 6.0, 8.0);
         Assert.IsTrue((v / 2.0) == new Vector4(1.0, 2.0, 3.0, 4.0));
      }

      /// <summary>
      /// TestVector4DotProduct
      /// </summary>
      [Test]
      public void TestVector4DotProduct()
      {
         Vector4 v1 = new Vector4(1.0, 2.0, 3.0, 4.0);
         Vector4 v2 = new Vector4(2.0, -1.0, 4.0, 3.0);
         Assert.IsTrue((v1 | v2) == 24.0);
      }

      /// <summary>
      /// TestVector4Length
      /// </summary>
      [Test]
      public void TestVector4Length()
      {
         Vector4 v = new Vector4(1.0, 2.0, 2.0, 4.0);
         Assert.IsTrue(v.Length == 5.0);
      }

      /// <summary>
      /// TestVector4Normalize
      /// </summary>
      [Test]
      public void TestVector4Normalize()
      {
         Vector4 v = new Vector4(5.0, 0.0, 0.0, 0.0);
         v.Normalize();
         Assert.IsTrue(v == new Vector4(1.0, 0.0, 0.0, 0.0));
      }

      /// <summary>
      /// TestVector4NullEquality
      /// </summary>
      [Test]
      public void TestVector4NullEquality()
      {
         Vector4 v1 = null;
         Vector4 v2 = new Vector4(1, 2, 3, 4);

         Assert.IsTrue(v1 == null);
         Assert.IsTrue(v2 != null);
      }
   }
}
