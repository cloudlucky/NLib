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
    using System.Text;

    using NLib.Collections.Generic;

    /// <summary>
    /// It's a set of algorithms for resolve the circulations problems for graphs.
    /// </summary>
    public static class GraphFlot
    {
        public static ulong FordFulkersonAlgorithm<T>(this IGraph<T, ulong> graph, IGraphNode<T, ulong> start, IGraphNode<T, ulong> terminated)
        {
            return FordFulkersonAlgorithm<T>(graph, start, terminated, Comparer<T>.Default);
        }

        /// <summary>
        /// As long as there an open path through the residual graph, 
        ///  send the minimum of the residual capacities on the path.
        ///  The algorithm works only if all weights are integers.
        /// </summary>
        /// <typeparam name="T">
        ///  Type for the name of node
        /// </typeparam>
        /// <param name="graph">
        /// The graph to evaluate
        /// </param>
        /// <param name="start">
        /// some root node
        /// </param>
        /// <param name="terminated">
        /// some end node
        /// </param>
        /// <param name="comparerValue">
        /// comparer Value.
        /// </param>
        /// <returns>
        /// maximum flot  
        /// </returns>
       
        public static ulong FordFulkersonAlgorithm<T>(this IGraph<T, ulong> graph, IGraphNode<T, ulong> start, IGraphNode<T, ulong> terminated, IComparer<T> comparerValue)
        {
            ulong flowMax = 0;
            ulong bottleneck = ulong.MaxValue;
            var stack = new Stack<IGraphEdge<T, ulong>>();
            var currentNode = start;

            IGraphEdge<T, ulong> currentEdge = currentNode.Edges.Where(e => (e.Capacity - e.Flow) > 0).FirstOrDefault();
            graph.Nodes.ForEach(node => node.Edges.ForEach(edge => edge.Flow = 0));

            while (currentEdge != default(IGraphEdge<T, ulong>))
            {
                while (comparerValue.Compare(currentNode.Value, terminated.Value) != 0)
                {
                    if ((currentEdge.Capacity - currentEdge.Flow) > 0)
                    {
                        if (bottleneck > (currentEdge.Capacity - currentEdge.Flow))
                        {
                            bottleneck = currentEdge.Capacity - currentEdge.Flow;
                        }

                        stack.Push(currentEdge);
                        currentNode = currentEdge.To;
                    }

                    currentEdge = currentNode.Edges.Where(e => (e.Capacity - e.Flow) > 0).FirstOrDefault();
                }

                stack.ForEach(e => e.Flow = e.Flow + bottleneck);
                stack.Clear();

                flowMax = flowMax + bottleneck;
                currentNode = start;
                currentEdge = currentNode.Edges.Where(e => (e.Capacity - e.Flow) > 0).FirstOrDefault();

                bottleneck = ulong.MaxValue;
            }

            return flowMax;
        }
    }
}