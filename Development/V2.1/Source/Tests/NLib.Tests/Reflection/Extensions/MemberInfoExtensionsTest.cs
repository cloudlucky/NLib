namespace NLib.Tests.Reflection.Extensions
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Reflection.Extensions;

    [TestClass]
    public class MemberInfoExtensionsTest
    {
        [TestMethod]
        public void GetCustomAttribute1()
        {
            var c = new CustomAttributesTest();

            Assert.IsNull(c.GetType().GetProperty("P1").GetCustomAttribute<CustomAttributeAttribute>(false));
        }

        [TestMethod]
        public void GetCustomAttribute2()
        {
            var c = new CustomAttributesTest();

            Assert.IsNotNull(c.GetType().GetProperty("P2").GetCustomAttribute<CustomAttributeAttribute>(false));
        }

        [TestMethod]
        public void GetCustomAttributes1()
        {
            var c = new CustomAttributesTest();

            Assert.AreEqual(0, c.GetType().GetProperty("P1").GetCustomAttributes<CustomAttributeAttribute>(false).Length);
        }

        [TestMethod]
        public void GetCustomAttributes2()
        {
            var c = new CustomAttributesTest();

            Assert.AreEqual(1, c.GetType().GetProperty("P2").GetCustomAttributes<CustomAttributeAttribute>(false).Length);
        }

        [TestMethod]
        public void GetCustomAttributes3()
        {
            var c = new CustomAttributesTest();
            var x = c.GetType().GetProperty("P3").GetCustomAttributes<CustomAttributeAttribute>(false);

            Assert.AreEqual(1, x.Length);
            Assert.AreEqual("Foo", x.ElementAt(0).Name);
        }

        [TestMethod]
        public void GetCustomAttributes4()
        {
            var c = new CustomAttributesTest();
            var x = c.GetType().GetProperty("P4").GetCustomAttributes<CustomAttributeAttribute>(false);

            Assert.AreEqual(2, x.Length);
        }

        [TestMethod]
        public void GetMemberTypeTest1()
        {
            var c = new CustomAttributesTest();

            Assert.AreEqual(typeof(string), c.GetType().GetProperty("P1").GetMemberType());
            Assert.AreEqual(typeof(int), c.GetType().GetProperty("P5").GetMemberType());
            Assert.AreEqual(typeof(CustomAttributesTest), c.GetType().GetProperty("P6").GetMemberType());
            Assert.AreEqual(typeof(string), c.GetType().GetField("M1").GetMemberType());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetMemberTypeTest2()
        {
            var c = new CustomAttributesTest();

            var f = c.GetType().GetMemberType();

            Assert.Fail();
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
