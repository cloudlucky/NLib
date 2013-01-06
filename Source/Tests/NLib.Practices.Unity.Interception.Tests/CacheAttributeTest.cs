namespace NLib.Practices.Unity.Interception.Tests
{
    using System.Threading;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CacheAttributeTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interface1, Class1>("class1");
            container.RegisterType<Interface1, Class2>("class2");
            container.Configure<Interception>().SetDefaultInterceptorFor<Interface1>(new TransparentProxyInterceptor());

            var locator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);
        }

        [TestMethod]
        public void Test1()
        {
            var i1 = ServiceLocator.Current.GetInstance<Interface1>("class1");

            i1.P1 = "Foo";
            Assert.AreEqual("Foo", i1.P1);
            i1.P1 = "Bar";
            Assert.AreEqual("Foo", i1.P1);
            Thread.Sleep(2000);
            Assert.AreEqual("Bar", i1.P1);
        }

        [TestMethod]
        public void Test2()
        {
            var i1 = ServiceLocator.Current.GetInstance<Interface1>("class2");

            i1.P1 = "Foo";
            Assert.AreEqual("Foo", i1.P1);
            i1.P1 = "Bar";
            Assert.AreEqual("Foo", i1.P1);
            Thread.Sleep(2000);
            Assert.AreEqual("Bar", i1.P1);
        }

        private interface Interface1
        {
            string P1 { get; set; }
        }

        private class Class1 : Interface1
        {
            public string P1 { [Cache(AbsoluteExpiration = 1000)] get; set; }
        }

        [Cache(AbsoluteExpiration = 1000)]
        private class Class2 : Interface1
        {
            public string P1 { get; set; }
        }
    }
}
