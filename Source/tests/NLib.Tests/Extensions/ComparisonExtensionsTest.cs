namespace NLib.Tests.Extensions
{
    using System;

    using NLib.Extensions;

    using Xunit;
    public class ComparisonExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            Comparison<int> ec = (i1, i2) => i1 + i2;

            Assert.Equal(5, ec(2, 3));
        }

        [Fact]
        public void Test2()
        {
            Comparison<int> ec = (i1, i2) => i1 + i2;
            var t = ec.ToComparer();

            Assert.Equal(3, t.Compare(1, 2));
        }

        [Fact]
        public void Test3()
        {
            Comparison<int> ec = (i1, i2) => i1 - i2;
            var t = ec.ToComparer();

            Assert.Equal(-1, t.Compare(0, 1));

        }
    }
}
