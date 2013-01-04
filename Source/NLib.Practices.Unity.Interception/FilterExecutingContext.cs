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