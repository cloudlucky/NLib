namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public class FilterExecutedContext : FilterContextBase
    {
        private readonly IMethodReturn methodReturn;

        public FilterExecutedContext(IMethodInvocation methodInvocation, IMethodReturn methodReturn)
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