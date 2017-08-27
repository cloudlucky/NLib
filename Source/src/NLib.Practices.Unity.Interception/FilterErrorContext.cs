namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Represents the Error filter context.
    /// </summary>
    public class FilterErrorContext : FilterContextBase
    {
        /// <summary>
        /// The method return.
        /// </summary>
        private readonly IMethodReturn methodReturn;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterErrorContext" /> class.
        /// </summary>
        /// <param name="methodInvocation">The method invocation.</param>
        /// <param name="methodReturn">The method return.</param>
        public FilterErrorContext(IMethodInvocation methodInvocation, IMethodReturn methodReturn)
            : base(methodInvocation)
        {
            this.methodReturn = methodReturn;
        }

        /// <summary>
        /// Gets the method return.
        /// </summary>
        public IMethodReturn MethodReturn
        {
            get { return this.methodReturn; }
        }
    }
}