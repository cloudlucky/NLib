// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphFlot.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace NLib.Collections.Generic.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///   It's a set of algorithms for resolve the circulations problems for graphs.
    /// </summary>
    public static class GraphFlot
    {

        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, T start, T terminated)
        {
            var startNode = graph[start];
            var terminatedNode = graph[terminated];

            return FindPath(graph, startNode, terminatedNode, Comparer<T>.Default);
        }

        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated)
        {
            return FindPath(graph, start, terminated, Comparer<T>.Default);
        }

        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, T start, T terminated, IComparer<T> comparerValue)
        {
            var startNode = graph[start];
            var terminatedNode = graph[terminated];

            return FindPath(graph, startNode, terminatedNode, comparerValue);
        }

        /// <summary>
        ///   Find a path
        /// </summary>
        /// <typeparam name = "T">Type for the name of node</typeparam>
        /// <param name = "graph">The graph</param>
        /// <param name = "start">some root node</param>
        /// <param name = "terminated">some end node</param>
        /// <param name = "comparerValue">comparer Value.</param>
        /// <returns>path or null</returns>
        public static Stack<IGraphEdge<T, Number>> FindPath<T>(IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated, IComparer<T> comparerValue)
        {
            Check.ArgumentNullException(graph, "graph");
            Check.ArgumentNullException(start, "start");
            Check.ArgumentNullException(terminated, "terminated");
            Check.ArgumentNullException(comparerValue, "comparerValue");

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

        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, T start, T terminated)
        {
            var startNode = graph[start];
            var terminatedNode = graph[terminated];

            return FordFulkersonAlgorithm(graph, startNode, terminatedNode, Comparer<T>.Default);
        }

        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated)
        {
            return FordFulkersonAlgorithm(graph, start, terminated, Comparer<T>.Default);
        }

        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, T start, T terminated, IComparer<T> comparerValue)
        {
            var startNode = graph[start];
            var terminatedNode = graph[terminated];

            return FordFulkersonAlgorithm(graph, startNode, terminatedNode, comparerValue);
        }

        /// <summary>
        ///   As long as there an open path through the residual graph, 
        ///   send the minimum of the residual capacities on the path.
        ///   The algorithm works only if all weights are integers.
        /// </summary>
        /// <typeparam name = "T">Type for the name of node</typeparam>
        /// <param name = "graph">The residual graph</param>
        /// <param name = "start">some root node</param>
        /// <param name = "terminated">some end node</param>
        /// <param name = "comparerValue">comparer Value</param>
        /// <returns>maximum flot</returns>
        /// <exception cref="ArgumentNullException">If graph , start or terminated is
        /// null.</exception>
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated, IComparer<T> comparerValue)
        {
            Check.ArgumentNullException(graph, "graph");
            Check.ArgumentNullException(start, "start");
            Check.ArgumentNullException(terminated, "terminated");
            Check.ArgumentNullException(comparerValue, "comparerValue");

            var path = FindPath(graph, start, terminated, comparerValue);
            Number flowMax = 0;

            while (path.Count > 0)
            {
                Number bottleneck = path.Min(edge => edge.Value);

                flowMax += bottleneck;
                foreach (var edge in path)
                {
                    edge.Value -=  bottleneck;
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

        public static void Djkstra<T>(this IGraph<T, Number> graph, T start, IDictionary<T, Number> distance, IDictionary<T, T> previous)
        {
            var startNode = graph[start];
            Djkstra(graph, startNode, distance, previous, Comparer<T>.Default);
        }

        public static void Djkstra<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IDictionary<T, Number> distance, IDictionary<T, T> previous)
        {
            Djkstra(graph, start, distance, previous, Comparer<T>.Default);
        }

        public static void Djkstra<T>(this IGraph<T, Number> graph, T start, IDictionary<T, Number> distance, IDictionary<T, T> previous, IComparer<T> comparerValue)
        {
            var startNode = graph[start];
            Djkstra(graph, startNode, distance, previous, comparerValue);
        }

        /// <summary>
        ///   Given a digraph with nonnegative weights on its edges and vertices Start, 
        ///   find a shortest path from Start.
        /// </summary>
        /// <typeparam name = "T">Type for the name of node</typeparam>
        /// <param name = "graph">A digraphe with nonegative weights</param>
        /// <param name = "start">Start vertices for to find a shortest path</param>
        /// <param name = "distance">Shortest distance of start from a node</param>
        /// <param name = "previous">Previous visited node</param>
        /// <param name = "comparerValue">comparer Value.</param>
        /// <exception cref="ArgumentNullException">If graph , start, distance, previous or comparerValue is
        /// null.</exception>
        private static void Djkstra<T>(IGraph<T, Number> graph, IGraphNode<T, Number> start, IDictionary<T, Number> distance, IDictionary<T, T> previous, IComparer<T> comparerValue)
        {
            Check.ArgumentNullException(graph, "graph");
            Check.ArgumentNullException(start, "start");
            Check.ArgumentNullException(distance, "distance");
            Check.ArgumentNullException(previous, "previous");
            Check.ArgumentNullException(comparerValue, "comparerValue");

            foreach (var node in graph.Nodes)
            {
                distance.Add(node.Value, comparerValue.Compare(start.Value, node.Value) == 0 ? 0 : Number.MaxValue);
                previous.Add(node.Value, node.Value);
                node.Marker = false;
            }

            var currentNode = start;
            for (var i = 0; i < graph.Nodes.Count(); i++)
            {
                currentNode.Marker = true;
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
                    if (distance[node.Value] < minValue && ! node.Marker)
                    {
                        currentNode = node;
                        minValue = distance[node.Value];
                    }
                }
            }
        }

    }
}