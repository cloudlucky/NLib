using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using NLib.Collections.Generic.Resources;
using NLib.Extensions;

namespace NLib.Collections.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a collection of objects that is maintained in sorted order.
    /// </summary>
    /// <typeparam name="T">The type of elements in the bag.</typeparam>
    [Serializable]
    [DebuggerTypeProxy(typeof(BagBaseDebugView<>))]
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public class SortedBag<T> : BagBase<T>
    {
        /// <summary>
        /// The comparison.
        /// </summary>
        private readonly Comparison<T> currentComparer;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.SortedBag`1" /> class.
        /// </summary>
        public SortedBag()
            : this(Comparer<T>.Default)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.SortedBag`1" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public SortedBag(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.SortedBag`1" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(IComparer<T> comparer)
            : this(null, comparer)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.SortedBag`1" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(Comparison<T> comparer)
            : this(null, comparer)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.SortedBag`1" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public SortedBag(IEnumerable<T> collection, IComparer<T> comparer)
            : this(collection, null, comparer)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.SortedBag`1" /> class.
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
            Check.Current.Requires<ArgumentNullException>(comparer != null || comparison != null, CollectionResource.Initialize_ArgumentNullException_ComparerAndComparison);

            this.currentComparer = comparison ?? comparer.Compare;
            this.EqualityComparer = (x, y) => this.currentComparer(x, y) == 0;
            this.Model = new SortedDictionary<T, int>(comparer ?? comparison.ToComparer());
            this.AddRange(collection);
        }

        /// <inheritdoc />
        public override ISet<T> UniqueSet => new SortedSet<T>(this, this.currentComparer.ToComparer());

        /// <inheritdoc />
        protected override EqualityComparison<T> EqualityComparer { get; }
    }
}