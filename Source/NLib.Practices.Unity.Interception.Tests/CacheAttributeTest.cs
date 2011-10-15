namespace NLib.Practices.Unity.Interception.Tests
{
    using System;
    using System.Threading;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    using NUnit.Framework;

    [TestFixture]
    public class CacheAttributeTest
    {
        [Test]
        public void Test1()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interface1, Class1>();
            container.Configure<Interception>().SetDefaultInterceptorFor<Interface1>(new TransparentProxyInterceptor());

            var locator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);

            var i1 = ServiceLocator.Current.GetInstance<Interface1>();

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
    }
}
