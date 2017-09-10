namespace NLib.Tests
{
    using System;

    using Xunit;

    public class MathHelperTest
    {
        [Fact]
        public void Factorial1()
        {
            Assert.Equal(1, MathHelper.Factorial(0));
            Assert.Equal(1, MathHelper.Factorial(1));
            Assert.Equal(2, MathHelper.Factorial(2));
            Assert.Equal(6, MathHelper.Factorial(3));
            Assert.Equal(24, MathHelper.Factorial(4));
            Assert.Equal(120, MathHelper.Factorial(5));
            Assert.Equal(720, MathHelper.Factorial(6));
            Assert.Equal(5040, MathHelper.Factorial(7));
            Assert.Equal(40320, MathHelper.Factorial(8));
            Assert.Equal(362880, MathHelper.Factorial(9));
            Assert.Equal(3628800, MathHelper.Factorial(10));
            Assert.Equal(1307674368000, MathHelper.Factorial(15));
            Assert.Equal(2432902008176640000, MathHelper.Factorial(20));
            Assert.Equal(1.5511210043330986e25, MathHelper.Factorial(25));
        }

        [Fact]
        public void Factorial2()
        {
            Assert.Throws(typeof(ArgumentException), () => MathHelper.Factorial(-2));
        }

        [Fact]
        public void GreatCommonDivisor1()
        {
            Assert.Equal(3, MathHelper.GreatCommonDivisor(3, 6));
            Assert.Equal(-3, MathHelper.GreatCommonDivisor(-3, -6));
            Assert.Equal(1, MathHelper.GreatCommonDivisor(3, 4));
        }

        [Fact]
        public void LeastCommonMultiple1()
        {
            Assert.Equal(6, MathHelper.LeastCommonMultiple(3, 6));
            Assert.Equal(-6, MathHelper.LeastCommonMultiple(-3, -6));
            Assert.Equal(12, MathHelper.LeastCommonMultiple(3, 4));
        }

        [Fact]
        public void LeastCommonMultiple2()
        {
            Assert.Equal(0, MathHelper.LeastCommonMultiple(0, 6));
            Assert.Equal(0, MathHelper.LeastCommonMultiple(-3, 0));
        }
    }
}
