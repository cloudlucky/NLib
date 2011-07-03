using System;
using System.Collections.Generic;
using System.Text;

namespace NLib.Collections.Generic
{
    /// <summary>
    /// Represents a node in a graph.  A graph node contains some piece of data, along with a set of
    /// neighbors.  There can be an optional cost between a graph node and each of its neighbors.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph node.</typeparam>
    public class GraphNode<T>
    {
        public GraphNode() { }
        public GraphNode(T value) : this(value, null) { }
        public GraphNode(T value,  GraphNodeList<T> neighbors)
        {
            this.Value = value;
            this.Neighbors = neighbors ?? new GraphNodeList<T>();
            this.Costs = new List<int>();
        }

        public T Value { get; set; }

        /// <summary>
        /// Returns the set of neighbors for this graph node.
        /// </summary>
        public GraphNodeList<T> Neighbors { get; private set; }

        /// <summary>
        /// Returns the set of costs for the edges eminating from this graph node.
        /// The k<sup>th</sup> cost (Cost[k]) represents the cost from the graph node to the node
        /// represented by its k<sup>th</sup> neighbor (Neighbors[k]).
        /// </summary>
        /// <value></value>
        public List<int> Costs { get; set; }
    }
}
