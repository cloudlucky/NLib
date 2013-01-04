namespace NLib.Practices.Unity.Interception
{
    using System;

    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Defines the default call handler for the <see cref="FilterBaseAttribute"/>.
    /// </summary>
    public class FilterDefaultCallHandler : ICallHandler
    {
        /// <summary>
        /// The filter base attribute.
        /// </summary>
        private readonly IFilterAttribute filterAttribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterDefaultCallHandler"/> class.
        /// </summary>
        /// <param name="filterAttribute">The filter attribute.</param>
        /// <param name="order">The order in which the handler will be executed.</param>
        public FilterDefaultCallHandler(IFilterAttribute filterAttribute, int order)
        {
            Check.Current.ArgumentNullException(filterAttribute, "filterAttribute");

            this.filterAttribute = filterAttribute;
            this.Order = order;
        }

        /// <summary>
        /// Gets the filter base attribute.
        /// </summary>
        public IFilterAttribute FilterAttribute
        {
            get { return this.filterAttribute; }
        }

        /// <summary>
        /// Gets or sets the order in which the handler will be executed.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Implement this method to execute your handler processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
        /// <returns>Return value from the target.</returns>
        public virtual IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var executing = this.DoExecuting(input, getNext);
            if (executing != null)
            {
                return executing;
            }

            var methodReturn = this.DoExecute(input, getNext);

            var error = this.DoError(input, getNext, methodReturn);
            if (error != null)
            {
                return error;
            }

            var executed = this.DoExecuted(input, getNext, methodReturn);
            if (executed != null)
            {
                return executed;
            }

            return methodReturn;
        }

        /// <summary>
        /// Do the error handling.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
        /// <param name="methodReturn">Outputs to the current call to the target.</param>
        /// <returns>Return value from the target or null to continue.</returns>
        protected virtual IMethodReturn DoError(IMethodInvocation input, GetNextHandlerDelegate getNext, IMethodReturn methodReturn)
        {
            if (methodReturn.Exception != null)
            {
                var error = this.filterAttribute.OnError(new FilterErrorContext(input, methodReturn));

                if (error != null)
                {
                    var executed = this.DoExecuted(input, getNext, error);

                    if (executed != null)
                    {
                        return executed;
                    }

                    return error;
                }
            }

            return null;
        }

        /// <summary>
        /// Do the execution.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
        /// <returns>Return value from the target or null to continue.</returns>
        protected virtual IMethodReturn DoExecute(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            return getNext()(input, getNext);
        }

        /// <summary>
        /// Do the executed.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
        /// <param name="methodReturn">Outputs to the current call to the target.</param>
        /// <returns>Return value from the target or null to continue.</returns>
        protected virtual IMethodReturn DoExecuted(IMethodInvocation input, GetNextHandlerDelegate getNext, IMethodReturn methodReturn)
        {
            var executed = this.filterAttribute.OnExecuted(new FilterExecutedContext(input, methodReturn));

            if (executed != null)
            {
                return executed;
            }

            return null;
        }

        /// <summary>
        /// Do the executing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate in the handler chain.</param>
        /// <returns>Return value from the target or null to continue.</returns>
        protected virtual IMethodReturn DoExecuting(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var executingContext = new FilterExecutingContext(input);
            var executing = this.filterAttribute.OnExecuting(executingContext);

            if (executingContext.Cancel)
            {
                var cancel = this.filterAttribute.OnCancel(new FilterCancelContext(input));

                return cancel ?? input.CreateExceptionMethodReturn(new OperationCanceledException());
            }

            if (executing != null)
            {
                var executed = this.DoExecuted(input, getNext, executing);

                if (executed != null)
                {
                    return executed;
                }

                return executing;
            }

            return null;
        }
    }
}