using System.Linq.Expressions;

namespace NLib.Linq.Extensions
{
    /// <summary>
    /// Defines extensions methods for <see cref="Expression"/>.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>The name of the parameter.</returns>
        public static string GetParameterName(this Expression reference)
        {
            var lambda = (LambdaExpression)reference;
            var body = (MemberExpression)lambda.Body;

            return body.Member.Name;
        }
    }
}
