// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashBag.cs" company=".">
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

    using NLib.Collections.Generic.Extensions;
    using NLib.Collections.Generic.Resources;

    /// <summary>
    /// Represents a collection of objects.
    /// </summary>
    /// <typeparam name="T">The type of elements in the bag.</typeparam>
    [Serializable]
    [DebuggerTypeProxy(typeof(BagBaseDebugView<>))]
    [DebuggerDisplay("Count = {Count}")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public class HashBag<T> : BagBase<T>
    {
        /// <summary>
        /// The equality comparer.
        /// </summary>
        private readonly EqualityComparison<T> equalityComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashBag{T}"/> class.
        /// </summary>
        public HashBag()
            : this(EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public HashBag(IEnumerable<T> collection)
            : this(collection, EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashBag{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public HashBag(IEqualityComparer<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashBag{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public HashBag(EqualityComparison<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public HashBag(IEnumerable<T> collection, IEqualityComparer<T> comparer)
            : this(collection, null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public HashBag(IEnumerable<T> collection, EqualityComparison<T> comparer)
            : this(collection, comparer, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashBag{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparison">The comparer. If null <paramref name="comparer"/> will be use.</param>
        /// <param name="comparer">The comparer. If null <paramref name="comparison"/> will be use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> and <paramref name="comparison"/> are null.</exception>
        protected HashBag(IEnumerable<T> collection, EqualityComparison<T> comparison, IEqualityComparer<T> comparer)
        {
            Check.Requires<ArgumentNullException>(comparer != null || comparison != null, CollectionResource.Initialize_ArgumentNullException_ComparerAndComparison);

            this.equalityComparer = comparison ?? comparer.Equals;
            this.Model = new Dictionary<T, int>(comparer ?? comparison.ToEqualityComparer());
            this.AddRange(collection);
        }

        /// <summary>
        /// Gets the unique set.
        /// </summary>
        public override ISet<T> UniqueSet
        {
            get { return new HashSet<T>(this, this.EqualityComparer.ToEqualityComparer()); }
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
