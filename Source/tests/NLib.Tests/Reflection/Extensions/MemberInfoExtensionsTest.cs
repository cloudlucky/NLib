namespace NLib.Tests.Reflection.Extensions
{
    using System;
    using System.Linq;

    using NLib.Reflection.Extensions;

    using Xunit;

    public class MemberInfoExtensionsTest
    {
        [Fact]
        public void GetCustomAttribute1()
        {
            var c = new CustomAttributesTest();

            Assert.Null(c.GetType().GetProperty("P1").GetCustomAttribute<CustomAttributeAttribute>(false));
        }

        [Fact]
        public void GetCustomAttribute2()
        {
            var c = new CustomAttributesTest();

            Assert.NotNull(c.GetType().GetProperty("P2").GetCustomAttribute<CustomAttributeAttribute>(false));
        }

        [Fact]
        public void GetCustomAttributes1()
        {
            var c = new CustomAttributesTest();

            Assert.Equal(0, c.GetType().GetProperty("P1").GetCustomAttributes<CustomAttributeAttribute>(false).Length);
        }

        [Fact]
        public void GetCustomAttributes2()
        {
            var c = new CustomAttributesTest();

            Assert.Equal(1, c.GetType().GetProperty("P2").GetCustomAttributes<CustomAttributeAttribute>(false).Length);
        }

        [Fact]
        public void GetCustomAttributes3()
        {
            var c = new CustomAttributesTest();
            var x = c.GetType().GetProperty("P3").GetCustomAttributes<CustomAttributeAttribute>(false);

            Assert.Equal(1, x.Length);
            Assert.Equal("Foo", x.ElementAt(0).Name);
        }

        [Fact]
        public void GetCustomAttributes4()
        {
            var c = new CustomAttributesTest();
            var x = c.GetType().GetProperty("P4").GetCustomAttributes<CustomAttributeAttribute>(false);

            Assert.Equal(2, x.Length);
        }

        [Fact]
        public void GetMemberTypeTest1()
        {
            var c = new CustomAttributesTest();

            Assert.Equal(typeof(string), c.GetType().GetProperty("P1").GetMemberType());
            Assert.Equal(typeof(int), c.GetType().GetProperty("P5").GetMemberType());
            Assert.Equal(typeof(CustomAttributesTest), c.GetType().GetProperty("P6").GetMemberType());
            Assert.Equal(typeof(string), c.GetType().GetField("M1").GetMemberType());
        }

        [Fact]
        public void GetMemberTypeTest2()
        {
            var c = new CustomAttributesTest();

            Assert.Throws(typeof(NotSupportedException), () => c.GetType().GetMemberType());
        }

        public class CustomAttributesTest
        {
            public string M1;

            public string P1 { get; set; }

            [CustomAttribute]
            public string P2 { get; set; }

            [CustomAttribute(Name = "Foo")]
            public string P3 { get; set; }

            [CustomAttribute]
            [CustomAttribute(Name = "Foo")]
            public string P4 { get; set; }

            public int P5 { get; set; }

            public CustomAttributesTest P6 { get; set; }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public sealed class CustomAttributeAttribute : Attribute
        {
            public string Name { get; set; }
        }
    }
}
