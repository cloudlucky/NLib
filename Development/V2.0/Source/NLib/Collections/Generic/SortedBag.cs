// --------------------------------------------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    using NLib.Collections.Generic.Resources;
    using NLib.Extensions;

    /// <summary>
    /// Represents a collection of objects that is maintained in sorted order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the bag.</typeparam>
    [Serializable]
    [DebuggerTypeProxy(typeof(BagBaseDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public class SortedBag<T> : BagBase<T>
    {
        /// <summary>
        /// The comparison.
        /// </summary>
        private readonly Comparison<T> currentComparer;

        /// <summary>
        /// The equality comparer.
        /// </summary>
        private readonly EqualityComparison<T> equalityComparer;

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
            : this(collection, null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(IEnumerable<T> collection, Comparison<T> comparer)
            : this(collection, comparer, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortedBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparison">The comparer. If null <paramref name="comparer"/> will be use.</param>
        /// <param name="comparer">The comparer. If null <paramref name="comparison"/> will be use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> and <paramref name="comparison"/> are null.</exception>
        protected SortedBag(IEnumerable<T> collection, Comparison<T> comparison, IComparer<T> comparer)
        {
            Check.Requires<ArgumentNullException>(comparer != null || comparison != null, CollectionResource.Initialize_ArgumentNullException_ComparerAndComparison);

            this.currentComparer = comparison ?? comparer.Compare;
            this.equalityComparer = (x, y) => this.currentComparer(x, y) == 0;
            this.Model = new SortedDictionary<T, int>(comparer ?? comparison.ToComparer());
            this.AddRange(collection);
        }

        /// <summary>
        /// Gets the unique set.
        /// </summary>
        public override ISet<T> UniqueSet
        {
            get { return new SortedSet<T>(this, this.currentComparer.ToComparer()); }
        }

        /// <summary>
        /// Gets the equality comparer.
        /// </summary>
        protected override EqualityComparison<T> EqualityComparer
        {
            get { return this.equalityComparer; }
        }
    }
}