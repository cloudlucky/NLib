namespace NLib.Web.Mvc.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmbeddedResourceControllerTest
    {
        [TestMethod]
        public void GetFileAction()
        {
            var controller = new EmbeddedResourceController();
            var result = controller.GetFile("NLib.Web.Mvc.Tests", "EmbeddedResourceFile.txt");

            Assert.IsNotNull(result);
        }
    }
}
