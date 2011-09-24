// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Graph.Complex.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;
    using System.Numerics;

    public class Graph<T> : Graph<T, Complex>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        public Graph()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEqualityComparer<T> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public Graph(EqualityComparison<T> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEnumerable<T> collection, IEqualityComparer<T> comparer)
            : base(collection, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEnumerable<T> collection, EqualityComparison<T> comparer)
            : base(collection, comparer)
        {
        }
    }
}