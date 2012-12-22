// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterBaseAttribute.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Represents the base class for interception-filter attributes.
    /// </summary>
    public abstract class FilterBaseAttribute : HandlerAttribute, IFilterAttribute
    {
        /// <summary>
        /// Derived classes implement this method. When called, it creates a new call handler as specified in the attribute configuration.
        /// </summary>
        /// <param name="container">The <see cref="IUnityContainer"/> to use when creating handlers, if necessary.</param>
        /// <returns>A new call handler object.</returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new FilterDefaultCallHandler(this, this.Order);
        }

        /// <summary>
        /// When overridden in a derived class, handles when the execution is canceled.
        /// </summary>
        /// <param name="context">The cancel context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn"/>.</returns>
        public virtual IMethodReturn OnCancel(FilterCancelContext context)
        {
            return null;
        }

        /// <summary>
        /// When overridden in a derived class, handles when an error occured during the execution.
        /// </summary>
        /// <param name="context">The error context.</param>
        /// <returns>
        /// Null to continue or an instance that implement <see cref="IMethodReturn"/>.
        /// </returns>
        public virtual IMethodReturn OnError(FilterErrorContext context)
        {
            return null;
        }

        /// <summary>
        /// When overridden in a derived class, handles after the execution.
        /// </summary>
        /// <param name="context">The executed context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn"/>.</returns>
        public virtual IMethodReturn OnExecuted(FilterExecutedContext context)
        {
            return null;
        }

        /// <summary>
        /// When overridden in a derived class, handles before the execution.
        /// </summary>
        /// <param name="context">The executing context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn"/>.</returns>
        public virtual IMethodReturn OnExecuting(FilterExecutingContext context)
        {
            return null;
        }
    }
}
