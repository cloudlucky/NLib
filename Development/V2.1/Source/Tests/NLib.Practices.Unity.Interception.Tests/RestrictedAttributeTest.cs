namespace NLib.Practices.Unity.Interception.Tests
{
    using System.Security.Principal;
    using System.Threading;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    using Xunit;

    public class RestrictedAttributeTest
    {
        [Fact]
        public void Test1()
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Interface1, Class1>();
            container.Configure<Interception>().SetDefaultInterceptorFor<Interface1>(new TransparentProxyInterceptor());

            var locator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);

            var i1 = ServiceLocator.Current.GetInstance<Interface1>();

            var identity = new GenericIdentity("foo");
            var principal = new GenericPrincipal(identity, new[] { "Administrator" });

            Thread.CurrentPrincipal = principal;

            i1.M1("foo");
        }

        private interface Interface1
        {
            void M1(string s);
        }

        [Restricted]
        [Debug]
        private class Class1 : Interface1
        {
            public void M1(string s)
            {
            }
        }
    }
}
