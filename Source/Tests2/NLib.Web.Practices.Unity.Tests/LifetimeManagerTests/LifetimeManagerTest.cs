namespace NLib.Web.Practices.Unity.Tests.LifetimeManagerTests
{
    using NLib.Web.Hosting;

    using Xunit;
    public class LifetimeManagerTest
    {
        [Fact]
        public void WebRequestTest()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Foo");

                Assert.Equal(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpSessionLifetimeManager: Foo"));
            }
        }

        [Fact]
        public void WebRequestTest2()
        {
            using (var td = new UnitTestWorkerDriver())
            {
                var response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Foo");

                Assert.Equal(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpSessionLifetimeManager: Foo"));

                response = td.GetResponse("LifetimeManagerTest.aspx", "Name=Bar");

                Assert.Equal(200, response.StatusCode);
                Assert.True(response.Output.ToString().Contains("HttpRequestLifetimeManager: Bar"));
                Assert.True(response.Output.ToString().Contains("ContainerControlledLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpApplicationLifetimeManager: Foo"));
                Assert.True(response.Output.ToString().Contains("HttpSessionLifetimeManager: Bar"));
            }
        }
    }
}
