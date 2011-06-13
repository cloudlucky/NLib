namespace NLib.Tests.Patterns
{
    using NLib.Patterns;

    using NUnit.Framework;

    [TestFixture]
    public class LazySingletonTest
    {
        [Test]
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
