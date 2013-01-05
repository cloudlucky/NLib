namespace NLib.Web.Mvc
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// Embedded resource controller.
    /// </summary>
    public class EmbeddedResourceController : Controller
    {
        /// <summary>
        /// Contains predefined mime types.
        /// </summary>
        private static readonly Dictionary<string, string> MimeTypes = InitializeMimeTypes();

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly where the embedded resource is.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The embedded resource file.</returns>
        public ActionResult GetFile(string assemblyName, string resourceName)
        {
            Check.Current.ArgumentNullOrWhiteSpaceException(assemblyName, "assemblyName")
                         .ArgumentNullOrWhiteSpaceException(resourceName, "resourceName");

            Assembly assembly;
            try
            {
                assembly = Assembly.Load(assemblyName);
            }
            catch (FileNotFoundException)
            {
                this.Response.StatusCode = 404;
                return null;
            }

            var resourceStream = assembly.GetManifestResourceStream(string.Format(CultureInfo.CurrentCulture, "{0}.{1}", assemblyName, resourceName));

            if (resourceStream == null)
            {
                this.Response.StatusCode = 404;
                return null;
            }

            var contentType = GetContentType(resourceName);
            return this.File(resourceStream, contentType);
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>The type of the content.</returns>
        private static string GetContentType(string resourceName)
        {
            var extension = resourceName.Substring(resourceName.LastIndexOf('.')).ToUpperInvariant();
            return MimeTypes[extension];
        }

        /// <summary>
        /// Initializes the mime types.
        /// </summary>
        /// <returns>The mime types.</returns>
        private static Dictionary<string, string> InitializeMimeTypes()
        {
            return new Dictionary<string, string>
                {
                    { ".GIF", "image/gif" }, 
                    { ".PNG", "image/png" }, 
                    { ".JPG", "image/jpeg" }, 
                    { ".JS", "text/javascript" }, 
                    { ".CSS", "text/css" }, 
                    { ".TXT", "text/plain" }, 
                    { ".XML", "application/xml" }, 
                    { ".ZIP", "application/zip" }
                };
        }
    }
}
