// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualityComparisonExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines extensions methods for <see cref="Comparison{T}"/>.
    /// </summary>
    public static class EqualityComparisonExtension
    {
        /// <summary>
        /// Convert <see cref="EqualityComparison{T}"/> to <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <typeparam name="T">The compare type.</typeparam>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="Comparison{T}"/> converted to <see cref="IComparer{T}"/>.</returns>
        public static IEqualityComparer<T> ToEqualityComparer<T>(this EqualityComparison<T> comparison)
        {
            return new EqualityComparer<T>(comparison);
        }

        /// <summary>
        /// Util class to convert <see cref="Comparison{T}"/> to <see cref="IComparer{T}"/>.
        /// </summary>
        /// <typeparam name="T">The compare type.</typeparam>
        private class EqualityComparer<T> : IEqualityComparer<T>
        {
            /// <summary>
            /// The comparison to use.
            /// </summary>
            private readonly EqualityComparison<T> comparer;

            /// <summary>
            /// Initializes a new instance of the <see cref="EqualityComparer{T}"/> class.
            /// </summary>
            /// <param name="comparer">The comparer.</param>
            public EqualityComparer(EqualityComparison<T> comparer)
            {
                Check.Requires<ArgumentNullException>(comparer != null, new { paramName = "comparer" });

                this.comparer = comparer;
            }

            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <param name="x">The first object of type <paramref name="x"/> to compare.</param>
            /// <param name="y">The second object of type <paramref name="y"/> to compare.</param>
            /// <returns>
            /// true if the specified objects are equal; otherwise, false.
            /// </returns>
            public bool Equals(T x, T y)
            {
                return this.comparer(x, y);
            }

            /// <summary>
            /// Returns a hash code for the specified object.
            /// </summary>
            /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param>
            /// <returns>
            /// A hash code for the specified object.
            /// </returns>
            /// <exception cref="ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
            public int GetHashCode(T obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
