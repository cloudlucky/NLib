// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphFlot.cs" company="">
//   
// </copyright>
// <summary>
//   It's a set of algorithms for resolve the circulations problems for graphs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;

    /// <summary>
    /// It's a set of algorithms for resolve the circulations problems for graphs.
    /// </summary>
    public static class GraphFlot
    {
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, T start, T terminated)
        {
            var startNode = graph[start];
            var terminatedNode = graph[terminated];

            return FordFulkersonAlgorithm(graph, startNode, terminatedNode, Comparer<T>.Default);
        }

        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, T start, T terminated)
        {
                var startNode = graph[start];
                var terminatedNode = graph[terminated];

                return FindPath(graph, startNode, terminatedNode, Comparer<T>.Default);
        }

        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated)
        {
            return FordFulkersonAlgorithm(graph, start, terminated, Comparer<T>.Default);
        }

        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated)
        {
            return FindPath(graph, start, terminated, Comparer<T>.Default);
        }

        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, T start, T terminated, IComparer<T> comparerValue)
        {
            var startNode = graph[start];
            var terminatedNode = graph[terminated];

            return FordFulkersonAlgorithm(graph, startNode, terminatedNode, comparerValue);
        }


        public static Stack<IGraphEdge<T, Number>> FindPath<T>(this IGraph<T, Number> graph, T start, T terminated, IComparer<T> comparerValue)
        {
            var startNode = graph[start];
            var terminatedNode = graph[terminated];

            return FindPath(graph, startNode, terminatedNode, comparerValue);
        }
        /// <summary>
        /// As long as there an open path through the residual graph, 
        /// send the minimum of the residual capacities on the path.
        /// The algorithm works only if all weights are integers.
        /// </summary>
        /// <typeparam name="T">Type for the name of node</typeparam>
        /// <param name="graph">The residual graph</param>
        /// <param name="start">some root node</param>
        /// <param name="terminated">some end node</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <returns>maximum flot</returns>
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated, IComparer<T> comparerValue)
        {
            var path = FindPath(graph, start, terminated, comparerValue);
            Number flowMax = 0;

            while (path.Count > 0) 
            {
                Number bottleneck = path.Min(edge => edge.Value);

                flowMax += bottleneck;
                foreach (var edge in path)
                {
                    graph.AddUndirectedEdge(edge.To.Value, edge.From.Value, edge.Value - bottleneck);
                    var edgeReversed = graph.GetEdge(edge.To.Value, edge.From.Value);

                    if (edgeReversed == null)
                        graph.AddUndirectedEdge(edge.To.Value, edge.From.Value, bottleneck);
                    else
                        edgeReversed.Value += bottleneck;

                    if (edge.Value == 0)
                        graph.RemoveDirectedEdge(edge);
                 }

                 path = FindPath(graph, start, terminated, comparerValue);
            } 

            return flowMax;
        }

        /// <summary>
        /// Find a path  
        /// </summary>
        /// <typeparam name="T">Type for the name of node</typeparam>
        /// <param name="graph">The graph</param>
        /// <param name="start">some root node</param>
        /// <param name="terminated">some end node</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <returns>path or null</returns>
        public static Stack<IGraphEdge<T, Number>> FindPath<T>(IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated, IComparer<T> comparerValue)
        {
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
                else
                    if (path.Count > 0)
                    {
                        currentNode = path.Peek().From;
                        markedEdge.Push(path.Pop());
                    }
                    else
                        currentNode = null;
            }

            if (currentNode == null || comparerValue.Compare(currentNode.Value, terminated.Value) != 0)
                path.Clear();

            path.ForEach(edge => edge.Marked = false);
            markedEdge.ForEach(edge => edge.Marked = false);
            
            return path;
        }

   

    }
}