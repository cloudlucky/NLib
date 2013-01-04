namespace NLib.Practices.Unity.Interception
{
    using System;
    using System.Runtime.Caching;

    using Microsoft.Practices.Unity.InterceptionExtension;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CacheAttribute : FilterBaseAttribute
    {
        static CacheAttribute()
        {
            Cache = new MemoryCache("NLibCache");
        }

        public CacheAttribute()
        {
            this.AbsoluteExpiration = 300000;
            this.SlidingExpiration = 0;
        }

        public static ObjectCache Cache { get; set; }

        public string Key { get; set; }

        public int AbsoluteExpiration { get; set; }

        public int SlidingExpiration { get; set; }

        public override IMethodReturn OnExecuted(FilterExecutedContext context)
        {
            var key = this.GetKey(context);
            if (!Cache.Contains(key))
            {
                var policy = new CacheItemPolicy
                    {
                        AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMilliseconds(this.AbsoluteExpiration)),
                        Priority = CacheItemPriority.Default,
                        SlidingExpiration = TimeSpan.FromMilliseconds(this.SlidingExpiration),
                    };

                Cache.Add(key, context.MethodReturn.ReturnValue, policy);
            }

            return null;
        }

        public override IMethodReturn OnExecuting(FilterExecutingContext context)
        {
            var key = this.GetKey(context);
            if (Cache.Contains(key))
            {
                return context.MethodInvocation.CreateMethodReturn(Cache[key]);
            }

            return null;
        }

        protected string GetKey(FilterContextBase context)
        {
            return this.Key ?? context.MethodInvocation.MethodBase.DeclaringType.FullName + context.MethodInvocation.MethodBase.Name;
        }
    }
}
