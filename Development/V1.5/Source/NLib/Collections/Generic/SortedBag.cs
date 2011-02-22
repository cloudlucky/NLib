﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortedBag.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using NLib.Collections.Generic.Extensions;
    using NLib.Collections.Generic.Resources;
    using NLib.Extensions;

    /// <summary>
    /// Represents a collection of objects that is maintained in sorted order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the bag.</typeparam>
    [Serializable]
    [DebuggerTypeProxy(typeof(SortedBagDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public class SortedBag<T> : IBag<T>
    {
        /// <summary>
        /// The current comparer.
        /// </summary>
        private Comparison<T> currentComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        public SortedBag()
            : this(Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public SortedBag(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(IComparer<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(Comparison<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(IEnumerable<T> collection, IComparer<T> comparer)
        {
            Check.Requires<ArgumentNullException>(comparer != null, new { paramName = "comparer" });

            this.Initialize(collection, null, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(IEnumerable<T> collection, Comparison<T> comparer)
        {
            Check.Requires<ArgumentNullException>(comparer != null, new { paramName = "comparer" });

            this.Initialize(collection, comparer, null);
        }

        /// <summary>
        /// Gets or sets the number of elements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}"/> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="ICollection{T}"/> is read-only; otherwise, false.
        /// </returns>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the Set of unique elements in the Bag.
        /// </summary>
        public ISet<T> UniqueSet
        {
            get { return new SortedSet<T>(this, this.currentComparer.ToComparer()); }
        }

        /// <summary>
        /// Gets the implementation model.
        /// </summary>
        protected IDictionary<T, int> Model { get; private set; }

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public void Add(T item)
        {
            this.Add(item, 1);
        }

        /// <summary>
        /// Adds nCopies copies of the specified object to the Bag.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="numberCopies">The number of copies to add.</param>
        public void Add(T item, int numberCopies)
        {
            if (this.Contains(item))
            {
                this.Model[item] += numberCopies;
            }
            else
            {
                this.Model.Add(item, numberCopies);
            }

            this.Count += numberCopies;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IBag{T}"/>.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                collection.ForEach(this.Add);
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="IEnumerable{T}"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="IEnumerable{T}"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="IEnumerable{T}"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool BagEquals(IEnumerable<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Model.Keys.All(key => other.Count(x => this.currentComparer(x, key) == 0) == this.GetCount(key));
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public virtual void Clear()
        {
            this.Model.Clear();
            this.Count = 0;
        }

        /// <summary>
        /// Determines whether the <see cref="ICollection{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="ICollection{T}"/>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return this.Model.ContainsKey(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="array"/> is multidimensional.
        ///     -or-
        ///     <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        ///     -or-
        ///     The number of elements in the source <see cref="ICollection{T}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        ///     -or-
        ///     Type <paramref name="array"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            CheckError.ArgumentNullException(array, "array");
            Check.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, SortedBagResource.CopyTo_ArgumentOutOfRangeException_ArrayIndex, new { paramName = "arrayIndex" });
            Check.Requires<ArgumentException>(arrayIndex < array.Length && arrayIndex + this.Count <= array.Length, SortedBagResource.CopyTo_ArgumentException_Array, new { paramName = "array" });

            if (this.Count > 0)
            {
                this.ForEach(i => array[arrayIndex++] = i);
            }
        }

        /// <summary>
        /// Removes all occurrences of all elements in the specified collection from the current <see cref="IBag{T}"/> object.
        /// </summary>
        /// <param name="other">The collection of items to remove from the <see cref="IBag{T}"/> object.</param>
        /// <exception cref="ArgumentNullException">other is null.</exception>
        public void ExceptAllWith(IEnumerable<T> other)
        {
            other.ForEach(i => this.RemoveAll(i));
        }

        /// <summary>
        /// Removes one occurrence of all elements in the specified collection from the current <see cref="IBag{T}"/> object.
        /// </summary>
        /// <param name="other">The collection of items to remove from the <see cref="IBag{T}"/> object.</param>
        /// <exception cref="ArgumentNullException">other is null.</exception>
        public void ExceptWith(IEnumerable<T> other)
        {
            other.ForEach(i => this.Remove(i));
        }

        /// <summary>
        /// Gets the number of occurrences (cardinality) of the given object currently in the bag.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>the number of occurrences of the item; zero if not found.</returns>
        public int GetCount(T item)
        {
            return this.Model.ContainsKey(item) ? this.Model[item] : 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var n in this.Model)
            {
                for (var i = 0; i < n.Value; ++i)
                {
                    yield return n.Key;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Modifies the current bag so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public void IntersectWith(IEnumerable<T> other)
        {
            CheckError.ArgumentNullException(other, "other");
            
            foreach (var t in other)
            {
                if (!this.Contains(t))
                {
                    this.RemoveAll(t);
                }
                else
                {
                    var t1 = t;
                    var count = other.Count(x => this.currentComparer(x, t1) == 0);
                    var total = this.GetCount(t);

                    if (total > count)
                    {
                        this.Model[t] = count;
                        this.Count -= total - count;
                    }
                }
            }

            var i = 0;
            while (i < this.Model.Keys.Count)
            {
                var key = this.Model.Keys.ElementAt(i);
                if (!other.Contains(key))
                {
                    this.RemoveAll(key);
                }
                else
                {
                    ++i;
                }
            }
        }

        /// <summary>
        /// Determines whether the current bag is a property (strict) subbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag is a correct subbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        public bool IsProperSubBagOf(IEnumerable<T> other)
        {
            return this.IsSubBagOf(other)
                   && this.All(x => other.Contains(x));
        }

        /// <summary>
        /// Determines whether the current bag is a correct superbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the <see cref="IBag{T}"/> object is a correct superbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag. </param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        public bool IsProperSuperBagOf(IEnumerable<T> other)
        {
            return this.IsSuperBagOf(other);
        }

        /// <summary>
        /// Determines whether a bag is a subbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag is a subbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        public bool IsSubBagOf(IEnumerable<T> other)
        {
            return other.All(x => this.Contains(x) && this.Model[x] <= other.Count(y => this.currentComparer(x, y) == 0));
        }

        /// <summary>
        /// Determines whether the current bag is a superbag of a specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag is a superbag of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        public bool IsSuperBagOf(IEnumerable<T> other)
        {
            return other.All(x => this.Contains(x) && this.Model[x] >= other.Count(y => this.currentComparer(x, y) == 0));
        }

        /// <summary>
        /// Determines whether the current bag overlaps with the specified collection.
        /// </summary>
        /// <returns>
        /// true if the current bag and <paramref name="other"/> share at least one common element; otherwise, false.
        /// </returns>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        public bool Overlaps(IEnumerable<T> other)
        {
            return other.All(this.Contains);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="ICollection{T}"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="ICollection{T}"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public bool Remove(T item)
        {
            return this.Remove(item, 1) == 1;
        }

        /// <summary>
        /// Removes a certain occurrences number of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <param name="numberCopies">The number of copies to remove.</param>
        /// <returns>
        /// The occurrence number of <paramref name="item"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public int Remove(T item, int numberCopies)
        {
            if (!this.Contains(item))
            {
                return 0;
            }

            var before = this.Model[item];
            this.Model[item] -= numberCopies;
            var remove = false;

            if (this.Model[item] <= 0)
            {
                this.Model[item] = 0;
                remove = true;
            }

            var result = before - this.Model[item];
            this.Count -= result;

            if (remove)
            {
                this.Model.Remove(item);
            }

            return result;
        }

        /// <summary>
        /// Removes all occurrences of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// The occurrence number of <paramref name="item"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public int RemoveAll(T item)
        {
            if (!this.Contains(item))
            {
                return 0;
            }

            var result = this.Model[item];
            this.Model.Remove(item);
            this.Count -= result;

            return result;
        }

        /// <summary>
        /// Modifies the current bag so that it contains only elements that are present either in the current bag or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            CheckError.ArgumentNullException(other, "other");

            var tmp = new List<T>();

            foreach (var t in other)
            {
                if (this.Contains(t))
                {
                    this.RemoveAll(t);
                }
                else
                {
                    tmp.Add(t);
                }
            }

            this.AddRange(tmp);
        }

        /// <summary>
        /// Modifies the current bag so that it contains all elements that are present in both the current bag and in the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current bag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="other"/> is null.</exception>
        public void UnionWith(IEnumerable<T> other)
        {
            this.AddRange(other);
        }

        /// <summary>
        /// Initializes the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparison">The comparer. If null <paramref name="comparer"/> will be use.</param>
        /// <param name="comparer">The comparer. If null <paramref name="comparison"/> will be use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> and <paramref name="comparison"/> are null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2", Justification = "CheckError class do the check")]
        private void Initialize(IEnumerable<T> collection, Comparison<T> comparison, IComparer<T> comparer)
        {
            Check.Requires<ArgumentNullException>(comparer != null || comparison != null, SortedBagResource.Initialize_ArgumentNullException_ComparerAndComparison);

            this.currentComparer = comparison ?? comparer.Compare;
            this.Model = new SortedDictionary<T, int>(comparer ?? comparison.ToComparer());
            this.AddRange(collection);
        }
    }
}
