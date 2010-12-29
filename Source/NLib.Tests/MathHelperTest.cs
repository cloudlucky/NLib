namespace NLib.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class MathHelperTest
    {
        [Test]
        public void Factorial1()
        {
            Assert.AreEqual(1, MathHelper.Factorial(0));
            Assert.AreEqual(1, MathHelper.Factorial(1));
            Assert.AreEqual(2, MathHelper.Factorial(2));
            Assert.AreEqual(6, MathHelper.Factorial(3));
            Assert.AreEqual(24, MathHelper.Factorial(4));
            Assert.AreEqual(120, MathHelper.Factorial(5));
            Assert.AreEqual(720, MathHelper.Factorial(6));
            Assert.AreEqual(5040, MathHelper.Factorial(7));
            Assert.AreEqual(40320, MathHelper.Factorial(8));
            Assert.AreEqual(362880, MathHelper.Factorial(9));
            Assert.AreEqual(3628800, MathHelper.Factorial(10));
            Assert.AreEqual(1307674368000, MathHelper.Factorial(15));
            Assert.AreEqual(2432902008176640000, MathHelper.Factorial(20));
            Assert.AreEqual(1.5511210043330986e25, MathHelper.Factorial(25));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Factorial2()
        {
            MathHelper.Factorial(-2);
        }

        [Test]
        public void GreatCommonDivisor1()
        {
            Assert.AreEqual(3, MathHelper.GreatCommonDivisor(3, 6));
            Assert.AreEqual(-3, MathHelper.GreatCommonDivisor(-3, -6));
            Assert.AreEqual(1, MathHelper.GreatCommonDivisor(3, 4));
        }

        [Test]
        public void LeastCommonMultiple1()
        {
            Assert.AreEqual(6, MathHelper.LeastCommonMultiple(3, 6));
            Assert.AreEqual(-3, MathHelper.GreatCommonDivisor(-3, -6));
            Assert.AreEqual(12, MathHelper.LeastCommonMultiple(3, 4));
        }
    }
}
