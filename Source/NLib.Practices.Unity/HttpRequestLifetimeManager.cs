// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestLifetimeManager.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Practices.Unity
{
    using System;
    using System.Linq;
    using System.Web;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// A <see cref="LifetimeManager"/> that holds the instances given to it, 
    /// keeping one instance per HttpRequest.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Need to register as a HttpModule in the web.config or dispose of it at the end of the request.
    /// </para>
    /// </remarks>
    public class HttpRequestLifetimeManager : LifetimeManager, IDisposable, IHttpModule
    {
        /// <summary>
        /// The key.
        /// </summary>
        private readonly object key;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestLifetimeManager"/> class.
        /// </summary>
        public HttpRequestLifetimeManager()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Disposes all object in the application context.
        /// If this class (<see cref="HttpRequestLifetimeManager"/>) as not been registered as a HttpModule, use this method in the Application_EndRequest event of the Global.asax.
        /// </summary>
        public static void DisposeAll()
        {
            var keys = HttpContext.Current.Application.AllKeys;

            foreach (var httpRequestLifetimeManager in keys.Select(key => HttpContext.Current.Items[key]).OfType<HttpRequestLifetimeManager>())
            {
                httpRequestLifetimeManager.Dispose();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestLifetimeManager"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public HttpRequestLifetimeManager(object key)
        {
            this.key = key;
        }

        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>
        /// the object desired, or null if no such object is currently stored.
        /// </returns>
        public override object GetValue()
        {
            return HttpContext.Current.Items[this.key];
        }

        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[this.key] = newValue;
        }

        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            if (HttpContext.Current.Items.Contains(this.key))
            {
                var disposable = HttpContext.Current.Items[this.key] as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }

                HttpContext.Current.Items.Remove(this.key);
            }
        }

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += (sender, e) => this.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.RemoveValue();
        }
    }
}
