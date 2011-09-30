// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Graph.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic.Extensions;
    using NLib.Collections.Generic.Resources;

    /// <summary>
    /// Represents a graph with an arbitrary collection of GraphNode instances. 
    /// Provides methods to search and manipulate the tree.
    /// </summary>
    /// <remarks>
    /// For more information about Graph, see <![CDATA[http://msdn.microsoft.com/en-us/library/ms379574(VS.80).aspx]]>.
    /// </remarks>
    /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
    /// <typeparam name="TCost">The type of cost.</typeparam>
    public class Graph<T, TCost> : IGraph<T, TCost>
    {

        /// <summary>
        /// The set of nodes in the graph
        /// </summary>
        private readonly HashSet<GraphNode<T, TCost>> nodeSet;

        /// <summary>
        /// The equality comparer.
        /// </summary>
        private readonly EqualityComparison<T> equalityComparison;

        /// <summary>
        /// The equality comparer.
        /// </summary>
        private readonly IEqualityComparer<T> equalityComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}"/> class.
        /// </summary>
        public Graph()
            : this(EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEqualityComparer<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public Graph(EqualityComparison<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEnumerable<T> collection, IEqualityComparer<T> comparer)
            : this(collection, null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEnumerable<T> collection, EqualityComparison<T> comparer)
            : this(collection, comparer, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparison">The comparer. If null <paramref name="comparer"/> will be use.</param>
        /// <param name="comparer">The comparer. If null <paramref name="comparison"/> will be use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> and <paramref name="comparison"/> are null.</exception>
        protected Graph(IEnumerable<T> collection, EqualityComparison<T> comparison, IEqualityComparer<T> comparer)
        {
            Check.Requires<ArgumentNullException>(comparer != null || comparison != null, CollectionResource.Initialize_ArgumentNullException_ComparerAndComparison);

            this.equalityComparison = comparison ?? comparer.Equals;
            this.equalityComparer = comparer ?? comparison.ToEqualityComparer();
            this.nodeSet = new HashSet<GraphNode<T, TCost>>(new GraphNodeEqualityComparer(this.equalityComparison));
            this.AddRange(collection);
         
        }

        private class GraphNodeEqualityComparer : IEqualityComparer<GraphNode<T, TCost>>
        {
            private readonly EqualityComparison<T> comparison;

            public GraphNodeEqualityComparer(EqualityComparison<T> comparison)
            {
                this.comparison = comparison;
            }

            public bool Equals(GraphNode<T, TCost> x, GraphNode<T, TCost> y)
            {
                return this.comparison(x.Value, y.Value);
            }

            public int GetHashCode(GraphNode<T, TCost> obj)
            {
                return obj.Value.GetHashCode();
            }
        }

        private class EdgeEqualityComparer : IEqualityComparer<GraphEdge<T, TCost>>
        {
            private readonly EqualityComparison<T> comparison;

            public EdgeEqualityComparer(EqualityComparison<T> comparison)
            {
                this.comparison = comparison;
            }

            public bool Equals(GraphEdge<T, TCost> x, GraphEdge<T, TCost> y)
            {
                return this.comparison(x.To.Value, y.To.Value);
            }

            public int GetHashCode(GraphEdge<T, TCost> obj)
            {
                return obj.To.GetHashCode();
            }
        }

        public IEqualityComparer<T> Comparer
        {
            get { return this.equalityComparer; }
        }

        public EqualityComparison<T> Comparison
        {
            get { return this.equalityComparison; }
        }

        /// <summary>
        /// Gets the number of nodes
        /// </summary>
        public int Count
        {
            get { return this.nodeSet.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}"/> is read-only.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        public GraphNode<T, TCost> this[T item]
        {
            get
            {
                GraphNode<T, TCost> node;
                if (this.TryGetNode(item, out node))
                {
                    return node;
                }

                // TODO throw better exception
                throw new Exception();
            }
        }

        /// <summary>
        /// Adds a new value to the graph.
        /// </summary>
        /// <param name="value">The value to add to the graph</param>
        public void Add(T value)
        {
            this.Add(new GraphNode<T, TCost>(value));
        }

        /// <summary>
        /// Adds a new GraphNode instance to the Graph
        /// </summary>
        /// <param name="node">The GraphNode instance to add.</param>
        public void Add(GraphNode<T, TCost> node)
        {
            Check.ArgumentNullException(node, "node");

            if (!this.Contains(node.Value))
            {
                this.nodeSet.Add(node);
            }
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        public void AddDirectedEdge(T from, T to)
        {
            this.AddDirectedEdge(from, to, default(TCost));
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        public void AddDirectedEdge(T from, T to, TCost cost)
        {
            var nodeFrom = this.GetNode(from);
            var nodeTo = this.GetNode(to);

            if (nodeTo != null && nodeFrom != null)
            {
                this.AddDirectedEdge(nodeFrom, nodeTo, cost);
            }
        }

        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        /// <param name="from">The value of one of the GraphNodetraversee es that is joined by the edge.</param>
        /// <param name="to">The value of one of the GraphNodes that is joined by the edge.</param>
        public void AddUndirectedEdge(T from, T to)
        {
            AddUndirectedEdge(from, to, default(TCost));
        }

        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="to">The value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="cost">The cost of the undirected edge.</param>
        public void AddUndirectedEdge(T from, T to, TCost cost)
        {
            var nodeFrom = this.GetNode(from);
            var nodeTo = this.GetNode(to);

            if (nodeTo != null && nodeFrom != null)
            {
                this.AddDirectedEdge(nodeFrom, nodeTo, cost);
                this.AddDirectedEdge(nodeTo, nodeFrom, cost);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection in the bag.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public virtual void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                collection.ForEach(this.Add);
            }
        }

        /// <summary>
        /// Clears out the contents of the Graph.
        /// </summary>
        public void Clear()
        {
            this.nodeSet.Clear();
        }

        /// <summary>
        /// Returns a Boolean, indicating if a particular value exists within the graph.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>True if the value exist in the graph; false otherwise.</returns>
        public bool Contains(T value)
        {
            return this.GetNode(value) != null;
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="array"/> is multidimensional.
        ///     -or-
        ///     <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        ///     -or-
        ///     The number of elements in the source <see cref="ICollection{T}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        ///     -or-
        ///     Type <paramref name="array"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Check.ArgumentNullException(array, "array");
            Check.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, CollectionResource.CopyTo_ArgumentOutOfRangeException_ArrayIndex, new { paramName = "arrayIndex" });
            Check.Requires<ArgumentException>(arrayIndex < array.Length && arrayIndex + this.Count <= array.Length, CollectionResource.CopyTo_ArgumentException_Array, new { paramName = "array" });

            if (this.Count > 0)
            {
                this.ForEach(i => array[arrayIndex++] = i);
            }
        }

        public IEnumerable<GraphNode<T, TCost>> GetAllNodes()
        {
            return this.nodeSet;
        }

        public GraphNode<T, TCost> GetNodeByValue(T value)
        {
            return this.GetNode(value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="ICollection{T}"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="ICollection{T}"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public bool Remove(T item)
        {
            // first remove the node from the nodeset
            var nodeToRemove = this.GetNode(item);
            if (nodeToRemove == null)
            {
                return false;
            }

            // otherwise, the node was found
            this.nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (var gnode in this.nodeSet)
            {
                var edge = gnode.Edges.FirstOrDefault(x => this.equalityComparison(x.To.Value, nodeToRemove.Value));
                if (edge != null)
                {
                    gnode.Edges.Remove(edge);
                }
            }

            nodeToRemove.Edges.Clear();

            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return this.GetAllNodes().Select(x => x.Value).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool TryGetNode(T item, out GraphNode<T, TCost> node)
        {
            node = this.GetNode(item);

            return node != null;
        }

        protected void AddDirectedEdge(GraphNode<T, TCost> nodeFrom, GraphNode<T, TCost> nodeTo, TCost cost)
        {
            var edge = nodeFrom.Edges.FirstOrDefault(x => this.equalityComparison(x.To.Value, nodeTo.Value));

            if (edge != null)
            {
                edge.Value = cost;
            }
            else
            {
                nodeFrom.Edges.Add(new GraphEdge<T, TCost>(nodeFrom, nodeTo, cost));
            }
        }

        protected GraphNode<T, TCost> GetNode(T value)
        {
            return this.nodeSet.FirstOrDefault(x => this.equalityComparison(x.Value, value));
        }

        /// <summary>
        /// selecting some node as the root and explores as far as possible along each branch before backtracking.
        /// </summary>
        /// <param name="item">
        /// The some node for start iteration
        /// </param>
        /// <returns>
        /// Iterates throught a collection of nodes.
        /// </returns>
        public IEnumerable<GraphNode<T, TCost>> DepthFirstTraversal(GraphNode<T, TCost> item)
        {
            if (item.marker)
            {
                this.nodeSet.ForEach(node => node.marker = false);
            }

            var stack = new Stack<GraphNode<T, TCost>>();
            stack.Push(item);
            item.marker = true;
            var unmarked = this.Count - 1;
            yield return item;
            
            do
            {
                var useEdge = item.Edges.FirstOrDefault(x => !x.To.marker);
                
                if (useEdge == null)
                {
                      stack.Pop();
                      item = stack.Peek();
                }
                else
                {
                    item = useEdge.To;
                    stack.Push(item);
                    item.marker = true;
                    unmarked--;
                    yield return item;
                }
            }
            while (unmarked > 0);
        }

        /// <summary>
        /// Selecting some node as the root and explores as far as possible along each branch before backtracking.
        /// </summary>
        /// <param name="item">
        /// The value of some root node for start iteration
        /// </param>
        /// <returns>
        /// Iterates throught a collection of nodes.
        /// </returns>
        public IEnumerable<GraphNode<T, TCost>> DepthFirstTraversal(T item)
        {
            return this.DepthFirstTraversal(this.GetNodeByValue(item));
        }

        /// <summary>
        /// Selecting some node as the root and to go through them level-by-level. 
        /// </summary>
        /// <param name="item">
        /// The some root node for start iteration
        /// </param>
        /// <returns>
        /// Iterates throught a collection of nodes.
        /// </returns>
        public IEnumerable<GraphNode<T, TCost>> BreathFirstTraversal(GraphNode<T, TCost> item)
        {
            if (item.marker)
            {
                this.nodeSet.ForEach(node => node.marker = false);
            }

            item.marker = true;
            var unmarked = this.Count - 1;
            yield return item;

            var queue = new Queue<GraphNode<T, TCost>>();

            while (unmarked > 0)
            {

                foreach (var useEdge in item.Edges)
                {
                    if (!useEdge.To.marker)
                    {
                        useEdge.To.marker = true;
                        unmarked--;
                        yield return useEdge.To;
                        queue.Enqueue(useEdge.To);
                    }
                }
                item = queue.Dequeue();

            }

        }

        /// <summary>
        /// Selecting some node as the root and to go through them level-by-level. 
        /// </summary>
        /// <param name="item">
        /// The value of some root node for start iteration
        /// </param>
        /// <returns>
        /// Iterates throught a collection of nodes.
        /// </returns>
        public IEnumerable<GraphNode<T, TCost>> BreathFirstTraversal(T item)
        {
            return this.BreathFirstTraversal(this.GetNodeByValue(item));
        }



    }
}