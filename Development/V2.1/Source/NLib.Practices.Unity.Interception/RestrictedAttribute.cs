namespace NLib.Practices.Unity.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Restrict the access.
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RestrictedAttribute : FilterBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictedAttribute" /> class.
        /// </summary>
        public RestrictedAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictedAttribute" /> class.
        /// </summary>
        /// <param name="roles">The authorize roles.</param>
        public RestrictedAttribute(params string[] roles)
        {
            this.Roles = roles;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        public IEnumerable<string> Roles { get; private set; }

        /// <summary>
        /// When overridden in a derived class, handles before the execution.
        /// </summary>
        /// <param name="context">The executing context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn" />.</returns>
        public override IMethodReturn OnExecuting(FilterExecutingContext context)
        {
            if (Thread.CurrentPrincipal == null || !Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return context.MethodInvocation.CreateExceptionMethodReturn(new UnauthorizedAccessException());
            }
            
            if (this.Roles != null && !this.Roles.Any(x => Thread.CurrentPrincipal.IsInRole(x)))
            {
                return context.MethodInvocation.CreateExceptionMethodReturn(new UnauthorizedAccessException());
            }

            return null;
        }
    }
}
