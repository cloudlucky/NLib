// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlHelperExtensions.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Web.Mvc.Extensions
{
    using System.Reflection;
    using System.Web.Mvc;

    /// <summary>
    /// Defines extensions methods for <see cref="UrlHelper"/>.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// The current assembly name.
        /// </summary>
        private static readonly string CurrentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// The NLib.validate.js embedded resource name.
        /// </summary>
        private const string NLibValidateJs = "NLib.validate.js";

        /// <summary>
        /// The NLib.validate.unobstrusive.js embedded resource name.
        /// </summary>
        private const string NLibValidateUnobstrusiveJs = "NLib.validate.unobstrusive.js";

        /// <summary>
        /// Get embedded resource link.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="assemblyName">Name of the assembly where the embedded resource is.</param>
        /// <param name="resourceName">Name of the resource in the assembly.</param>
        /// <returns>The link for the embedded resource</returns>
        public static string EmbeddedResource(this UrlHelper urlHelper, string assemblyName, string resourceName)
        {
            return urlHelper.Action("GetFile", "EmbeddedResource", new { assemblyName, resourceName });
        }

        /// <summary>
        /// Gets the link for NLib.validate.js.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The link for NLib.validate.js.</returns>
        public static string NLibValidateScript(this UrlHelper urlHelper)
        {
            return EmbeddedResource(urlHelper, CurrentAssemblyName, NLibValidateJs);
        }

        /// <summary>
        /// Gets the link for NLib.validate.unobstrusive.js.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The link for NLib.validate.unobstrusive.js.</returns>
        public static string NLibValidateUnobstrusiveScript(this UrlHelper urlHelper)
        {
            return EmbeddedResource(urlHelper, CurrentAssemblyName, NLibValidateUnobstrusiveJs);
        }
    }
}
