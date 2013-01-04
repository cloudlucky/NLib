using NLib.Web.Practices.Unity;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Initializer), "Initialize")]
[assembly: WebActivator.ApplicationShutdownMethod(typeof(Initializer), "Shutdown")]

namespace NLib.Web.Practices.Unity
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    /// <summary>
    /// Used by WebActivator to do some stuff at the start and the end of the application cycle.
    /// </summary>
    public static class Initializer
    {
        /// <summary>
        /// Call when the application start.
        /// </summary>
        public static void Initialize()
        {
            DynamicModuleUtility.RegisterModule(typeof(HttpRequestLifetimeManager));
            DynamicModuleUtility.RegisterModule(typeof(HttpSessionLifetimeManager));
        }

        /// <summary>
        /// Call when the application shutdown.
        /// </summary>
        public static void Shutdown()
        {
            HttpApplicationLifetimeManager.DisposeAll();
        }
    }
}
