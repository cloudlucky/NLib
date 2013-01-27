namespace NLib.Practices.Unity.Interception
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Caching;

    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Cache a method result.
    /// </summary>
    /// <remarks>
    /// The implementation of the cache can be set by <see cref="CacheAttribute.Cache"/> property.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Reviewed. It's OK. Like MVC attributes.")]
    public class CacheAttribute : FilterBaseAttribute
    {
        /// <summary>
        /// Initializes static members of the <see cref="CacheAttribute" /> class.
        /// </summary>
        static CacheAttribute()
        {
            Cache = new MemoryCache(typeof(CacheAttribute).FullName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheAttribute" /> class.
        /// </summary>
        public CacheAttribute()
        {
            this.AbsoluteExpiration = 300000;
            this.SlidingExpiration = 0;
        }

        /// <summary>
        /// Gets or sets the cache.
        /// </summary>
        public static ObjectCache Cache { get; set; }

        /// <summary>
        /// Gets or sets the key used by the cache.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether a cache entry should be evicted after a specified duration.
        /// </summary>
        public int AbsoluteExpiration { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether a cache entry should be evicted if it has not been accessed in a given span of time.
        /// </summary>
        public int SlidingExpiration { get; set; }

        /// <summary>
        /// Cache the result.
        /// </summary>
        /// <param name="context">The executed context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn" />.</returns>
        public override IMethodReturn OnExecuted(FilterExecutedContext context)
        {
            var key = this.GetKeyName(context);
            if (!Cache.Contains(key))
            {
                var policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMilliseconds(this.AbsoluteExpiration)),
                        Priority = CacheItemPriority.Default,
                        SlidingExpiration = TimeSpan.FromMilliseconds(this.SlidingExpiration),
                    };

                var returnValue = context.MethodReturn.ReturnValue;
                if (returnValue != null)
                {
                    Cache.Add(key, context.MethodReturn.ReturnValue, policy);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the cache result; otherwise it execute method.
        /// </summary>
        /// <param name="context">The executing context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn" />.</returns>
        public override IMethodReturn OnExecuting(FilterExecutingContext context)
        {
            var key = this.GetKeyName(context);
            if (Cache.Contains(key))
            {
                return context.MethodInvocation.CreateMethodReturn(Cache[key]);
            }

            return null;
        }

        /// <summary>
        /// Gets the key name for the cache.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The key name.</returns>
        protected string GetKeyName(FilterContextBase context)
        {
            return this.Key ?? context.MethodInvocation.Target.GetType().FullName + context.MethodInvocation.MethodBase.Name + context.MethodInvocation.Target.GetHashCode();
        }
    }
}
