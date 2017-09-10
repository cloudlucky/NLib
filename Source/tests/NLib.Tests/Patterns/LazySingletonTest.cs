namespace NLib.Tests.Patterns
{
    using NLib.Patterns;

    using Xunit;

    public class LazySingletonTest
    {
        [Fact]
        public void CurrentTest1()
        {
            var t = LazySingleton<Test1>.Current;
            LazySingleton<Test1>.Current.Name = "Foo";

            Assert.Equal("Foo", t.Name);
            Assert.Equal("Foo", LazySingleton<Test1>.Current.Name);
            Assert.True(ReferenceEquals(t, LazySingleton<Test1>.Current));
            Assert.True(ReferenceEquals(LazySingleton<Test1>.Current, LazySingleton<Test1>.Current));
        }

        private class Test1
        {
            public string Name { get; set; }
        }
    }
}
