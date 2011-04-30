// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWorkerDriver.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Web.Hosting
{
    using System;
    using System.Web;

    /// <summary>
    /// Provides a simple interface to get a response of an ASP.NET request.
    /// </summary>
    public interface IWorkerDriver : IDisposable
    {
        /// <summary>
        /// Gets the response of a request.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="queryString">The query string.</param>
        /// <returns>The response</returns>
        HttpResponse GetResponse(string page, string queryString);
    }
}
