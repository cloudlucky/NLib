namespace NLib.Web.Hosting
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.Hosting;

    /// <summary>
    /// Provides a simple way to process a request an ASP.NET applications outside an Internet Information Services (IIS) application.
    /// </summary>
    public sealed class WorkerRequest : MarshalByRefObject, IWorkerRequest
    {
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>The response.</returns>
        /// <example>
        /// <code>
        /// ProcessRequest("Text.aspx", "Name=Foo");
        /// </code>
        /// </example>
        public TextWriter ProcessRequest(string page, string queryString)
        {
            using (var writer = new StringWriter(CultureInfo.CurrentCulture))
            {
                var request = new SimpleWorkerRequest(page, queryString, writer);
                HttpRuntime.ProcessRequest(request);
                writer.Flush();

                return writer;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            HttpRuntime.Close();
        }
    }
}
