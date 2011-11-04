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
    using System.Linq;
    using System.Text;

    using NLib.Collections.Generic;

    /// <summary>
    /// It's a set of algorithms for resolve the circulations problems for graphs.
    /// </summary>
    public static class GraphFlot
    {
   //     public static TCost FordFulkersonAlgorithm<T, TCost>(this IGraph<T, TCost> graph, IGraphNode<T, TCost> start, IGraphNode<T, TCost> terminated)
   //     {
          //  return FordFulkersonAlgorithm(graph, start, terminated, Comparer<TCost>.Default, Comparer<T>.Default);
   //         return default(TCost);
  //      }

        /// <summary>
        /// As long as there an open path through the residual graph, 
        ///  send the minimum of the residual capacities on the path.
        ///  The algorithm works only if all weights are integers.
        /// </summary>
        /// <param name="graph">
        /// The graph to evaluate
        /// </param>
        /// <param name="start">
        ///  The some root node
        /// </param>
        /// <param name="terminated">
        /// The some end node
        /// </param>
        /// <returns>
        /// Le maximum flot  
        /// </returns>
        public static int FordFulkersonAlgorithm(this IGraph<int, int> graph, IGraphNode<int, int> start, IGraphNode<int, int> terminated )
        {
            // init
            foreach (var node in graph.Nodes)
            {
                foreach (var edge in node.Edges)
                {
                    edge.Flow = 0; 
                }
            }

            var stack = new Stack<IGraphEdge<int, int>>();
            int flowMax = 0;
            bool edgefound = false;
            var currentNode = start;
            IGraphEdge<int, int> currentEdge = null;
            foreach (var e in currentNode.Edges)
            {
                if ((e.Value - e.Flow) > 0)
                {
                    currentEdge = e;
                    edgefound = true;
                    break;
                }
            }

            int min = int.MaxValue;

            while (edgefound == true)
            {
                

                while (currentNode.Value != terminated.Value)
                {
                    int capacityMin = currentEdge.Value - currentEdge.Flow;
                    if (capacityMin > 0)
                    {
                        if (min > capacityMin)
                        {
                            min = capacityMin;
                        }

                        stack.Push(currentEdge);
                        currentNode = currentEdge.To;
                    }

                    foreach (var e in currentNode.Edges)
                    {
                        if ((e.Value - e.Flow) > 0)
                        {
                            currentEdge = e;
                            break;
                        }
                    }
                }

                stack.ForEach(e => e.Flow = e.Flow + min);
                stack.Clear();
                flowMax = flowMax + min;

                currentNode = start;
                edgefound = false;
                foreach (var e in currentNode.Edges)
                {
                    if ((e.Value - e.Flow) > 0)
                    {
                        currentEdge = e;
                        edgefound = true;
                        break;
                    }
                }

                min = int.MaxValue;
            }

            return flowMax;
        }
    } 
                                  
}

  