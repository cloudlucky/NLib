namespace NLib.Practices.Unity.Interception
{
    using System;
    using System.Diagnostics;

    using Microsoft.Practices.Unity.InterceptionExtension;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [Conditional("DEBUG")]
    public class DebugAttribute : FilterBaseAttribute
    {
        public override IMethodReturn OnError(FilterErrorContext context)
        {
            Debug.WriteLine("Error: {0}.{1}", context.MethodInvocation.MethodBase.DeclaringType.FullName, context.MethodInvocation.MethodBase.Name);
            Debug.WriteLine("  Exception: {0}", (object)context.MethodReturn.Exception.StackTrace);

            return null;
        }

        public override IMethodReturn OnExecuted(FilterExecutedContext context)
        {
            Debug.WriteLine("Executed: {0}.{1}", context.MethodInvocation.MethodBase.DeclaringType.FullName, context.MethodInvocation.MethodBase.Name);
            Debug.WriteLine("  return value = {0}", context.MethodReturn.ReturnValue);

            if (context.MethodReturn.Outputs.Count > 0)
            {
                Debug.WriteLine("  with output arguments:");

                for (var i = 0; i < context.MethodReturn.Outputs.Count; ++i)
                {
                    var parameterInfo = context.MethodReturn.Outputs.GetParameterInfo(i);
                    Debug.WriteLine("    {0} = {1}", parameterInfo.Name, context.MethodReturn.Outputs[i]);
                }
            }

            return null;
        }

        public override IMethodReturn OnExecuting(FilterExecutingContext context)
        {
            Debug.WriteLine("Executing: {0}.{1}", context.MethodInvocation.MethodBase.DeclaringType.FullName, context.MethodInvocation.MethodBase.Name);

            if (context.MethodInvocation.Arguments.Count > 0)
            {
                Debug.WriteLine("  with arguments:");
                
                for (var i = 0; i < context.MethodInvocation.Arguments.Count; ++i)
                {
                    var parameterInfo = context.MethodInvocation.Arguments.GetParameterInfo(i);
                    Debug.WriteLine("    {0} = {1}", parameterInfo.Name, context.MethodInvocation.Arguments[i]);
                }
            }

            return null;
        }
    }
}
