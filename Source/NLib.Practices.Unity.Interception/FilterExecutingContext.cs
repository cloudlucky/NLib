// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterExecutingContext.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class FilterExecutingContext : FilterContextBase
    {
        public FilterExecutingContext(IMethodInvocation methodInvocation)
            : base(methodInvocation)
        {
        }

        public bool Cancel { get; set; }
    }
}