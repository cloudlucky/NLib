namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Represents the cancel filter context.
    /// </summary>
    public class FilterCancelContext : FilterContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCancelContext" /> class.
        /// </summary>
        /// <param name="methodInvocation">The method invocation.</param>
        public FilterCancelContext(IMethodInvocation methodInvocation)
            : base(methodInvocation)
        {
        }
    }
}