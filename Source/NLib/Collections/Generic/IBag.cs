// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBag.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Defines methods to manipulate generic bag.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the bag.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public interface IBag<T> : ICollection<T>
    {
        /// <summary>
        /// Gets the Set of unique elements in the Bag.
        /// </summary>
        ISet<T> UniqueSet { get; }

        /// <summary>
        /// Adds nCopies copies of the specified object to the Bag.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="numberCopies">The number of copies to add.</param>
        void Add(T item, int numberCopies);

        /// <summary>
        /// Determines whether the specified <see cref="IEnumerable{T}"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="IEnumerable{T}"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="IEnumerable{T}"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        bool BagEquals(IEnumerable<T> other);

        /// <summary>
        /// Removes one occurrence of all elements in the specified collection from the current <see cref="IBag{T}"/> object.
        /// </summary>
        /// <param name="other">The collection of items to remove from the <see cref="IBag{T}"/> object.</param>
        /// <exception cref="ArgumentNullException">other is null.</exception>
        void ExceptWith(IEnumerable<T> other);

        /// <summary>
        /// Removes all occurrences of all elements in the specified collection from the current <see cref="IBag{T}"/> object.
        /// </summary>
        /// <param name="other">The collection of items to remove from the <see cref="IBag{T}"/> object.</param>
        /// <exception cref="ArgumentNullException">other is null.</exception>
        void ExceptAllWith(IEnumerable<T> other);

        /// <summary>
        /// Gets the number of occurrences (cardinality) of the given object currently in the bag.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>the number of occurrences of the item; zero if not found.</returns>
        int GetCount(T item);

        /// <summary>
        /// Modifies the current bag so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        void IntersectWith(IEnumerable<T> other);

        /// <summary>
        /// Determines whether the current bag is a property (strict) subbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag is a correct subbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        bool IsProperSubBagOf(IEnumerable<T> other);

        /// <summary>
        /// Determines whether the current bag is a correct superbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the <see cref="IBag{T}"/> object is a correct superbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag. </param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        bool IsProperSuperBagOf(IEnumerable<T> other);

        /// <summary>
        /// Determines whether a bag is a subbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag is a subbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        bool IsSubBagOf(IEnumerable<T> other);

        /// <summary>
        /// Determines whether the current bag is a superbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag is a superbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        bool IsSuperBagOf(IEnumerable<T> other);

        /// <summary>
        /// Determines whether the current bag overlaps with the specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag and <paramref name="other"/> share at least one common element; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        bool Overlaps(IEnumerable<T> other);

        /// <summary>
        /// Removes a certain occurrences number of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <param name="numberCopies">The number of copies to remove.</param>
        /// <returns>
        /// The occurrence number of <paramref name="item"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        int Remove(T item, int numberCopies);

        /// <summary>
        /// Removes all occurrences of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// The occurrence number of <paramref name="item"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        int RemoveAll(T item);

        /// <summary>
        /// Modifies the current bag so that it contains only elements that are present either in the current bag or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        void SymmetricExceptWith(IEnumerable<T> other);

        /// <summary>
        /// Modifies the current bag so that it contains all elements that are present in both the current sbag and in the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        void UnionWith(IEnumerable<T> other);
    }
}
