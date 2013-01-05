namespace NLib.Web.Practices.Unity
{
    using System;
    using System.Linq;
    using System.Web;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// A <see cref="LifetimeManager"/> that holds the instances given to it, 
    /// keeping one instance per HttpRequest.
    /// </summary>
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
        /// Initializes a new instance of the <see cref="HttpRequestLifetimeManager"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public HttpRequestLifetimeManager(object key)
        {
            this.key = key;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="HttpRequestLifetimeManager" /> class.
        /// </summary>
        ~HttpRequestLifetimeManager()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Disposes all object in the application context.
        /// </summary>
        public static void DisposeAll()
        {
            var keys = HttpContext.Current.Items.Keys;

            foreach (var httpRequestLifetimeManager in keys.Cast<string>().Select(key => HttpContext.Current.Items[key]).OfType<HttpRequestLifetimeManager>())
            {
                httpRequestLifetimeManager.Dispose();
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
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.RemoveValue();
            }
        }
    }
}
