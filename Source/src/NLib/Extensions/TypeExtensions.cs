namespace NLib.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Reflection;

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
                //var fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
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
                //var propertyInfo = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
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
            Check.Current.ArgumentNullException(type, "type")
                         .ArgumentNullException(keySelector, "keySelector");

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
                throw new ArgumentException("Not a member access", "keySelector");
            }

            return memberExpression.Member;
        }
    }
}
