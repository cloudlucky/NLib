// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpApplicationLifetimeManager.cs" company=".">
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
    /// keeping one instance per HttpApplication.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This LifetimeManager does not dispose the instances it holds.
    /// It needs to be done at the Application End event.
    /// See <see cref="HttpApplicationLifetimeManager.DisposeAll()"/>.
    /// </para>
    /// </remarks>
    public class HttpApplicationLifetimeManager : LifetimeManager, IDisposable
    {
        /// <summary>
        /// The key.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApplicationLifetimeManager"/> class.
        /// </summary>
        public HttpApplicationLifetimeManager()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpApplicationLifetimeManager"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public HttpApplicationLifetimeManager(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// Disposes all object in the application context.
        /// Use this method in the Application_End event of the Global.asax.
        /// </summary>
        public static void DisposeAll()
        {
            var keys = HttpContext.Current.Application.AllKeys;

            foreach (var httpApplicationLifetimeManager in keys.Select(key => HttpContext.Current.Application[key]).OfType<HttpApplicationLifetimeManager>())
            {
                httpApplicationLifetimeManager.Dispose();
            }
        }

        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>
        /// the object desired, or null if no such object is currently stored.
        /// </returns>
        public override object GetValue()
        {
            return HttpContext.Current.Application[this.key];
        }

        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            HttpContext.Current.Application[this.key] = newValue;
        }

        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            if (HttpContext.Current.Application.AllKeys.Contains(this.key))
            {
                var disposable = HttpContext.Current.Application[this.key] as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }

                HttpContext.Current.Application.Remove(this.key);
            }
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
