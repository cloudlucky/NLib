namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class FilterErrorContext : FilterContextBase
    {
        private readonly IMethodReturn methodReturn;

        public FilterErrorContext(IMethodInvocation methodInvocation, IMethodReturn methodReturn)
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