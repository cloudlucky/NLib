namespace NLib.Web.Mvc.Tests.Extensions
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;

    using NLib.Web.Mvc.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class UrlHelperExtensionTest
    {
        public Controller Controller { get; set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.Controller = GetController();
        }

        protected Controller GetController()
        {
            var routes = new RouteCollection();
            routes.MapEmbeddedResourceRoute();

            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.SetupGet(x => x.ApplicationPath).Returns("/");
            request.SetupGet(x => x.Url).Returns(new Uri("http://localhost", UriKind.Absolute));
            request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns((string p) => p);

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);

            var controller = new Mock<Controller>().Object;
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);

            return controller;
        }

        [Test]
        public void EmbeddedResourceTest()
        {
            var s = this.Controller.Url.EmbeddedResource("NLib.Web.Mvc.Tests", "EmbeddedResourceFile.txt");

            Assert.AreEqual("/nlib/embeddedresource/EmbeddedResourceFile.txt?assemblyName=NLib.Web.Mvc.Tests", s);
        }

        [Test]
        public void NLibValidateScriptTest()
        {
            var s = this.Controller.Url.NLibValidateScript();

            Assert.AreEqual("/nlib/embeddedresource/NLib.validate.js?assemblyName=NLib.Web.Mvc", s);
        }

        [Test]
        public void NLibValidateUnobstrusiveScriptTest()
        {
            var s = this.Controller.Url.NLibValidateUnobstrusiveScript();

            Assert.AreEqual("/nlib/embeddedresource/NLib.validate.unobstrusive.js?assemblyName=NLib.Web.Mvc", s);
        }
    }
}
