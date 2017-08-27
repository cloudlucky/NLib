using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

using NLib.Extensions;

namespace NLib.Reflection
{
    /// <summary>
    /// Represents support for reflection.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class ReflectionHelper<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionHelper{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ReflectionHelper(T value)
        {
            this.Value = value;
            this.Type = value.GetType();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Reviewed. It's OK.")]
        public Type Type { get; }

        /// <summary>
        /// Gets the field by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The field if exists; otherwise null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        public FieldInfo GetField(string name)
        {
            Check.Current.ArgumentNullException(name, nameof(name));

            return this.Type.GetField(name);
        }

        /// <summary>
        /// Gets the property by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The property if exists; otherwise null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        public PropertyInfo GetProperty(string name)
        {
            Check.Current.ArgumentNullException(name, nameof(name));

            return this.Type.GetProperty(name);
        }

        /// <summary>
        /// Gets the member by selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The member if exists; otherwise null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Reviewed. It's OK.")]
        public MemberInfo GetMemberInfo<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            Check.Current.ArgumentNullException(keySelector, nameof(keySelector));

            return this.Type.GetMemberInfo(keySelector);
        }
    }
}
