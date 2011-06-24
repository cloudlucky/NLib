namespace NLib.Web.Mvc.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class EmbeddedResourceControllerTest
    {
        [Test]
        public void GetFileAction()
        {
            var controller = new EmbeddedResourceController();
            var result = controller.GetFile("NLib.Web.Mvc.Tests", "EmbeddedResourceFile.txt");

            Assert.NotNull(result);
        }
    }
}
