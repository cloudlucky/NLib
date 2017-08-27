using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

using NLib.Reflection.Resources;

namespace NLib.Reflection.Extensions
{
    /// <summary>
    /// Defines extensions methods for <see cref="ReflectionHelper{T}"/>.
    /// </summary>
    public static class ReflectionHelperExtensions
    {
        /// <summary>
        /// Get the field by name.
        /// </summary>
        /// <typeparam name="T">The Type of the object.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of the field.</param>
        /// <returns>The field helper.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        /// <exception cref="MissingFieldException">The field cannot be found.</exception>
        public static FieldHelper<T, object> Field<T>(this ReflectionHelper<T> helper, string name)
        {
            Check.Current.ArgumentNullException(helper, nameof(helper))
                         .ArgumentNullException(name, nameof(name));

            var fieldInfo = helper.GetField(name);

            Check.Current.Requires<MissingFieldException>(fieldInfo != null, new { className = helper.Type.Name, fieldName = name });

            return new FieldHelper<T, object>(helper, fieldInfo);
        }

        /// <summary>
        /// Get the field by selector.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <typeparam name="TKey">The type of the field.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The field helper.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        /// <exception cref="FieldAccessException">The field cannot be found or is not a <see cref="FieldInfo"/>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Reviewed. It's OK.")]
        public static FieldHelper<T, TKey> Field<T, TKey>(this ReflectionHelper<T> helper, Expression<Func<T, TKey>> keySelector)
        {
            Check.Current.ArgumentNullException(helper, nameof(helper))
                         .ArgumentNullException(keySelector, nameof(keySelector));

            var fieldInfo = helper.GetMemberInfo(keySelector) as FieldInfo;

            Check.Current.Requires<FieldAccessException>(fieldInfo != null, string.Format(CultureInfo.CurrentCulture, ReflectionResource.IsNotFieldInfo, "keySelector"));

            return new FieldHelper<T, TKey>(helper, fieldInfo);
        }

        /// <summary>
        /// Get the property by name.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name of the property.</param>
        /// <returns>The property helper.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        /// <exception cref="MissingMemberException">The property cannot be found.</exception>
        public static PropertyHelper<T, object> Property<T>(this ReflectionHelper<T> helper, string name)
        {
            Check.Current.ArgumentNullException(helper, nameof(helper))
                         .ArgumentNullException(name, nameof(name));

            var propertyInfo = helper.GetProperty(name);

            Check.Current.Requires<MissingMemberException>(propertyInfo != null, new { className = helper.Type.Name, memberName = name });

            return new PropertyHelper<T, object>(helper, propertyInfo);
        }

        /// <summary>
        /// Get the property by selector.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <typeparam name="TKey">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The property helper.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        /// <exception cref="FieldAccessException">The property cannot be found or is not a <see cref="PropertyInfo"/>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Reviewed. It's OK.")]
        public static PropertyHelper<T, TKey> Property<T, TKey>(this ReflectionHelper<T> helper, Expression<Func<T, TKey>> keySelector)
        {
            Check.Current.ArgumentNullException(helper, nameof(helper))
                         .ArgumentNullException(keySelector, nameof(keySelector));

            var propertyInfo = helper.GetMemberInfo(keySelector) as PropertyInfo;

            Check.Current.Requires<MemberAccessException>(propertyInfo != null, string.Format(CultureInfo.CurrentCulture, ReflectionResource.IsNotPropertyInfo, "keySelector"));

            return new PropertyHelper<T, TKey>(helper, propertyInfo);
        }

        /// <summary>
        /// Gets the Fluent reflection.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The reflection helper.</returns>
        public static ReflectionHelper<T> Reflection<T>(this T value)
        {
            Check.Current.ArgumentNullException(value, nameof(value));

            return new ReflectionHelper<T>(value);
        }

        /// <summary>
        /// Returns the specified object.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="helper">The helper.</param>
        /// <returns>The object.</returns>
        public static T Return<T>(this ReflectionHelper<T> helper)
        {
            Check.Current.ArgumentNullException(helper, nameof(helper));

            return helper.Value;
        }
    }
}
