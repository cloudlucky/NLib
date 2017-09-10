namespace NLib.Tests.Extensions
{
    using System.Linq;

    using NLib.Extensions;

    using Xunit;

    public class ArrayExtensionsTest
    {
        [Fact]
        public void SwapValues1()
        {
            var l = new[] { 1, 2, 3, 4, 5, 6 };
            l.SwapValues(2, 3);

            Assert.Equal(new[] { 1, 2, 4, 3, 5, 6 }, l);
        }

        [Fact]
        public void SwapValues2()
        {
            var l = new[] { 1, 2, 3 };
            l.SwapValues(0, 2);
            l.SwapValues(2, 0);
            l.SwapValues(1, 1);

            Assert.Equal(new[] { 1, 2, 3 }, l);
        }
    }
}
