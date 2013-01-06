namespace NLib.Practices.Unity.Interception.Tests
{
    using System;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DebugAttributeTest
    {
        [TestMethod]
        public void Test1()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interface1, Class1>();
            container.Configure<Interception>().SetDefaultInterceptorFor<Interface1>(new TransparentProxyInterceptor());

            var locator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);

            var i1 = ServiceLocator.Current.GetInstance<Interface1>();

            try
            {
                i1.M1("foo");
            }
            catch
            {
            }
        }

        private interface Interface1
        {
            void M1(string s);
        }

        [Debug]
        private class Class1 : Interface1
        {
            public void M1(string s)
            {
                throw new Exception("foo bar");
            }
        }
    }
}
