using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace NLib.Extensions
{
    /// <summary>
    /// Defines extensions methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the field.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns>The field information.</returns>
        public static FieldInfo GetField(this Type type, string name)
        {
            while (type != null && type != typeof(object))
            {
                var fieldInfo = type.GetTypeInfo().GetDeclaredField(name);

                if (fieldInfo != null)
                {
                    return fieldInfo;
                }

                type = type.GetTypeInfo().BaseType;
            }

            return null;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns>The property information.</returns>
        public static PropertyInfo GetProperty(this Type type, string name)
        {
            while (type != null && type != typeof(object))
            {
                var propertyInfo = type.GetTypeInfo().GetDeclaredProperty(name);

                if (propertyInfo != null)
                {
                    return propertyInfo;
                }

                type = type.GetTypeInfo().BaseType;
            }

            return null;
        }

        /// <summary>
        /// Searches for the public property with the specified name.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="type">The type  .</param>
        /// <param name="keySelector">The selector of the public property to get.</param>
        /// <returns>
        /// A <see cref="PropertyInfo"/> object representing the public property with the specified name, if found; otherwise, null.
        /// </returns>
        /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Check class do the check")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Check class do the check")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Reviewed. It's ok.")]
        public static MemberInfo GetMemberInfo<T, TKey>(this Type type, Expression<Func<T, TKey>> keySelector)
        {
            Check.Current.ArgumentNullException(type, nameof(type))
                         .ArgumentNullException(keySelector, nameof(keySelector));

            MemberExpression memberExpression = null;

            switch (keySelector.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpression = ((UnaryExpression)keySelector.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = keySelector.Body as MemberExpression;
                    break;
            }

            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access", nameof(keySelector));
            }

            return memberExpression.Member;
        }
    }
}
