// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGraph.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;

    public interface IGraph<T, TCost> : ICollection<T>
    {
        GraphNode<T, TCost> this[T item] { get; }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        void AddDirectedEdge(T from, T to);
        
         /// <summary>
         /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        void AddDirectedEdge(T from, T to, TCost cost);
        
        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        void AddUndirectedEdge(T from, T to);
        
        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        void AddUndirectedEdge(T from, T to, TCost cost);

        GraphNode<T, TCost> GetNodeByValue(T value);

        bool TryGetNode(T item, out GraphNode<T, TCost> node);
    }
}
