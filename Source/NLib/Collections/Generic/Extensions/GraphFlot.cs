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
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Numerics;
    using System.Text;

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
        /// As long as there an open path through the residual graph, 
        /// send the minimum of the residual capacities on the path.
        /// The algorithm works only if all weights are integers.
        /// </summary>
        /// <typeparam name="T">Type for the name of node</typeparam>
        /// <param name="graph">The graph to evaluate</param>
        /// <param name="start">some root node</param>
        /// <param name="terminated">some end node</param>
        /// <param name="comparerValue">comparer Value.</param>
        /// <returns>maximum flot</returns>
        public static Number FordFulkersonAlgorithm<T>(this IGraph<T, Number> graph, IGraphNode<T, Number> start, IGraphNode<T, Number> terminated, IComparer<T> comparerValue)
        {
            Number flowMax = 0;
            var bottleneck = Number.MaxValue;
            var stack = new Stack<IGraphEdge<T, Number>>();
            var currentNode = start;

            graph.Nodes.ForEach(node => node.Edges.ForEach(edge => edge.Flow = 0));
            var currentEdge = currentNode.Edges.FirstOrDefault(e => e.Value - e.Flow > 0);

            while (currentEdge != default(IGraphEdge<T, Number>))
            {
                while (comparerValue.Compare(currentNode.Value, terminated.Value) != 0)
                {
                    if ((currentEdge.Value - currentEdge.Flow) > 0)
                    {
                        if (bottleneck > (currentEdge.Value - currentEdge.Flow))
                        {
                            bottleneck = currentEdge.Value - currentEdge.Flow;
                        }

                        stack.Push(currentEdge);
                        currentNode = currentEdge.To;
                    }

                    currentEdge = currentNode.Edges.FirstOrDefault(e => e.Value - e.Flow > 0);
                }

                stack.ForEach(e => e.Flow += bottleneck);
                stack.Clear();

                flowMax += bottleneck;
                currentNode = start;
                currentEdge = currentNode.Edges.FirstOrDefault(e => e.Value - e.Flow > 0);

                bottleneck = Number.MaxValue;
            }

            return flowMax;
        }
    }
}