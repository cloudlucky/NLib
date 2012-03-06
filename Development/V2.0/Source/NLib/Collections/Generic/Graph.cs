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
    public class Graph<T, TCost> : IGraph<T, TCost>, ICloneable 
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

        /// <summary>
        /// Gets the <see cref="IEqualityComparer{T}"/> object that is used to determine equality for the values in the set.
        /// </summary>
        public IEqualityComparer<T> Comparer
        {
            get { return this.equalityComparer; }
        }

        /// <summary>
        /// Gets the <see cref="EqualityComparison{T}"/> object that is used to determine equality for the values in the set.
        /// </summary>
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
        /// Gets the nodes of the graph.
        /// </summary>
        IEnumerable<IGraphNode<T, TCost>> IGraph<T, TCost>.Nodes
        {
            get { return this.Nodes; }
        }

        /// <summary>
        /// Gets the nodes of the graph.
        /// </summary>
        public IEnumerable<GraphNode<T, TCost>> Nodes
        {
            get { return this.nodeSet; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}"/> is read-only.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the <see cref="IGraphNode{T, TCost}"/> with the specified item.
        /// </summary>
        /// <param name="item">The item to search.</param>
        /// <returns>The item if found; otherwise null.</returns>
        public IGraphNode<T, TCost> this[T item]
        {
            get { return this.GetNode(item); }
        }

        /// <summary>
        /// Gets or sets the <see cref="TCost"/> with the specified from.
        /// </summary>
        /// <param name="from">The from item.</param>
        /// <param name="to">The to item.</param>
        /// <returns>The the cost of the edge between the from and the to items.</returns>
        public TCost this[T from, T to]
        {
            get
            {
                var edge = this.GetEdge(from, to);

                if (edge == null)
                {
                    // TODO throw better exception
                    throw new Exception();
                }

                return edge.Value;
            }

            set
            {
                this.AddDirectedEdge(from, to, value);
            }
        }

        /// <summary>
        /// Adds a new value to the graph.
        /// </summary>
        /// <param name="item">The value to add to the graph</param>
        public virtual void Add(T item)
        {
            this.Add(new GraphNode<T, TCost>(item));
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        public virtual void AddDirectedEdge(T from, T to)
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
        public virtual void AddDirectedEdge(T from, T to, TCost cost)
        {
            var nodeFrom = this.GetNodeByItem(from);
            var nodeTo = this.GetNodeByItem(to);

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
        public virtual void AddUndirectedEdge(T from, T to)
        {
            this.AddUndirectedEdge(from, to, default(TCost));
        }

        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The from value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="to">The to value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="cost">The cost of the undirected edge.</param>
        public virtual void AddUndirectedEdge(T from, T to, TCost cost)
        {
            var nodeFrom = this.GetNodeByItem(from);
            var nodeTo = this.GetNodeByItem(to);

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
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                collection.ForEach(this.Add);
            }
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
        public virtual IEnumerable<IGraphNode<T, TCost>> BreathFirstTraversal(IGraphNode<T, TCost> item)
        {
            if (item.Marker)
            {
                this.nodeSet.ForEach(node => node.Marker = false);
            }

            item.Marker = true;
            var unmarked = this.Count - 1;
            yield return item;

            var queue = new Queue<IGraphNode<T, TCost>>();

            while (unmarked > 0)
            {
                foreach (var useEdge in item.Edges)
                {
                    if (!useEdge.To.Marker)
                    {
                        useEdge.To.Marker = true;
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
        public virtual IEnumerable<IGraphNode<T, TCost>> BreathFirstTraversal(T item)
        {
            return this.BreathFirstTraversal(this.GetNode(item));
        }

        /// <summary>
        /// Clears out the contents of the Graph.
        /// </summary>
        public virtual void Clear()
        {
            this.nodeSet.Clear();
        }

        /// <summary>
        /// Returns a Boolean, indicating if a particular value exists within the graph.
        /// </summary>
        /// <param name="item">The value to search for.</param>
        /// <returns>True if the value exist in the graph; false otherwise.</returns>
        public virtual bool Contains(T item)
        {
            return this.GetNodeByItem(item) != null;
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
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            Check.ArgumentNullException(array, "array");
            Check.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, CollectionResource.CopyTo_ArgumentOutOfRangeException_ArrayIndex, new { paramName = "arrayIndex" });
            Check.Requires<ArgumentException>(arrayIndex < array.Length && arrayIndex + this.Count <= array.Length, CollectionResource.CopyTo_ArgumentException_Array, new { paramName = "array" });

            if (this.Count > 0)
            {
                this.ForEach(i => array[arrayIndex++] = i);
            }
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
        public virtual IEnumerable<IGraphNode<T, TCost>> DepthFirstTraversal(IGraphNode<T, TCost> item)
        {
            if (item.Marker)
            {
                this.nodeSet.ForEach(node => node.Marker = false);
            }

            var stack = new Stack<IGraphNode<T, TCost>>();
            stack.Push(item);
            item.Marker = true;
            var unmarked = this.Count - 1;
            yield return item;

            do
            {
                var useEdge = item.Edges.FirstOrDefault(x => !x.To.Marker);

                if (useEdge == null)
                {
                    stack.Pop();
                    item = stack.Peek();
                }
                else
                {
                    item = useEdge.To;
                    stack.Push(item);
                    item.Marker = true;
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
        public virtual IEnumerable<IGraphNode<T, TCost>> DepthFirstTraversal(T item)
        {
            return this.DepthFirstTraversal(this.GetNode(item));
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.Nodes.Select(x => x.Value).GetEnumerator();
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

        /// <summary>
        /// Gets the edge between the two nodes.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        /// <returns>The edge between the two nodes; otherwise null;</returns>
        public virtual IGraphEdge<T, TCost> GetEdge(T from, T to)
        {
            return this.GetEdge(this.GetNodeByItem(from), this.GetNodeByItem(to));
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The node; otherwise null.</returns>
        public virtual IGraphNode<T, TCost> GetNode(T item)
        {
            return this.GetNodeByItem(item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="ICollection{T}"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="ICollection{T}"/>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public virtual bool Remove(T item)
        {
            var nodeToRemove = this.GetNodeByItem(item);
            if (nodeToRemove == null)
            {
                return false;
            }

            this.nodeSet.Remove(nodeToRemove);

            foreach (var gnode in this.nodeSet)
            {
                var edge = this.GetEdge(gnode, nodeToRemove);
                if (edge != null)
                {
                    gnode.Edges.Remove(edge);
                }
            }

            nodeToRemove.Edges.Clear();

            return true;
        }

        /// <summary>
        /// Adds a new GraphNode instance to the Graph
        /// </summary>
        /// <param name="item">The GraphNode instance to add.</param>
        protected virtual void Add(GraphNode<T, TCost> item)
        {
            Check.ArgumentNullException(item, "item");

            if (!this.Contains(item.Value))
            {
                this.nodeSet.Add(item);
            }
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        protected virtual void AddDirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost cost)
        {
            var edge = this.GetEdge(from, to);

            if (edge != null)
            {
                edge.Value = cost;
            }
            else
            {
                from.Edges.Add(new GraphEdge<T, TCost>(from, to, cost));
                
            }
        }

        public virtual void RemoveDirectedEdge(IGraphEdge<T, TCost> edge)
        {
                 var from = this.Nodes.FirstOrDefault(x => this.Comparison(x.Value, edge.From.Value));
                 if (from != null)
                 {
                   var edgeDeleted =   from.Edges.FirstOrDefault(x => this.Comparison(x.To.Value, edge.To.Value));
                   from.Edges.Remove(edgeDeleted);
                 }
        }

        /// <summary>
        /// Gets the edge between the two nodes.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        /// <returns>The edge between the two nodes; otherwise null;</returns>
        protected virtual GraphEdge<T, TCost> GetEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
        {
            Check.ArgumentNullException(from, "from");
            Check.ArgumentNullException(to, "to");

            return from.Edges.FirstOrDefault(x => this.Comparison(x.To.Value, to.Value));
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The node; otherwise null.</returns>
        protected virtual GraphNode<T, TCost> GetNodeByItem(T item)
        {
            return this.nodeSet.FirstOrDefault(x => this.equalityComparison(x.Value, item));
        }

        /// <summary>
        /// Graph node equality comparer
        /// </summary>
        protected class GraphNodeEqualityComparer : IEqualityComparer<GraphNode<T, TCost>>
        {
            /// <summary>
            /// The equality comparison.
            /// </summary>
            private readonly EqualityComparison<T> comparison;

            /// <summary>
            /// Initializes a new instance of the <see cref="Graph{T, TCost}.GraphNodeEqualityComparer"/> class.
            /// </summary>
            /// <param name="comparison">The comparison.</param>
            public GraphNodeEqualityComparer(EqualityComparison<T> comparison)
            {
                this.comparison = comparison;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Graph{T, TCost}.GraphNodeEqualityComparer"/> class.
            /// </summary>
            /// <param name="comparer">The comparer.</param>
            public GraphNodeEqualityComparer(IEqualityComparer<T> comparer)
            {
                this.comparison = comparer.Equals;
            }

            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <param name="x">The first object of type T to compare.</param>
            /// <param name="y">The second object of type T to compare.</param>
            /// <returns>
            /// true if the specified objects are equal; otherwise, false.
            /// </returns>
            public bool Equals(GraphNode<T, TCost> x, GraphNode<T, TCost> y)
            {
                return this.comparison(x.Value, y.Value);
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <param name="obj">The obj.</param>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            /// <exception cref="ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
            public int GetHashCode(GraphNode<T, TCost> obj)
            {
                Check.ArgumentNullException(obj, "obj");

                return obj.Value.GetHashCode();
            }
        }

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary> 
        /// <returns>
        /// 
        /// </returns>
        public object Clone()
        {
            var graphClone = new Graph<T, TCost>();
            
            foreach (var node in (HashSet<GraphNode<T, TCost>>)this.nodeSet)
            {
                var cloneNode = new GraphNode<T, TCost>(node.Value) { Marker = node.Marker };
                graphClone.Add(cloneNode);
            }
           
            foreach (var node in (HashSet<GraphNode<T, TCost>>)this.nodeSet)
            {
                foreach (var edge in node.Edges )
                {
                    graphClone.AddDirectedEdge(edge.From.Value, edge.To.Value, edge.Value);
                    graphClone.GetEdge(edge.From.Value, edge.To.Value).Marked = edge.Marked;
                }
            }

            return graphClone;
        }

        #endregion
    }
}
