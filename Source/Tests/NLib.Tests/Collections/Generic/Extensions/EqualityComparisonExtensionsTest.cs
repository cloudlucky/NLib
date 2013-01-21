namespace NLib.Tests.Collections.Generic.Extensions
{
    using NLib.Collections.Generic.Extensions;

    using Xunit;

    public class EqualityComparisonExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            EqualityComparison<int> ec = (i1, i2) => i1 == i2;

            Assert.True(ec(2, 2));
        }

        [Fact]
        public void Test2()
        {
            EqualityComparison<int> ec = (i1, i2) => i1 == i2;
            var t = ec.ToEqualityComparer();

            Assert.True(t.Equals(1, 1));
            Assert.Equal(1, t.GetHashCode(1));
        }
    }
}
