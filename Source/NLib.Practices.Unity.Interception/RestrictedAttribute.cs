// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RestrictedAttribute.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Practices.Unity.Interception
{
    using System;
    using System.Linq;
    using System.Threading;

    using Microsoft.Practices.Unity.InterceptionExtension;

    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RestrictedAttribute : FilterBaseAttribute
    {
        public RestrictedAttribute()
        {
        }

        public RestrictedAttribute(params string[] roles)
        {
            this.Roles = roles;
        }

        public string[] Roles { get; set; }

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
