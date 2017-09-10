using Xunit;

namespace NLib.Tests
{
    public class StructExtensionsTest
    {
        [Fact]
        public void ToStringInvariantWihInt32()
        {
            var i = 100_000;
            Assert.Equal("100000", i.ToStringInvariant());
        }

        [Fact]
        public void ToStringInvariantWihNullableInt32()
        {
            int? i = null;
            Assert.Equal(null, i.ToStringInvariant());
            
            i = 100_000;
            Assert.Equal("100000", i.ToStringInvariant());
        }
    }
}
