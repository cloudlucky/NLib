namespace NLib.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines extensions methods for <see cref="Comparison{T}"/>.
    /// </summary>
    public static class ComparisonExtensions
    {
        /// <summary>
        /// Convert <see cref="Comparison{T}"/> to <see cref="IComparer{T}"/>.
        /// </summary>
        /// <typeparam name="T">The compare type.</typeparam>
        /// <param name="comparison">The comparison.</param>
        /// <returns>The <see cref="Comparison{T}"/> converted to <see cref="IComparer{T}"/>.</returns>
        /// <remarks>It uses <see cref="Comparer{T}.Create(Comparison{T})"/> method.</remarks>
        public static IComparer<T> ToComparer<T>(this Comparison<T> comparison)
        {
            return Comparer<T>.Create(comparison);
        }
    }
}
