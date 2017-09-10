namespace NLib.Web.Practices.Unity.Tests
{
    using System;
    using System.Linq;
    using System.Web;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using NLib.Web.Practices.Unity;
    using NLib.Web.Practices.Unity.Tests.LifetimeManagerTests;

    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new UnityContainer();

            container.RegisterType<IService, Service>("HttpRequestLifetimeManager", new HttpRequestLifetimeManager(), new InjectionFactory(c => new Service(HttpContext.Current.Request.QueryString["Name"])))
                     .RegisterType<IService, Service>("ContainerControlledLifetimeManager", new ContainerControlledLifetimeManager(), new InjectionFactory(c => new Service(HttpContext.Current.Request.QueryString["Name"])))
                     .RegisterType<IService, Service>("HttpApplicationLifetimeManager", new HttpApplicationLifetimeManager(), new InjectionFactory(c => new Service(HttpContext.Current.Request.QueryString["Name"])))
                     .RegisterType<IService, Service>("HttpSessionLifetimeManager", new HttpSessionLifetimeManager(), new InjectionFactory(c => new Service(HttpContext.Current.Request.QueryString["Name"])));

            var locator = new UnityServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);
        }

        protected void Application_EndRequest()
        {
            if (HttpContext.Current.Items.OfType<HttpRequestLifetimeManager>().Any())
            {
                throw new Exception("The HttpContext.Current.Items is not empty of HttpRequestLifetimeManager ");
            }
        }
    }
}
