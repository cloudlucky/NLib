namespace NLib.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using NLib.Collections.Generic.Extensions;
    using NLib.Collections.Generic.Resources;

    /// <summary>
    /// Provides the base interface for the abstraction of a graph edge.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public interface IGraphEdge<T, TCost>
    {
        /// <summary>
        /// Gets the from node.
        /// </summary>
        IGraphNode<T, TCost> From { get; }

        /// <summary>
        /// Gets the to node.
        /// </summary>
        IGraphNode<T, TCost> To { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        TCost Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IGraphEdge{T, TCost}" /> is marked.
        /// </summary>
        bool Marked { get; set; }
    }

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
        /// <c>true</c> if marker; otherwise, <c>false</c>.
        /// </value>
        bool Marked { get; set; }
    }

    /// <summary>
    /// Represents a graph with an arbitrary collection of GraphNode instances. 
    /// Provides methods to search and manipulate the tree.
    /// </summary>
    /// <remarks>
    /// For more information about Graph, see <![CDATA[http://msdn.microsoft.com/en-us/library/ms379574(VS.80).aspx]]>.
    /// </remarks>
    /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
    /// <typeparam name="TCost">The type of cost.</typeparam>
    [Serializable]
    public class Graph<T, TCost> : IGraph<T, TCost>, ICloneable, IEnumerable
    {
        /// <summary>
        /// The set of nodes in the graph
        /// </summary>
        private readonly HashSet<GraphNode<T, TCost>> nodeSet;

        /// <summary>
        /// The equality comparer.
        /// </summary>
        private readonly IEqualityComparer<T> equalityComparer;

        /// <summary>
        /// The equality comparer.
        /// </summary>
        private readonly EqualityComparison<T> equalityComparison;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}" /> class.
        /// </summary>
        public Graph()
            : this(EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEqualityComparer<T> comparer)
            : this(null, comparer)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T, TCost}" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public Graph(IEnumerable<T> collection, IEqualityComparer<T> comparer)
        {
            this.equalityComparison = comparer.Equals;
            this.equalityComparer = comparer;

            this.nodeSet = new HashSet<GraphNode<T, TCost>>(new GraphNodeEqualityComparer(this.equalityComparison));
            this.AddRange(collection);
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
        public IEnumerable<GraphNode<T, TCost>> Nodes
        {
            get { return this.nodeSet; }
        }

        /// <summary>
        /// Gets the <see cref="IGraphNode{T, TCost}" /> with the specified item.
        /// </summary>
        /// <param name="item">The item to search.</param>
        /// <returns>The item if found; otherwise null.</returns>
        public virtual IGraphNode<T, TCost> this[T item]
        {
            get
            {
                return this.GetNode(item);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="TCost" /> with the specified from.
        /// </summary>
        /// <param name="from">The from item.</param>
        /// <param name="to">The to item.</param>
        /// <returns>The the cost of the edge between the from and the to items.</returns>
        public TCost this[T from, T to]
        {
            get
            {
                return this.GetEdge(from, to).Value;
            }

            set
            {
                var edge = this.GetEdge(from, to);
                edge.Value = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="EqualityComparison{T}" /> object that is used to determine equality for the values in the set.
        /// </summary>
        protected EqualityComparison<T> Comparison
        {
            get { return this.equalityComparison; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}" /> is read-only.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the nodes of the graph.
        /// </summary>
        IEnumerable<IGraphNode<T, TCost>> IGraph<T, TCost>.Nodes
        {
            get { return this.Nodes; }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }




        /// <summary>
        /// Adds a new value to the graph.
        /// </summary>
        /// <param name="item">The value to add to the graph</param>
        public virtual void Add(T item)
        {
            this.Add(GraphNodeFactory.GetFactory("GraphNodeDefaultFactory").Create<T, TCost>(item));
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge emanates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        public virtual void AddDirectedEdge(T from, T to, TCost cost = default(TCost))
        {
            this.AddDirectedEdge(this.GetNodeByItem(from), this.GetNodeByItem(to), cost);
        }

        /// <summary>
        /// Add the elements of the specified collection in the graph.
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
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The from value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="to">The to value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="cost">The cost of the undirected edge.</param>
        public virtual void AddUndirectedEdge(T from, T to, TCost cost = default(TCost))
        {
            this.AddUndirectedEdge(this.GetNodeByItem(from), this.GetNodeByItem(to), cost);
        }

        /// <summary>
        /// Selecting some node as the root and to go through them level-by-level.
        /// </summary>
        /// <param name="item">The value of some root node for start iteration.</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        public virtual IEnumerable<IGraphNode<T, TCost>> BreathFirstTraversal(T item)
        {
            return this.BreathFirstTraversal(this.GetNode(item));
        }

        /// <summary>
        /// Selecting some node as the root and to go through them level-by-level.
        /// </summary>
        /// <param name="item">The some root node for start iteration.</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        public virtual IEnumerable<IGraphNode<T, TCost>> BreathFirstTraversal(IGraphNode<T, TCost> item)
        {
            if (item.Marked)
            {
                this.nodeSet.ForEach(node => node.Marked = false);
            }

            item.Marked = true;
            var unmarked = this.Count - 1;
            yield return item;

            var queue = new Queue<IGraphNode<T, TCost>>();

            while (unmarked > 0)
            {
                foreach (var useEdge in item.Edges.Where(useEdge => !useEdge.To.Marked))
                {
                    useEdge.To.Marked = true;
                    unmarked--;
                    yield return useEdge.To;
                    queue.Enqueue(useEdge.To);
                }

                item = queue.Dequeue();
            }
        }

        /// <summary>
        /// Clears out the contents of the Graph.
        /// </summary>
        public virtual void Clear()
        {
            this.nodeSet.Clear();
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A copy of your graph.
        /// </returns>
        public virtual object Clone()
        {
            var clone = new Graph<T, TCost>();

            foreach (var nodeClone in this.nodeSet.Select(node => new GraphNode<T, TCost>(node.Value) { Marked = node.Marked }))
            {
                clone.Add(nodeClone);
            }

            foreach (var edge in this.nodeSet.SelectMany(node => node.Edges))
            {
                clone.GetType().InvokeMember("Add" + edge.GetType().Name.Trim('`', '2'), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, clone, new object[] { edge.From.Value, edge.To.Value, edge.Value });
                clone.GetEdge(edge.From, edge.To).Marked = edge.Marked;
            }

            return clone;
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
        /// Copies the elements of the <see cref="ICollection{T}" /> to an <see cref="Array" />, starting at a particular <see cref="Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array" /> that is the destination of the elements copied from <see cref="ICollection{T}" />. The <see cref="Array" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="array" /> is multidimensional.
        /// -or-
        /// <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />.
        /// -or-
        /// The number of elements in the source <see cref="ICollection{T}" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
        /// -or-
        /// Type <paramref name="array" /> cannot be cast automatically to the type of the destination <paramref name="array" />.
        /// </exception>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {  
            if (this.Count > 0)
            {
                this.ForEach(i => array[arrayIndex++] = i);
            }
        }

        /// <summary>
        /// Selecting some node as the root and explores as far as possible along each branch before backtracking.
        /// </summary>
        /// <param name="item">The value of some root node for start iteration.</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        public virtual IEnumerable<IGraphNode<T, TCost>> DepthFirstTraversal(T item)
        {
            return this.DepthFirstTraversal(this.GetNode(item));
        }

        /// <summary>
        /// selecting some node as the root and explores as far as possible along each branch before backtracking.
        /// </summary>
        /// <param name="item">The some node for start iteration.</param>
        /// <returns>Iterates through a collection of nodes.</returns>
        public virtual IEnumerable<IGraphNode<T, TCost>> DepthFirstTraversal(IGraphNode<T, TCost> item)
        {
            if (item.Marked)
            {
                this.nodeSet.ForEach(node => node.Marked = false);
            }

            var stack = new Stack<IGraphNode<T, TCost>>();
            stack.Push(item);
            item.Marked = true;
            var unmarked = this.Count - 1;
            yield return item;

            do
            {
                var useEdge = item.Edges.FirstOrDefault(x => !x.To.Marked);

                if (useEdge == null)
                {
                    stack.Pop();
                    item = stack.Peek();
                }
                else
                {
                    item = useEdge.To;
                    stack.Push(item);
                    item.Marked = true;
                    unmarked--;
                    yield return item;
                }
            }
            while (unmarked > 0);
        }

        /// <summary>
        /// Gets the edge between the two nodes.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        /// <returns>The edge between the two nodes; otherwise null;</returns>
        public virtual IGraphEdge<T, TCost> GetEdge(IGraphNode<T, TCost> from, IGraphNode<T, TCost> to)
        {
            return from.Edges.FirstOrDefault(x => this.Comparison(x.To.Value, to.Value));
        }

        /// <summary>
        /// Gets the edge between the two nodes.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        /// <returns>The edge between the two nodes; otherwise null;</returns>
        public IGraphEdge<T, TCost> GetEdge(T from, T to)
        {
            return this.GetEdge(this.GetNodeByItem(from), this.GetNodeByItem(to));
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return this.Nodes.Select(x => x.Value).GetEnumerator();
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
        /// Remove node from GraphNode 
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns>If a node is removed then return true.</returns>
        public virtual bool Remove(IGraphNode<T, TCost> node)
        {
            return this.Remove(node.Value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}" />.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}" />.</param>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="ICollection{T}" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="ICollection{T}" />.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}" /> is read-only.</exception>
        public virtual bool Remove(T item)
        {
            Check.Current.ArgumentNullException(item, "Require argument 'item'");
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
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}" />.
        /// </summary>
        /// <param name="edge">The object to remove from the <see cref="ICollection{T}" />.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}" /> is read-only.</exception>
        public virtual void RemoveDirectedEdge(IGraphEdge<T, TCost> edge)
        {
            var from = this.Nodes.FirstOrDefault(x => this.Comparison(x.Value, edge.From.Value));
            if (from != null)
            {
                from.Edges.Remove(@from.Edges.FirstOrDefault(x => this.Comparison(x.To.Value, edge.To.Value)));
            }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}" />.
        /// </summary>
        /// <param name="edge">The object to remove from the <see cref="ICollection{T}" />.</param>
        /// <param name="graph">The graph.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}" /> is read-only.</exception>
        public virtual void RemoveEdge(IGraphEdge<T, TCost> edge, IGraph<T, TCost> graph = null)
        {
            var obj = graph ?? this;
            obj.GetType().InvokeMember("Remove" + edge.GetType().Name.Trim('`', '2'), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, obj, new object[] { edge });
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}" />.
        /// </summary>
        /// <param name="edge">The object to remove from the <see cref="ICollection{T}" />.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}" /> is read-only.</exception>
        public virtual void RemoveUndirectedEdge(IGraphEdge<T, TCost> edge)
        {
            var to = this.Nodes.FirstOrDefault(x => this.Comparison(x.Value, edge.To.Value));
            to.Edges.Remove(to.Edges.FirstOrDefault(x => this.Comparison(x.To.Value, edge.From.Value)));
            var from = this.Nodes.FirstOrDefault(x => this.Comparison(x.Value, edge.From.Value));
            from.Edges.Remove(from.Edges.FirstOrDefault(x => this.Comparison(x.To.Value, edge.To.Value)));
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The node; otherwise null.</returns>
        protected internal GraphNode<T, TCost> GetNodeByItem(T item)
        {
            Check.Current.ArgumentNullException(item, "Require argument 'item'");
            return this.nodeSet.FirstOrDefault(x => this.equalityComparison(x.Value, item));
        }

        /// <summary>
        /// Adds a new GraphNode instance to the Graph
        /// </summary>
        /// <param name="item">The GraphNode instance to add.</param>
        protected void Add(GraphNode<T, TCost> item)
        {
            Check.Current.ArgumentNullException(item, "Require argument 'item'");
            if (!this.Contains(item.Value))
            {
                this.nodeSet.Add(item);
            }
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge emanates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        /// <param name="marked">The value is true for a marked edge.</param>
        private void AddDirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost cost, bool marked = false)
        {
            Check.Current.ArgumentNullException(from, "Require argument 'from' ")
                         .ArgumentNullException(to, "Require argument 'to' ");

            var edge = this.GetEdge(from, to);

            if (edge != null)
            {
                Check.Current.ArgumentException(edge.GetType().Name.Contains("DirectedEdge"), "edge", "It is not an undirected edge.");
                edge.Value = cost;
                edge.Marked = marked;
            }
            else
            {
                edge = GraphEdgeFactory.GetFactory("DirectedEdgeFactory").Create(from, to, cost);
                edge.Marked = marked;
                from.Edges.Add(edge);
            }
        }

        /// <summary>
        /// Add a undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the undirected edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        /// <param name="marked">The value is true for a marked edge</param>
        private void AddUndirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost cost, bool marked = false)
        {
            Check.Current.ArgumentNullException(from, "Require argument 'from' ")
                         .ArgumentNullException(to, "Require argument 'to' ");

            var edge = this.GetEdge(from, to) ?? this.GetEdge(to, from);

            if (edge != null)
            {
                Check.Current.ArgumentException(edge.GetType().Name.Contains("UndirectedEdge"), "edge", "It is not an undirected edge.");
                edge.Value = cost;
                edge.Marked = marked;
            }
            else
            {
                var factory = GraphEdgeFactory.GetFactory("UndirectedEdgeFactory");
                edge = factory.Create<T, TCost>(to, from, cost);
                edge.Marked = marked;
                to.Edges.Add(edge);

                edge = factory.Create<T, TCost>(from, to, cost);
                edge.Marked = marked;
                from.Edges.Add(edge);
            }
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
            /// Initializes a new instance of the <see cref="Graph{T, TCost}.GraphNodeEqualityComparer" /> class.
            /// </summary>
            /// <param name="comparison">The comparison.</param>
            public GraphNodeEqualityComparer(EqualityComparison<T> comparison)
            {
                Check.Current.ArgumentNullException(comparison, "Require argument 'compararison'");
                this.comparison = comparison;
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
                Check.Current.ArgumentNullException(x, "Require argument 'x' ")
                             .ArgumentNullException(y, "Require argument 'y'");
                return this.comparison(x.Value, y.Value);
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <param name="obj">The graph node.</param>
            /// <returns>
            /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            /// </returns>
            /// <exception cref="ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
            public int GetHashCode(GraphNode<T, TCost> obj)
            {
                Check.Current.ArgumentNullException(obj, "Require argument 'obj'");
                return obj.Value.GetHashCode();
            }
        }
    }

    /// <summary>
    /// Represents a graph with an arbitrary collection of GraphNode instances. 
    /// Provides methods to search and manipulate the tree.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
    [CLSCompliant(false)]
    public class Graph<T> : Graph<T, Number>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}" /> class.
        /// </summary>
        public Graph()
        {
        }
    }

    /// <summary>
    /// Represents a node in a graph.  A graph node contains some piece of data, along with a set of
    /// neighbors. There can be an optional cost between a graph node and each of its neighbors.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph node.</typeparam>
    /// ///
    /// <typeparam name="TCost">The type of cost.</typeparam>
    public class GraphNode<T, TCost> : IGraphNode<T, TCost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode{T, TCost}" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public GraphNode(T value)
        {
            Check.Current.ArgumentNullException(value, "Require argument 'value'");
            this.Value = value;
            this.Edges = new List<IGraphEdge<T, TCost>>();
            this.Marked = false;
        }

        /// <summary>
        /// Gets or sets the edges.
        /// </summary>
        public ICollection<IGraphEdge<T, TCost>> Edges { get; protected set; }

        /// <summary>
        /// Gets the edges.
        /// </summary>
        IEnumerable<IGraphEdge<T, TCost>> IGraphNode<T, TCost>.Edges
        {
            get
            {
                return this.Edges;
            }
        }

        /// <summary>
        /// Gets the neighbors.
        /// </summary>
        IEnumerable<INode<T>> INode<T>.Neighbors
        {
            get
            {
                return this.Edges.Select(x => x.To);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GraphNode{T, TCost}" /> is marker.
        /// </summary>
        /// <value>
        /// <c>true</c> if marker; otherwise, <c>false</c>.
        /// </value>
        public bool Marked { get; set; }

    }

    /// <summary>
    /// Represents an edge between two nodes.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public class GraphEdge<T, TCost> : IGraphEdge<T, TCost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge{T, TCost}" /> class.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        public GraphEdge(IGraphNode<T, TCost> from, IGraphNode<T, TCost> to)
            : this(from, to, default(TCost))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge{T, TCost}" /> class.
        /// </summary>
        /// <param name="from">The from node.</param>
        /// <param name="to">The to node.</param>
        /// <param name="value">The value of the value.</param>
        public GraphEdge(IGraphNode<T, TCost> from, IGraphNode<T, TCost> to, TCost value)
        {
            Check.Current.ArgumentNullException(from, "Require argument 'from'")
                         .ArgumentNullException(to, "Require argument 'to'")
                         .ArgumentNullException(value, "Require argument 'value'");

            this.From = from;
            this.To = to;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphEdge{T, TCost}" /> class.
        /// </summary>
        protected GraphEdge()
        {
        }

        /// <summary>
        /// Gets or sets the from node.
        /// </summary>
        public IGraphNode<T, TCost> From { get; protected set; }

        /// <summary>
        /// Gets the from node.
        /// </summary>
        IGraphNode<T, TCost> IGraphEdge<T, TCost>.From
        {
            get
            {
                return this.From;
            }
        }

        /// <summary>
        /// Gets or sets the to node.
        /// </summary>
        public IGraphNode<T, TCost> To { get; protected set; }

        /// <summary>
        /// Gets the to node.
        /// </summary>
        IGraphNode<T, TCost> IGraphEdge<T, TCost>.To
        {
            get
            {
                return this.To;
            }
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

    /// <summary>
    /// A factory for the edges
    /// </summary>
    public abstract class GraphEdgeFactory
    {
        public static GraphEdgeFactory GetFactory(string typeName)
        {
            Check.Current.ArgumentNullException(typeName, "Require argument 'typeName'");
            Type type = Type.GetType("NLib.Collections.Generic." + typeName);
            return (GraphEdgeFactory)type.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
        }

        public abstract GraphEdge<T, TCost> Create<T, TCost>();

        public abstract GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to);

        public abstract GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value);
    }

    /// <summary>
    /// The factory for undirected edges
    /// </summary>
    internal sealed class UndirectedEdgeFactory : GraphEdgeFactory
    {
        public override GraphEdge<T, TCost> Create<T, TCost>()
        {
            return (new UndirectedEdge<T, TCost>());
        }

        public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
        {
            return (new UndirectedEdge<T, TCost>(from, to));
        }

        public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
        {
            return (new UndirectedEdge<T, TCost>(from, to, value));
        }

    }

    /// <summary>
    /// The factory for directed edges
    /// </summary>
    internal sealed class DirectedEdgeFactory : GraphEdgeFactory
    {
        public override GraphEdge<T, TCost> Create<T, TCost>()
        {
            return (new DirectedEdge<T, TCost>());
        }

        public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
        {
            return (new DirectedEdge<T, TCost>(from, to));
        }

        public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
        {
            return (new DirectedEdge<T, TCost>(from, to, value));
        }
    }

    /// <summary>
    /// Represents an undirected edge
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public sealed class UndirectedEdge<T, TCost> : GraphEdge<T, TCost>
    {
        public UndirectedEdge()
        {
        }

        public UndirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
            : base(from, to)
        {
        }

        public UndirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
            : base(from, to, value)
        {
        }

        private TCost undirectedEdgeValue;

        public override TCost Value
        {
            get
            {
                return this.undirectedEdgeValue;
            }

            set
            {
                this.undirectedEdgeValue = value;

                var reverse = this.To.Edges.FirstOrDefault(e => e.To == this.From);
                if (reverse != null && Comparer<TCost>.Default.Compare(reverse.Value, value) != 0)
                {
                    reverse.Value = value;
                }
            }
        }
    }

    /// <summary>
    /// Represents a directed edge
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public sealed class DirectedEdge<T, TCost> : GraphEdge<T, TCost>
    {
        public DirectedEdge()
        {
        }

        public DirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
            : base(from, to)
        {
             
        }

        public DirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
            : base(from, to, value)
        {
        }
    }

    /// <summary>
    /// A factory for the nodes
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public abstract class GraphNodeFactory
    {
        public static GraphNodeFactory GetFactory(string typeName)
        {
            Check.Current.ArgumentNullException(typeName, "Require argument 'typeName'");
            Type type = Type.GetType("NLib.Collections.Generic." + typeName);
            return (GraphNodeFactory)type.InvokeMember(null, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
        }

        public abstract GraphNode<T, TCost> Create<T, TCost>(T value);
    }

    /// <summary>
    /// The factory for a graph node
    /// </summary>
    internal sealed class GraphNodeDefaultFactory : GraphNodeFactory
    {
        public override GraphNode<T, TCost> Create<T, TCost>(T value)
        {
            return (new GraphNodeDefault<T, TCost>(value));
        }
    }

    /// <summary>
    /// Represents a graph node
    /// </summary>
    internal sealed class GraphNodeDefault<T, TCost> : GraphNode<T, TCost>
    {
        public GraphNodeDefault(T value)
            : base(value)
        {
            this.Marked = false;
        }
    }



}