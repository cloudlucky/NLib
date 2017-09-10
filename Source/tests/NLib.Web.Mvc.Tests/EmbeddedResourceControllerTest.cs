namespace NLib.Web.Mvc.Tests
{
    using Xunit;

    public class EmbeddedResourceControllerTest
    {
        [Fact]
        public void GetFileAction()
        {
            var controller = new EmbeddedResourceController();
            var result = controller.GetFile("NLib.Web.Mvc.Tests", "EmbeddedResourceFile.txt");

            Assert.NotNull(result);
        }
    }
}
