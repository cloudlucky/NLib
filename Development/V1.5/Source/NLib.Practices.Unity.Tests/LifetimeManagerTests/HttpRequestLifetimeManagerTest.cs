namespace NLib.Practices.Unity.Tests.LifetimeManagerTests
{
    using NLib.Web.Hosting;

    using NUnit.Framework;

    [TestFixture]
    public class HttpRequestLifetimeManagerTest
    {
        [Test]
        public void WebRequestTest()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("HttpRequestLifetimeManagerTest.aspx", "Name=Foo");

                Assert.AreEqual(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
            }
        }

        [Test]
        public void WebRequestTest2()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("HttpRequestLifetimeManagerTest.aspx", "Name=Foo");

                Assert.AreEqual(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));

                response = td.GetResponse("HttpRequestLifetimeManagerTest.aspx", "Name=Bar");

                Assert.AreEqual(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Bar"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
            }
        }
    }
}
