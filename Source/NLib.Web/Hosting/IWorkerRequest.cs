namespace NLib.Web.Hosting
{
    using System;
    using System.IO;

    /// <summary>
    /// Provides a simple interface to process a request an ASP.NET applications.
    /// </summary>
    public interface IWorkerRequest : IDisposable
    {
        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>The response.</returns>
        TextWriter ProcessRequest(string page, string queryString);
    }
}
