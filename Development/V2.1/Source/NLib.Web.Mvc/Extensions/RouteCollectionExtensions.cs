namespace NLib.Web.Mvc.Extensions
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Defines extensions methods for <see cref="RouteCollection"/>.
    /// </summary>
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Maps the route to get embedded resource.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void MapEmbeddedResourceRoute(this RouteCollection routes)
        {
            Check.Current.ArgumentNullException(routes, "routes");

            routes.MapRoute(
                "NLibEmbeddedResourceRoute", 
                "nlib/embeddedresource/{resourceName}",
                new { controller = "EmbeddedResource", action = "GetFile" },
                new[] { typeof(EmbeddedResourceController).Namespace });
        }
    }
}
