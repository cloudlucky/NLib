namespace NLib.Tests.Collections.Generic.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic.Extensions;

    using Xunit;

    public class ListExtensionsTest
    {
        [Fact]
        public void SwapValues1()
        {
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };
            l.SwapValues(2, 3);

            Assert.Equal(new[] { 1, 2, 4, 3, 5, 6 }, l);
        }
    }
}
