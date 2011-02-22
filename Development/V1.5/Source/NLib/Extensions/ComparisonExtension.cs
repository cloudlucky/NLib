// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComparisonExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines extensions methods for <see cref="Comparison{T}"/>.
    /// </summary>
    public static class ComparisonExtension
    {
        /// <summary>
        /// Convert <see cref="Comparison{T}"/> to <see cref="IComparer{T}"/>.
        /// </summary>
        /// <typeparam name="T">The compare type.</typeparam>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="Comparison{T}"/> converted to <see cref="IComparer{T}"/>.</returns>
        public static IComparer<T> ToComparer<T>(this Comparison<T> comparison)
        {
            return new Comparer<T>(comparison);
        }

        /// <summary>
        /// Util class to convert <see cref="Comparison{T}"/> to <see cref="IComparer{T}"/>.
        /// </summary>
        /// <typeparam name="T">The compare type.</typeparam>
        private class Comparer<T> : IComparer<T>
        {
            /// <summary>
            /// The comparison to use.
            /// </summary>
            private readonly Comparison<T> comparer;

            /// <summary>
            /// Initializes a new instance of the <see cref="Comparer{T}"/> class.
            /// </summary>
            /// <param name="comparer">The comparer.</param>
            public Comparer(Comparison<T> comparer)
            {
                Check.Requires<ArgumentNullException>(comparer != null, new { paramName = "comparer" });

                this.comparer = comparer;
            }

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>
            /// Value Condition 
            ///     Less than zero <paramref name="x"/> is less than <paramref name="y"/>.
            ///     Zero <paramref name="x"/> equals <paramref name="y"/>.
            ///     Greater than zero <paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            public int Compare(T x, T y)
            {
                return this.comparer(x, y);
            }
        }
    }
}
