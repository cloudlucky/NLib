namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class FilterCancelContext : FilterContextBase
    {
        public FilterCancelContext(IMethodInvocation methodInvocation)
            : base(methodInvocation)
        {
        }
    }
}