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

    /// <summary>
    /// Represents a node in a graph.  A graph node contains some piece of data, along with a set of
    /// neighbors. There can be an optional cost between a graph node and each of its neighbors.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph node.</typeparam>
    public class GraphNode<T, TCost>
    {
        protected GraphNode()
        {
        }

        public GraphNode(T value)
        {
            this.Value = value;
            this.Edges = new GraphEdgeSet<T, TCost>();
        }

        public GraphEdgeSet<T, TCost> Edges { get; protected set; }

        public T Value { get; protected set; }

        public TCost this[T to]
        {
            get { return this.Edges[to]; }
            set { this.Edges[to] = value; }
        }
    }
}