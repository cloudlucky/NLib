// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterContextBase.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public abstract class FilterContextBase
    {
        private readonly IMethodInvocation methodInvocation;

        protected FilterContextBase(IMethodInvocation methodInvocation)
        {
            this.methodInvocation = methodInvocation;
        }

        public IMethodInvocation MethodInvocation
        {
            get { return this.methodInvocation; }
        }
    }
}