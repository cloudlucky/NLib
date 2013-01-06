namespace NLib.Practices.Unity.Interception
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// Debug information with the Debug console.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    [Conditional("DEBUG")]
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Reviewed. It's OK. Like MVC attributes.")]
    public class DebugAttribute : FilterBaseAttribute
    {
        /// <summary>
        /// Writes the error to the debug console.
        /// </summary>
        /// <param name="context">The error context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn" />.</returns>
        public override IMethodReturn OnError(FilterErrorContext context)
        {
            if (context.MethodInvocation.MethodBase.DeclaringType != null)
            {
                Debug.WriteLine("Error: {0}.{1}", context.MethodInvocation.MethodBase.DeclaringType.FullName, context.MethodInvocation.MethodBase.Name);
            }

            Debug.WriteLine("  Exception: {0}", (object)context.MethodReturn.Exception.StackTrace);

            return null;
        }

        /// <summary>
        /// Writes the executed result to the debug console.
        /// </summary>
        /// <param name="context">The executed context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn" />.</returns>
        public override IMethodReturn OnExecuted(FilterExecutedContext context)
        {
            if (context.MethodInvocation.MethodBase.DeclaringType != null)
            {
                Debug.WriteLine("Executed: {0}.{1}", context.MethodInvocation.MethodBase.DeclaringType.FullName, context.MethodInvocation.MethodBase.Name);
            }

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

        /// <summary>
        /// Writes the input parameters to the debug console.
        /// </summary>
        /// <param name="context">The executing context.</param>
        /// <returns>Null to continue or an instance that implement <see cref="IMethodReturn" />.</returns>
        public override IMethodReturn OnExecuting(FilterExecutingContext context)
        {
            if (context.MethodInvocation.MethodBase.DeclaringType != null)
            {
                Debug.WriteLine("Executing: {0}.{1}", context.MethodInvocation.MethodBase.DeclaringType.FullName, context.MethodInvocation.MethodBase.Name);
            }

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
