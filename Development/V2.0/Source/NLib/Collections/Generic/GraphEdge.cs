// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphEdge.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    /// <summary>
    /// Represents a directed edge between two nodes.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public class GraphEdge<T, TCost> : IGraphEdge<T, TCost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge{T, TCost}"/> class.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        public GraphEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
            : this(from, to, default(TCost))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge{T, TCost}"/> class.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        /// <param name="value">The value of the value.</param>
        public  GraphEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
        {
            this.From = from;
            this.To = to;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge{T, TCost}"/> class.
        /// </summary>
        protected GraphEdge()
        { 
        }

        /// <summary>
        /// Gets or sets the from node.
        /// </summary>
        public GraphNode<T, TCost> From { get; protected set; }

        /// <summary>
        /// Gets the from node.
        /// </summary>
        IGraphNode<T, TCost> IGraphEdge<T, TCost>.From
        {
            get { return this.From; }
        }

        /// <summary>
        /// Gets or sets the to node.
        /// </summary>
        public GraphNode<T, TCost> To { get; protected set; }

        /// <summary>
        /// Gets the to node.
        /// </summary>
        IGraphNode<T, TCost> IGraphEdge<T, TCost>.To
        {
            get { return this.To; }
        }

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public virtual TCost Value { get; set; }

        /// <summary>
        /// Gets or sets Marked
        /// </summary>
        public bool Marked { get; set; }
    }
}
