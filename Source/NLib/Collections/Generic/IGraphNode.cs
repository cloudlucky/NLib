// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGraphNode.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides the base interface for the abstraction of a graph node.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public interface IGraphNode<T, TCost> : INode<T>
    {
        /// <summary>
        /// Gets the edges.
        /// </summary>
        IEnumerable<IGraphEdge<T, TCost>> Edges { get; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GraphNode{T, TCost}"/> is marker.
        /// </summary>
        /// <value>
        ///   <c>true</c> if marker; otherwise, <c>false</c>.
        /// </value>
        bool Marker { get; set; }
    }
}