// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRedBlackTreeNode.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;

    public interface IGraph<T> : IEnumerable<GraphNode<T>>
    {   
        

        /// <summary>
        /// Adds a new GraphNode instance to the Graph
        /// </summary>
        void AddNode(GraphNode<T> node);

        /// <summary>
        /// Adds a new GraphNode with value to the graph.
        /// </summary>
        void AddNode(T value);
        
        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        void AddDirectedEdge(T from, T to);
        
         /// <summary>
         /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        void AddDirectedEdge(T from, T to, object cost);
        
        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        void AddUndirectedEdge(T from, T to);
        
        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        void AddUndirectedEdge(T from, T to, object cost);

        /// <summary>
        /// Clears out the contents of the Graph.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns a Boolean, indicating if a particular value exists within the graph.
        /// </summary>
        bool Contains(T value);

        /// <summary>
        /// Attempts to remove a node from a graph.
        /// </summary>
        /// <param name="value">
        /// The name of the node.
        /// </param>
        /// <returns>
        /// Return true if the node is removed.
        /// </returns>
        bool Remove(T value);
         
    }
}
