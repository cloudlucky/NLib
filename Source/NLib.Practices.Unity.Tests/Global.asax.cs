namespace NLib.Practices.Unity.Tests
{
    using System;
    using System.Linq;
    using System.Web;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;

    using NLib.Practices.Unity.Tests.LifetimeManagerTests;

    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new UnityContainer();

            container.RegisterType<IService, Service>("HttpRequestLifetimeManager", new HttpRequestLifetimeManager(), new InjectionFactory(c => new Service(HttpContext.Current.Request.QueryString["Name"])))
                     .RegisterType<IService, Service>("ContainerControlledLifetimeManager", new ContainerControlledLifetimeManager(), new InjectionFactory(c => new Service(HttpContext.Current.Request.QueryString["Name"])));

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

        protected void Application_End()
        {
            HttpApplicationLifetimeManager.DisposeAll();

            // TODO : Vérifier qu'il n'y a plus de Lifetime manager dans l'application
        }

        protected void Session_End()
        {
            HttpSessionLifetimeManager.DisposeAll();

            // TODO : Vérifier qu'il n'y a plus de Lifetime manager dans la session
        }
    }
}
