using System;
using System.Globalization;

namespace NLib.Extensions
{
    /// <summary>
    /// Defines extensions methods for <see cref="Convert.ChangeType(object, Type)"/>.
    /// </summary>
    public static class ConvertExtensions
    {
        /// <summary>
        /// Returns an <typeparamref name="T"/> with the specified <typeparamref name="T"/> and whose value is equivalent to the specified object.
        /// </summary>
        /// <typeparam name="T">The type to convert.</typeparam>
        /// <param name="value">An <see cref="object"/> that implements the <see cref="IConvertible"/> interface.</param>
        /// <returns>
        /// An object whose <see cref="Type"/> is <typeparamref name="T"/> and whose value is equivalent to <paramref name="value"/>.
        /// -or-
        /// null, if <paramref name="value"/> is null and <typeparamref name="T"/> is not a value type.
        /// </returns>
        /// <exception cref="InvalidCastException">This conversion is not supported.
        /// -or-
        /// <paramref name="value"/> is null and <typeparamref name="T"/> is a value type.
        /// </exception>
        [CLSCompliant(false)]
        public static T ChangeType<T>(this object value) where T : IConvertible
        {
            return ChangeType<T>(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns an <typeparamref name="T"/> with the specified <typeparamref name="T"/> and whose value is equivalent to the specified object.
        /// </summary>
        /// <typeparam name="T">The type to convert.</typeparam>
        /// <param name="value">An <see cref="object"/> that implements the <see cref="IConvertible"/> interface.</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An object whose <see cref="Type"/> is <typeparamref name="T"/> and whose value is equivalent to <paramref name="value"/>.
        /// -or-
        /// null, if <paramref name="value"/> is null and <typeparamref name="T"/> is not a value type.
        /// </returns>
        /// <exception cref="InvalidCastException">This conversion is not supported.
        /// -or-
        /// <paramref name="value"/> is null and <typeparamref name="T"/> is a value type.
        /// </exception>
        [CLSCompliant(false)]
        public static T ChangeType<T>(this object value, IFormatProvider provider) where T : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T), provider);
        }
    }
}
