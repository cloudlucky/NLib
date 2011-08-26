
namespace NLib.Collections.Generic
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Represents a graph with an arbitrary collection of GraphNode instances. 
    /// Provides methods to search and manipulate the tree.
    /// </summary>
    /// <remarks>
    /// For more information about Graph, see <![CDATA[http://msdn.microsoft.com/en-us/library/ms379574(VS.80).aspx]]>.
    /// </remarks>
    /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
    /// 
    public class Graph<T> : IGraph<T> 
    {
        /// <summary>
        /// The set of nodes in the graph
        /// </summary>
        private readonly GraphNodeList<T> nodeSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        public Graph()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Graph{T}"/> class.
        /// </summary>
        /// <param name="nodeSet">
        /// The node set.
        /// </param>
        public Graph(GraphNodeList<T> nodeSet)
        {
            this.nodeSet = nodeSet ?? new GraphNodeList<T>();
        }

        /// <summary>
        /// Adds a new GraphNode instance to the Graph
        /// </summary>
        /// <param name="node">The GraphNode instance to add.</param>
        public void AddNode(GraphNode<T> node)
        {
            // adds a node to the graph
            this.nodeSet.Add(node);
        }

        /// <summary>
        /// Adds a new value to the graph.
        /// </summary>
        /// <param name="value">The value to add to the graph</param>
        public void AddNode(T value)
        {
            this.nodeSet.Add(new GraphNode<T>(value));
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        public void AddDirectedEdge(T from, T to)
        {
            AddDirectedEdge(from, to, null);
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        public void AddDirectedEdge(T from, T to, object cost)
        {
            GraphNode<T> nodeTo = this.nodeSet.FindByValue(to);
            if (nodeTo.Value != null)
            {
                ((GraphNode<T>)this.nodeSet.FindByValue(from)).Neighbors.Add(nodeTo);
                ((GraphNode<T>)this.nodeSet.FindByValue(from)).Costs.Add(cost);
            }
        }

        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to).
        /// </summary>
        /// <param name="from">The value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="to">The value of one of the GraphNodes that is joined by the edge.</param>
        public void AddUndirectedEdge(T from, T to)
        {
            AddUndirectedEdge(from, to, null);
        }

        /// <summary>
        /// Adds an undirected edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="to">The value of one of the GraphNodes that is joined by the edge.</param>
        /// <param name="cost">The cost of the undirected edge.</param>
        public void AddUndirectedEdge(T from, T to, object cost)
        {
            GraphNode<T> nodeFrom = this.nodeSet.FindByValue(from);
            GraphNode<T> nodeTo = this.nodeSet.FindByValue(to);

            nodeTo.Costs.Add(cost);
            nodeFrom.Costs.Add(cost);
            nodeFrom.Neighbors.Add(nodeTo);
            nodeTo.Neighbors.Add(nodeFrom);
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
            return this.nodeSet.FindByValue(value) != null;
        }

        /// <summary>
        /// Attempts to remove a node from a graph.
        /// </summary>
        /// <param name="value">The value that is to be removed from the graph.</param>
        /// <returns>True if the corresponding node was found, and removed; false if the value was not
        /// present in the graph.</returns>
        /// <remarks>This method removes the GraphNode instance, and all edges leading to or from the
        /// GraphNode.</remarks>
        public bool Remove(T value)
        {
            // first remove the node from the nodeset
            var nodeToRemove = (GraphNode<T>)this.nodeSet.FindByValue(value);
            if (nodeToRemove == null)
            {
                return false;
            }

            // otherwise, the node was found
            this.nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (GraphNode<T> gnode in this.nodeSet)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                    gnode.Costs.RemoveAt(index);
                }
            }

            return true;
        }

        /// <summary>
        /// Returns an enumerator that allows for iterating through the contents of the graph.
        public IEnumerator<GraphNode<T>> GetEnumerator()
        {
            return this.nodeSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
        /// <summary>
        /// Returns the set of nodes in the graph.
        /// </summary>
        public GraphNodeList<T> Nodes
        {
            get
            {
                return this.nodeSet;
            }
        }

        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int Count
        {
            get
            {
                return this.nodeSet.Count;
            }
        }
    }

    /// <summary>
    /// Represents a node in a graph.  A graph node contains some piece of data, along with a set of
    /// neighbors.  There can be an optional cost between a graph node and each of its neighbors.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph node.</typeparam>
    public class GraphNode<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode{T}"/> class.
        /// </summary>
        public GraphNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode{T}"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public GraphNode(T value)
            : this(value, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphNode{T}"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="neighbors">
        /// The neighbors.
        /// </param>
        public GraphNode(T value, GraphNodeList<T> neighbors)
        {
            this.Value = value;
            this.Neighbors = neighbors ?? new GraphNodeList<T>();
            this.Costs = new List<object>();
        }

        /// <summary>
        /// Gets or sets Value.
        /// </summary>
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
        public List<object> Costs { get; set; }

    }

    /// <summary>
    /// Adjacency-list representation
    /// </summary>
    /// <typeparam name="T">
    /// type of nodes
    /// </typeparam>
    public class GraphNodeList<T> : Collection<GraphNode<T>>
    {
        /// <summary>
        /// Searches the NodeList for a Node containing a particular value.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The Node in the NodeList, if it exists; null otherwise.</returns>
        public GraphNode<T> FindByValue(T value)
        {
            return Items.FirstOrDefault(node => node.Value.Equals(value));
        }
    }
}
