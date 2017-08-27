namespace NLib.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Defines extensions methods for <see cref="MemberInfo"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified <see cref="string"/> object occurs within this string.
        /// </summary>
        /// <param name="text">The string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <returns>
        /// true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public static bool Contains(this string text, string value, StringComparison comparisonType)
        {
            Check.Current.ArgumentNullException(text, "text");

            if (string.IsNullOrEmpty(value))
            { 
                return true;
            }

            return text.IndexOf(value, comparisonType) >= 0;
        }
    }
}
