using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using NLib.Collections.Generic.Extensions;
using NLib.Collections.Generic.Resources;

namespace NLib.Collections.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a collection of objects.
    /// </summary>
    /// <typeparam name="T">The type of elements in the bag.</typeparam>
    [Serializable]
    [DebuggerTypeProxy(typeof(BagBaseDebugView<>))]
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public class HashBag<T> : BagBase<T>
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.HashBag`1" /> class.
        /// </summary>
        public HashBag()
            : this(EqualityComparer<T>.Default)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.HashBag`1" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public HashBag(IEnumerable<T> collection)
            : this(collection, EqualityComparer<T>.Default)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.HashBag`1" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public HashBag(IEqualityComparer<T> comparer)
            : this(null, comparer)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.HashBag`1" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public HashBag(EqualityComparison<T> comparer)
            : this(null, comparer)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.HashBag`1" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public HashBag(IEnumerable<T> collection, IEqualityComparer<T> comparer)
            : this(collection, null, comparer)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.Collections.Generic.HashBag`1" /> class.
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
            Check.Current.Requires<ArgumentNullException>(comparer != null || comparison != null, CollectionResource.Initialize_ArgumentNullException_ComparerAndComparison);

            this.EqualityComparer = comparison ?? comparer.Equals;
            this.Model = new Dictionary<T, int>(comparer ?? comparison.ToEqualityComparer());
            this.AddRange(collection);
        }

        /// <inheritdoc />
        public override ISet<T> UniqueSet => new HashSet<T>(this, this.EqualityComparer.ToEqualityComparer());

        /// <inheritdoc />
        protected override EqualityComparison<T> EqualityComparer { get; }
    }
}
