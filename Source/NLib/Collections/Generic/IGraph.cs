namespace NLib.Collections.Generic
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///   Provides the base interface for the abstraction of graph.
    /// </summary>
    /// <typeparam name = "T">The type of data stored in the graph's nodes.</typeparam>
    /// <typeparam name = "TCost">The type of cost.</typeparam>
    public interface IGraph<T, TCost> : ICollection<T>
    {
        /// <summary>
        /// Gets the nodes of the graph.
        /// </summary>
        IEnumerable<IGraphNode<T, TCost>> Nodes { get; }

        /// <summary>
        /// Gets the <see cref = "IGraphNode{T, TCost}" /> with the specified item.
        /// </summary>
        /// <param name="item">The item to search.</param>
        /// <returns>The item if found; otherwise null.</returns>
        IGraphNode<T, TCost> this[T item] { get; }

        /// <summary>
        /// Gets or sets the <see cref = "TCost" /> with the specified from.
        /// </summary>
        /// <param name="from">The from item.</param>
        /// <param name="to">The to item.</param>
        /// <returns>The the cost of the edge between the from and the to items.</returns>
        TCost this[T from, T to] { get; set; }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to) with an associated cost.
        /// </summary>
        /// <param name="from">The value of the graph node from which the directed edge emanates.</param>
        /// <param name="to">The value of the graph node to which the edge leads.</param>
        /// <param name="cost">The cost of the edge <paramref name="from"/> to <paramref name="to"/>.</param>
        void AddDirectedEdge(T from, T to, TCost cost = default(TCost));

        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to) with an associated cost.
        /// </summary>
        /// <param name="from">The from value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="to">The to value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="cost">The cost of the undirected edge.</param>
        void AddUndirectedEdge(T from, T to, TCost cost = default(TCost));

        /// <summary>
        /// Add an edge in Graph
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="graph">The graph that is joined by the edge.</param>
        void AddEdge(IGraphEdge<T, TCost> edge, IGraph<T, TCost> graph = null);

        /// <summary>
        /// Adds the elements of the specified collection in the bag.
        /// </summary>
        /// <param name="collection">The collection.</param>
        void AddRange(IEnumerable<T> collection);

        /// <summary>
        /// Selecting some node as the root and to go through them level-by-level.
        /// </summary>
        /// <param name="item">The some root node for start iteration.</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        IEnumerable<IGraphNode<T, TCost>> BreathFirstTraversal(IGraphNode<T, TCost> item);

        /// <summary>
        /// Selecting some node as the root and to go through them level-by-level.
        /// </summary>
        /// <param name="item">The value of some root node for start iteration</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        IEnumerable<IGraphNode<T, TCost>> BreathFirstTraversal(T item);

        /// <summary>
        /// Selecting some node as the root and explores as far as possible along each branch before backtracking.
        /// </summary>
        /// <param name="item">The some node for start iteration.</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        IEnumerable<IGraphNode<T, TCost>> DepthFirstTraversal(IGraphNode<T, TCost> item);

        /// <summary>
        /// Selecting some node as the root and explores as far as possible along each branch before backtracking.
        /// </summary>
        /// <param name="item">The value of some root node for start iteration.</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        IEnumerable<IGraphNode<T, TCost>> DepthFirstTraversal(T item);

        /// <summary>
        /// Gets the edge between the two nodes.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        /// <returns>The edge between the two nodes; otherwise null;</returns>
        IGraphEdge<T, TCost> GetEdge(T from, T to);

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The node; otherwise null.</returns>
        IGraphNode<T, TCost> GetNode(T item);

        /// <summary>
        /// Remove node from GraphNode 
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>If a node is removed then return true.</returns>
        bool RemoveNode(IGraphNode<T, TCost> node);

        /// <summary>
        /// Remove an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        /// <param name="edge">The direct edge that we must removed from the graph.</param>
        /// <param name="graph">The name of graph to remove.</param>
        void RemoveEdge(IGraphEdge<T, TCost> edge, IGraph<T, TCost> graph = null);

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref = "ICollection{T}" />.
        /// </summary>
        /// <param name="edge">The object to remove from the <see cref = "ICollection{T}" />.</param>
        /// <exception cref = "NotSupportedException">The <see cref = "ICollection{T}" /> is read-only.</exception>
        void RemoveDirectedEdge(IGraphEdge<T, TCost> edge);

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref = "ICollection{T}" />.
        /// </summary>
        /// <param name="edge">The object to remove from the <see cref = "ICollection{T}" />.</param>
        /// <exception cref = "NotSupportedException">The <see cref = "ICollection{T}" /> is read-only.</exception>
        void RemoveUndirectedEdge(IGraphEdge<T, TCost> edge);

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A copy of your graph.</returns>
        object Clone();

    }
}