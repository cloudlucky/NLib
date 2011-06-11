namespace NLib.Practices.Unity.Tests.LifetimeManagerTests
{
    using NLib.Web.Hosting;

    using NUnit.Framework;

    [TestFixture]
    public class LifetimeManagerTest
    {
        [Test]
        public void WebRequestTest()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Foo");

                Assert.AreEqual(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpSessionLifetimeManager: Foo"));
            }
        }

        [Test]
        public void WebRequestTest2()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Foo");

                Assert.AreEqual(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpSessionLifetimeManager: Foo"));

                response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Bar");

                Assert.AreEqual(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Bar"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpSessionLifetimeManager: Bar"));
            }
        }
    }
}
