// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Defines extensions methods for <see cref="MemberInfo"/>.
    /// </summary>
    public static class StringExtension
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
            Check.ArgumentNullException(text, "text");

            if (string.IsNullOrEmpty(value))
            { 
                return true;
            }

            return text.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// 
        /// The longest common subsequence (Lcs) problem is to find the longest subsequence common to all sequences in a set of sequences
        /// </summary>
        /// <param name="str1">
        /// The sequence
        /// </param>
        /// <param name="str2">
        /// The subsequece
        /// </param>
        /// <param name="comparisonType">
        /// The comparison Type.
        /// </param>
        /// 
        /// <returns>
        /// The lcs.
        /// </returns>
        public static int Lcs(string str1, string str2, StringComparison comparisonType)
        {
            return 0;
        }

    }
}
