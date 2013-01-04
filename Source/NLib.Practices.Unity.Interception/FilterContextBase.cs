namespace NLib.Practices.Unity.Interception
{
    using Microsoft.Practices.Unity.InterceptionExtension;

    public abstract class FilterContextBase
    {
        private readonly IMethodInvocation methodInvocation;

        protected FilterContextBase(IMethodInvocation methodInvocation)
        {
            this.methodInvocation = methodInvocation;
        }

        public IMethodInvocation MethodInvocation
        {
            get { return this.methodInvocation; }
        }
    }
}