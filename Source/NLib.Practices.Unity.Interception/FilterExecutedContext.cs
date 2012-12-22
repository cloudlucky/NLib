// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterExecutedContext.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class FilterExecutedContext : FilterContextBase
    {
        private readonly IMethodReturn methodReturn;

        public FilterExecutedContext(IMethodInvocation methodInvocation, IMethodReturn methodReturn)
            : base(methodInvocation)
        {
            this.methodReturn = methodReturn;
        }

        public IMethodReturn MethodReturn
        {
            get { return this.methodReturn; }
        }
    }
}