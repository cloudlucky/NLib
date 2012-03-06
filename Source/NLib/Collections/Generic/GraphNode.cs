// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphNode.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a node in a graph.  A graph node contains some piece of data, along with a set of
    /// neighbors. There can be an optional cost between a graph node and each of its neighbors.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph node.</typeparam>
    /// /// <typeparam name="TCost">The type of cost.</typeparam>
    public class GraphNode<T, TCost> : IGraphNode<T, TCost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode{T, TCost}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public GraphNode(T value)
        {
            this.Value = value;
            this.Edges = new List<GraphEdge<T, TCost>>();
            this.Marker = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode{T, TCost}"/> class.
        /// </summary>
        protected GraphNode()
        {
        }

        /// <summary>
        /// Gets or sets the edges.
        /// </summary>
        public ICollection<GraphEdge<T, TCost>> Edges { get; protected set; }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        IEnumerable<IGraphEdge<T, TCost>> IGraphNode<T, TCost>.Edges
        {
            get { return this.Edges; }
        }

        /// <summary>
        /// Gets the neighbors.
        /// </summary>
        IEnumerable<INode<T>> INode<T>.Neighbors
        {
            get { return this.Edges.Select(x => x.To); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GraphNode{T, TCost}"/> is marker.
        /// </summary>
        /// <value>
        ///   <c>true</c> if marker; otherwise, <c>false</c>.
        /// </value>
        public bool Marker { get; set; }

    }
}
