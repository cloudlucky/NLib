namespace NLib.Tests.Extensions
{
    using NLib.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class ConvertExtensionTest
    {
        [Test]
        public void Test1()
        {
            var s = "46";
            var i = s.ChangeType<int>();

            Assert.AreEqual(s, i.ToString());
        }

        [Test]
        [SetCulture("fr-CA")]
        public void Test2()
        {
            var s = "46,22";
            var i = s.ChangeType<float>();

            Assert.AreEqual(s, i.ToString());
        }

        [Test]    
        public void Test3()
        {
            var s = "46";
            var i = s.ChangeType<string>();

            Assert.AreEqual(s, i);
        }
    }
}
