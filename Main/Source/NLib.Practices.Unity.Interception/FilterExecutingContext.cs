namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Represents the Executing filter context.
    /// </summary>
    public class FilterExecutingContext : FilterContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterExecutingContext" /> class.
        /// </summary>
        /// <param name="methodInvocation">The method invocation.</param>
        public FilterExecutingContext(IMethodInvocation methodInvocation)
            : base(methodInvocation)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FilterExecutingContext" /> is canceled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancel; otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }
    }
}