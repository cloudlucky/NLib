namespace NLib.Collections.Generic.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// It's a set of algorithms for resolve the circulations problems for graphs.
    /// </summary>
    public static class GraphExtensions
    {
        /// <summary>
        /// Find a path
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="start">start value node.</param>
        /// <param name="terminated">end value node.</param>
        /// <returns>path or null.</returns>
        [CLSCompliant(false)]
        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, T start, T terminated)
        {
            return FindPath(graph, graph[start], graph[terminated], Comparer<T>.Default);
        }

        /// <summary>
        /// Find a path
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="start">start value node.</param>
        /// <param name="terminated">end value node.</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <returns>path or null.</returns>
        [CLSCompliant(false)]
        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, T start, T terminated, IComparer<T> comparerValue)
        {
            return FindPath(graph, graph[start], graph[terminated], comparerValue);
        }

        /// <summary>
        /// Find a path
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="start">some root node.</param>
        /// <param name="terminated">some end node.</param>
        /// <returns>path or null.</returns>
        [CLSCompliant(false)]
        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated)
        {
            return FindPath(graph, start, terminated, Comparer<T>.Default);
        }

        /// <summary>
        /// Find a path
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The graph.</param>
        /// <param name="start">some root node.</param>
        /// <param name="terminated">some end node.</param>
        /// <param name="comparerValue">comparer Value..</param>
        /// <returns>path or null.</returns>
        [CLSCompliant(false)]
        public static Stack<IGraphEdge<T, Number>> FindPath<T>(IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated, IComparer<T> comparerValue)
        {
            Check.Current.ArgumentNullException(graph, "graph")
                         .ArgumentNullException(start, "start")
                         .ArgumentNullException(terminated, "terminated")
                         .ArgumentNullException(comparerValue, "comparerValue");

            var markedEdge = new Stack<IGraphEdge<T, Number>>();
            var path = new Stack<IGraphEdge<T, Number>>();
            var currentNode = start;

            while (currentNode != null && comparerValue.Compare(currentNode.Value, terminated.Value) != 0)
            {
                var currentEdge = currentNode.Edges.FirstOrDefault(e => e.Marked != true && !(path.Contains(e) || markedEdge.Contains(e)));
                if (currentEdge != null)
                {
                    currentEdge.Marked = true;
                    path.Push(currentEdge);
                    currentNode = currentEdge.To;
                }
                else if (path.Count > 0)
                {
                    currentNode = path.Peek().From;
                    markedEdge.Push(path.Pop());
                }
                else
                {
                    currentNode = null;
                }
            }

            if (currentNode == null || comparerValue.Compare(currentNode.Value, terminated.Value) != 0)
            {
                path.Clear();
            }

            path.ForEach(edge => edge.Marked = false);
            markedEdge.ForEach(edge => edge.Marked = false);

            return path;
        }

        /// <summary>
        /// As long as there an open path through the residual graph, 
        /// send the minimum of the residual capacities on the path.
        /// The algorithm works only if all weights are integers.
        /// Warning the graph is used as a residual graph. 
        /// You can make a copy with clone method.
        /// The marked value is used for avoid a recursive path.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The residual graph.</param>
        /// <param name="start">some root node value.</param>
        /// <param name="terminated">some end node value.</param>
        /// <returns>The maximum flow.</returns>
        /// <exception cref = "ArgumentNullException">If graph , start or terminated is null.</exception>
        [CLSCompliant(false)]
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, T start, T terminated)
        {
            return FordFulkersonAlgorithm(graph, graph[start], graph[terminated]);
        }

        /// <summary>
        /// As long as there an open path through the residual graph, 
        /// send the minimum of the residual capacities on the path.
        /// The algorithm works only if all weights are integers.
        /// Warning the graph is used as a residual graph. 
        /// You can make a copy with clone method.
        /// The marked value is used for avoid a recursive path.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The residual graph.</param>
        /// <param name="start">some root node value.</param>
        /// <param name="terminated">some end node value.</param>
        /// <returns>The maximum flow.</returns>
        /// <exception cref = "ArgumentNullException">If graph , start or terminated is null.</exception>
        [CLSCompliant(false)]
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated)
        {
            return FordFulkersonAlgorithm(graph, start, terminated, Comparer<T>.Default);
        }

        /// <summary>
        /// As long as there an open path through the residual graph, 
        /// send the minimum of the residual capacities on the path.
        /// The algorithm works only if all weights are integers.
        /// Warning the graph is used as a residual graph. 
        /// You can make a copy with clone method.
        /// The marked value is used for avoid a recursive path.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The residual graph.</param>
        /// <param name="start">start node value.</param>
        /// <param name="terminated">end node value.</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <returns>The maximum flow.</returns>
        /// <exception cref = "ArgumentNullException">If graph , start or terminated is null.</exception>
        [CLSCompliant(false)]
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, T start, T terminated, IComparer<T> comparerValue)
        {
            return FordFulkersonAlgorithm(graph, graph[start], graph[terminated], comparerValue);
        }

        /// <summary>
        /// As long as there an open path through the residual graph, 
        /// send the minimum of the residual capacities on the path.
        /// The algorithm works only if all weights are integers.
        /// Warning the graph is used as a residual graph. 
        /// You can make a copy with clone method.
        /// The marked value is used for avoid a recursive path.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">The residual graph.</param>
        /// <param name="start">some root node.</param>
        /// <param name="terminated">some end node.</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <returns>The maximum flow.</returns>
        /// <exception cref = "ArgumentNullException">If graph , start or terminated is null.</exception>
        [CLSCompliant(false)]
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated, IComparer<T> comparerValue)
        {
            Check.Current.ArgumentNullException(graph, "graph")
                         .ArgumentNullException(start, "start")
                         .ArgumentNullException(terminated, "terminated")
                         .ArgumentNullException(comparerValue, "comparerValue");

            var path = FindPath(graph, start, terminated, comparerValue);
            Number flowMax = 0;

            while (path.Count > 0)
            {
                Number bottleneck = path.Min(edge => edge.Value);

                flowMax += bottleneck;

                foreach (var edge in path)
                {
                    edge.Value -= bottleneck;
                    var edgeReversed = graph.GetEdge(edge.To.Value, edge.From.Value);

                    if (edgeReversed == null)
                    {
                        graph.AddDirectedEdge(edge.To.Value, edge.From.Value, bottleneck);
                    }
                    else
                    {
                        edgeReversed.Value += bottleneck;
                    }

                    if (edge.Value == 0)
                    {
                        graph.RemoveEdge(edge);
                    }
                }

                path = FindPath(graph, start, terminated, comparerValue);
            }

            return flowMax;
        }

        /// <summary>
        /// Given a digraph with nonnegative weights on its edges and vertices Start, 
        /// find a shortest path from Start.
        /// Warning the graph must be a digraph with no negative weight.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">A graph with no negative weights.</param>
        /// <param name="start">Start vertices for to find a shortest path.</param>
        /// <param name="distance">Shortest distance of start from a node.</param>
        /// <param name="previous">Previous visited node.</param>
        /// <exception cref = "ArgumentNullException">If graph , start, distance, previous or comparerValue is null.</exception>
        [CLSCompliant(false)]
        public static void Djkstra<T>(this IGraph<T, Number> graph, T start, IDictionary<T, Number> distance, IDictionary<T, T> previous)
        {
            Djkstra(graph, graph[start], distance, previous, Comparer<T>.Default);
        }

        /// <summary>
        /// Given a digraph with nonnegative weights on its edges and vertices Start, 
        /// find a shortest path from Start.
        /// Warning the graph must be a digraph with no negative weight.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">A graph with no negative weights.</param>
        /// <param name="start">Start vertices for to find a shortest path.</param>
        /// <param name="distance">Shortest distance of start from a node.</param>
        /// <param name="previous">Previous visited node.</param>
        /// <exception cref = "ArgumentNullException">If graph , start, distance, previous or comparerValue is null.</exception>
        [CLSCompliant(false)]
        public static void Djkstra<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IDictionary<T, Number> distance, IDictionary<T, T> previous)
        {
            Djkstra(graph, start, distance, previous, Comparer<T>.Default);
        }

        /// <summary>
        /// Given a digraph with nonnegative weights on its edges and vertices Start, 
        /// find a shortest path from Start.
        /// Warning the graph must be a digraph with no negative weight.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">A graph with no negative weights.</param>
        /// <param name="start">The value of Start vertices for to find a shortest path.</param>
        /// <param name="distance">Shortest distance of start from a node.</param>
        /// <param name="previous">Previous visited node.</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <exception cref = "ArgumentNullException">If graph , start, distance, previous or comparerValue is null.</exception>
        [CLSCompliant(false)]
        public static void Djkstra<T>(this IGraph<T, Number> graph, T start, IDictionary<T, Number> distance, IDictionary<T, T> previous, IComparer<T> comparerValue)
        {
            Djkstra(graph, graph[start], distance, previous, comparerValue);
        }

        /// <summary>
        /// Given a digraph with nonnegative weights on its edges and vertices Start, 
        /// find a shortest path from Start.
        /// Warning the graph must be a digraph with no negative weight.
        /// </summary>
        /// <typeparam name="T">Type for the name of node.</typeparam>
        /// <param name="graph">A graph with no negative weights.</param>
        /// <param name="start">Start vertices for to find a shortest path.</param>
        /// <param name="distance">Shortest distance of start from a node.</param>
        /// <param name="previous">Previous visited node.</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <exception cref = "ArgumentNullException">If graph , start, distance, previous or comparerValue is null.</exception>
        private static void Djkstra<T>(IGraph<T, Number> graph, IGraphNode<T, Number> start, IDictionary<T, Number> distance, IDictionary<T, T> previous, IComparer<T> comparerValue)
        {
            Check.Current.ArgumentNullException(graph, "graph")
                         .ArgumentNullException(start, "start")
                         .ArgumentNullException(distance, "distance")
                         .ArgumentNullException(previous, "previous")
                         .ArgumentNullException(comparerValue, "comparerValue");

            foreach (var node in graph.Nodes)
            {
                distance.Add(node.Value, comparerValue.Compare(start.Value, node.Value) == 0 ? 0 : Number.MaxValue);
                previous.Add(node.Value, node.Value);
                node.Marked = false;
            }

            var currentNode = start;
            for (var i = 0; i < graph.Nodes.Count(); i++)
            {
                currentNode.Marked = true;
                foreach (var edge in currentNode.Edges)
                {
                    if (distance[edge.To.Value] > distance[edge.From.Value] + edge.Value)
                    {
                        distance[edge.To.Value] = distance[edge.From.Value] + edge.Value;
                        previous[edge.To.Value] = currentNode.Value;
                    }
                }

                var minValue = Number.MaxValue;
                foreach (var node in graph.Nodes)
                {
                    if (distance[node.Value] < minValue && !node.Marked)
                    {
                        currentNode = node;
                        minValue = distance[node.Value];
                    }
                }
            }
        }
    }
}