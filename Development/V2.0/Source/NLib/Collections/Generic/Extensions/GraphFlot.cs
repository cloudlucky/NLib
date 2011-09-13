using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLib.Collections.Generic.Extensions
{
    /// <summary>
    /// It's a set of algorithms for resolve the circulations problems for graphs.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class GraphFlot<T>
    {
        /// <summary>
        /// As long as there an open path through the residual graph, 
        ///  send the minimum of the residual capacities on the path.
        ///  The algorithm works only if all weights are integers.
        /// </summary>
        /// <param name="graph">
        /// The graph 
        /// </param>
        /// <param name="start">
        /// The start node
        /// </param>
        /// <param name="end">
        /// The end node
        /// </param>
        /// <returns>
        /// The flow f from s of maximum flot  
        /// </returns>
        public GraphNodeList<T> FordFulkersonAlgorithm(Graph<T> graph, GraphNode<T> start, GraphNode<T> end)
        {
            return null;
        }


    }
}
