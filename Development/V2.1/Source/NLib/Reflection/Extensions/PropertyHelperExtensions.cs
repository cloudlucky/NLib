namespace NLib.Reflection.Extensions
{
    using System;

    /// <summary>
    /// Defines extensions methods for <see cref="PropertyHelper{T, TKey}"/>.
    /// </summary>
    public static class PropertyHelperExtensions
    {
        /// <summary>
        /// Sets the value of the property with optional index values for index properties.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <typeparam name="TKey">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="value">The value.</param>
        /// <param name="index">The index.</param>
        /// <returns>The reflection helper.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is null.</exception>
        public static ReflectionHelper<T> SetValue<T, TKey>(this PropertyHelper<T, TKey> helper, TKey value, params object[] index)
        {
            Check.Current.ArgumentNullException(helper, "helper");

            helper.PropertyInfo.SetValue(helper.ReflectionHelper.Value, value, index);

            return helper.ReflectionHelper;
        }

        /// <summary>
        /// Gets the value of the property with optional index values for indexed properties.
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <typeparam name="TKey">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="helper"/> is null.</exception>
        public static TKey GetValue<T, TKey>(this PropertyHelper<T, TKey> helper, params object[] index)
        {
            return (TKey)helper.PropertyInfo.GetValue(helper.ReflectionHelper.Value, index);
        }
    }
}
