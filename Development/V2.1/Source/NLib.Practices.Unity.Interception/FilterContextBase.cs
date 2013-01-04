namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Represents the base filter context.
    /// </summary>
    public abstract class FilterContextBase
    {
        /// <summary>
        /// The method invocation.
        /// </summary>
        private readonly IMethodInvocation methodInvocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterContextBase" /> class.
        /// </summary>
        /// <param name="methodInvocation">The method invocation.</param>
        protected FilterContextBase(IMethodInvocation methodInvocation)
        {
            this.methodInvocation = methodInvocation;
        }

        /// <summary>
        /// Gets the method invocation.
        /// </summary>
        public IMethodInvocation MethodInvocation
        {
            get { return this.methodInvocation; }
        }
    }
}