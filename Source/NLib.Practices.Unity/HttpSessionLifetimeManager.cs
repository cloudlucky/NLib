// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpSessionLifetimeManager.cs" company=".">
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
    /// keeping one instance per HttpSession.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This LifetimeManager does not dispose the instances it holds.
    /// It needs to be done at the Session End event.
    /// </para>
    /// </remarks>
    public class HttpSessionLifetimeManager : LifetimeManager, IDisposable
    {
        /// <summary>
        /// The key.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpSessionLifetimeManager"/> class.
        /// </summary>
        public HttpSessionLifetimeManager()
            : this(Guid.NewGuid().ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpSessionLifetimeManager"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public HttpSessionLifetimeManager(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// Disposes all object in the application context.
        /// Use this method in the Session_End event of the Global.asax.
        /// </summary>
        public static void DisposeAll()
        {
            var keys = HttpContext.Current.Session.Keys;

            foreach (var httpSessionLifetimeManager in keys.Cast<string>().Select(key => HttpContext.Current.Session[key]).OfType<HttpSessionLifetimeManager>())
            {
                httpSessionLifetimeManager.Dispose();
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
            return HttpContext.Current.Session[this.key];
        }

        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            HttpContext.Current.Session[this.key] = newValue;
        }

        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            if (HttpContext.Current.Session[this.key] != null)
            {
                var disposable = HttpContext.Current.Session[this.key] as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }

                HttpContext.Current.Session.Remove(this.key);
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
