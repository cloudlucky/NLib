// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerRequest.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Web.Hosting
{
    using System;
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
            using (var writer = new StringWriter())
            {
                var request = new SimpleWorkerRequest(page, queryString, writer);
                HttpRuntime.ProcessRequest(request);
                writer.Flush();

                request.cl

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
