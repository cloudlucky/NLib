namespace NLib.Tests.Patterns
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Patterns;

    [TestClass]
    public class LazySingletonTest
    {
        [TestMethod]
        public void CurrentTest1()
        {
            var t = LazySingleton<Test1>.Current;
            LazySingleton<Test1>.Current.Name = "Foo";

            Assert.AreEqual("Foo", t.Name);
            Assert.AreEqual("Foo", LazySingleton<Test1>.Current.Name);
            Assert.IsTrue(ReferenceEquals(t, LazySingleton<Test1>.Current));
            Assert.IsTrue(ReferenceEquals(LazySingleton<Test1>.Current, LazySingleton<Test1>.Current));
        }

        private class Test1
        {
            public string Name { get; set; }
        }
    }
}
