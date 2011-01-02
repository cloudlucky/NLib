namespace NLib.Tests.Reflection.Extensions
{
    using System;
    using System.Linq;

    using NLib;
    using NLib.Extensions;
    using NLib.Reflection.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class MemberInfoExtensionTest
    {
        [Test]
        public void GetCustomAttributes1()
        {
            var c = new CustomAttributesTest();

            Assert.AreEqual(0, c.GetType().GetProperty("P1").GetCustomAttributes<CustomAttributeAttribute>(false).Length);
        }

        [Test]
        public void GetCustomAttributes2()
        {
            var c = new CustomAttributesTest();

            Assert.AreEqual(1, c.GetType().GetProperty("P2").GetCustomAttributes<CustomAttributeAttribute>(false).Length);
        }

        [Test]
        public void GetCustomAttributes3()
        {
            var c = new CustomAttributesTest();
            var x = c.GetType().GetProperty("P3").GetCustomAttributes<CustomAttributeAttribute>(false);

            Assert.AreEqual(1, x.Length);
            Assert.AreEqual("Foo", x.ElementAt(0).Name);
        }

        [Test]
        public void GetCustomAttributes4()
        {
            var c = new CustomAttributesTest();
            var x = c.GetType().GetProperty("P4").GetCustomAttributes<CustomAttributeAttribute>(false);

            Assert.AreEqual(2, x.Length);
        }

        public class CustomAttributesTest
        {
            public string P1 { get; set; }

            [CustomAttribute]
            public string P2 { get; set; }

            [CustomAttribute(Name = "Foo")]
            public string P3 { get; set; }

            [CustomAttribute]
            [CustomAttribute(Name = "Foo")]
            public string P4 { get; set; }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public sealed class CustomAttributeAttribute : Attribute
        {
            public string Name { get; set; }
        }
    }
}
