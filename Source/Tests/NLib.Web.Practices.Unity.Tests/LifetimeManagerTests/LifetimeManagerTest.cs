namespace NLib.Web.Practices.Unity.Tests.LifetimeManagerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Web.Hosting;

    [TestClass]
    public class LifetimeManagerTest
    {
        [TestMethod]
        [DeploymentItem(".")]
        public void WebRequestTest()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Foo");

                Assert.AreEqual(200, response.StatusCode);
                Assert.IsTrue(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("HttpSessionLifetimeManager: Foo"));
            }
        }

        [TestMethod]
        [DeploymentItem(".")]
        public void WebRequestTest2()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Foo");

                Assert.AreEqual(200, response.StatusCode);
                Assert.IsTrue(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("HttpSessionLifetimeManager: Foo"));

                response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Bar");

                Assert.AreEqual(200, response.StatusCode);
                Assert.IsTrue(response.Output.ToString().Contains("HttpRequestLifetimeManager: Bar"));
                Assert.IsTrue(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.IsTrue(response.Output.ToString().Contains("HttpSessionLifetimeManager: Bar"));
            }
        }
    }
}
