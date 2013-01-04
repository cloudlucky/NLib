namespace NLib.Web.Practices.Unity
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.SessionState;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// A <see cref="LifetimeManager"/> that holds the instances given to it, 
    /// keeping one instance per HttpSession.
    /// </summary>
    public class HttpSessionLifetimeManager : LifetimeManager, IDisposable, IHttpModule
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
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An <see cref="HttpApplication"/> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            var ssm = context.Modules["Session"] as SessionStateModule;

            if (ssm != null)
            {
                ssm.End += (sender, e) => DisposeAll();
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
