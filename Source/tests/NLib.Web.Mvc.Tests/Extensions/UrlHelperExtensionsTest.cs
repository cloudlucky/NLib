namespace NLib.Web.Mvc.Tests.Extensions
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Moq;

    using NLib.Web.Mvc.Extensions;

    using Xunit;

    public class UrlHelperExtensionsTest
    {
        public UrlHelperExtensionsTest()
        {
            this.Controller = GetController();
        }

        public Controller Controller { get; set; }
        
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

        [Fact]
        public void EmbeddedResourceTest()
        {
            var s = this.Controller.Url.EmbeddedResource("NLib.Web.Mvc.Tests", "EmbeddedResourceFile.txt");

            Assert.Equal("/nlib/embeddedresource/EmbeddedResourceFile.txt?assemblyName=NLib.Web.Mvc.Tests", s);
        }

        [Fact]
        public void NLibValidateScriptTest()
        {
            var s = this.Controller.Url.NLibValidateScript();

            Assert.Equal("/nlib/embeddedresource/NLib.validate.js?assemblyName=NLib.Web.Mvc", s);
        }

        [Fact]
        public void NLibValidateUnobtrusiveScriptTest()
        {
            var s = this.Controller.Url.NLibValidateUnobtrusiveScript();

            Assert.Equal("/nlib/embeddedresource/NLib.validate.unobtrusive.js?assemblyName=NLib.Web.Mvc", s);
        }
    }
}
