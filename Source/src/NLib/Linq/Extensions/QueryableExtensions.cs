using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace NLib.Linq.Extensions
{
    /// <summary>
    /// Defines extensions methods for <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Filters sequence that are between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IQueryable<TSource> Between<TSource>(this IQueryable<TSource> source, TSource min, TSource max)
        {
            return Between(source, x => x, min, max);
        }

        /// <summary>
        /// Filters sequence that are between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Like Linq API")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "CheckError class do the check")]
        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, TKey min, TKey max)
        {
            Check.Current.ArgumentNullException(source, nameof(source))
                         .ArgumentNullException(keySelector, nameof(keySelector));

            var key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            var lowerBound = Expression.LessThanOrEqual(Expression.Constant(min), key);
            var upperBound = Expression.LessThanOrEqual(key, Expression.Constant(max));
            var and = Expression.AndAlso(lowerBound, upperBound);
            var lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);

            return source.Where(lambda);
        }

        /// <summary>
        /// Paginates the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="page">The zero-based page number.</param>
        /// <param name="pageSize">Size of a page.</param>
        /// <returns>The subset of the collection.</returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> collection, int page, int pageSize)
        {
            Check.Current.ArgumentNullException(collection, nameof(collection));

            var skip = Math.Max(pageSize * page, 0);

            return collection.Skip(skip).Take(pageSize);
        }
    }
}
