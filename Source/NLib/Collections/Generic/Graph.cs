
namespace NLib.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    /// <summary>
    /// Represents a graph.  A graph is an arbitrary collection of GraphNode instances.
    /// </summary>
    /// <typeparam name="T">The type of data stored in the graph's nodes.</typeparam>
    /// http://msdn.microsoft.com/en-us/library/ms379574(VS.80).aspx
    /// 
    public class Graph<T> : IEnumerable<GraphNode<T>>
    {
        private GraphNodeList<T> nodeSet;        // the set of nodes in the graph

        public Graph() : this(null) { }
        public Graph(GraphNodeList<T> nodeSet)
        {
            if (nodeSet == null)
                this.nodeSet = new GraphNodeList<T>();
            else
                this.nodeSet = nodeSet;
        }

        /// <summary>
        /// Adds a new GraphNode instance to the Graph
        /// </summary>
        /// <param name="node">The GraphNode instance to add.</param>
        public void AddNode(GraphNode<T> node)
        {
            // adds a node to the graph
            nodeSet.Add(node);
        }

        /// <summary>
        /// Adds a new value to the graph.
        /// </summary>
        /// <param name="value">The value to add to the graph</param>
        public void AddNode(T value)
        {
            nodeSet.Add(new GraphNode<T>(value));
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
        /// Adds a directed edge from one GraphNode (from) to another (to).
        /// </summary>
        /// <param name="from">The GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The GraphNode to which the edge leads.</param>
        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to)
        {
            AddDirectedEdge(from, to, null);
        }
          
        /// <summary>
        /// Adds a directed edge from one GraphNode (from) to another (to) with an associated cost.
        /// </summary>
        /// <param name="from">The GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int? cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        /// <summary>
        /// Adds a directed edge from a GraphNode with one value (from) to a GraphNode with another value (to)
        /// with an associated cost.
        /// </summary>
        /// <param name="from">The value of the GraphNode from which the directed edge eminates.</param>
        /// <param name="to">The value of the GraphNode to which the edge leads.</param>
        /// <param name="cost">The cost of the edge from "from" to "to".</param>
        public void AddDirectedEdge(T from, T to, int? cost)
        {
            ((GraphNode<T>)nodeSet.FindByValue(from)).Neighbors.Add(nodeSet.FindByValue(to));
            ((GraphNode<T>)nodeSet.FindByValue(from)).Costs.Add(cost);
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
        public void AddUndirectedEdge(T from, T to, int? cost)
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
            nodeSet.Clear();
        }

        /// <summary>
        /// Returns a Boolean, indicating if a particular value exists within the graph.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>True if the value exist in the graph; false otherwise.</returns>
        public bool Contains(T value)
        {
            return nodeSet.FindByValue(value) != null;
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
            GraphNode<T> nodeToRemove = (GraphNode<T>)nodeSet.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (GraphNode<T> gnode in nodeSet)
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
            foreach (GraphNode<T> node in nodeSet)
                yield return node;
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
                return nodeSet;
            }
        }

        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int Count
        {
            get { return nodeSet.Count; }
        }

    }
}
