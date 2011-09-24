// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GraphEdge.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;
    using System.Linq;

    public class GraphEdge<T, TCost>
    {
        protected GraphEdge()
        {
        }

        public GraphEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
            : this(from, to, default(TCost))
        {
        }

        public GraphEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
        {
            this.From = from;
            this.To = to;
            this.Value = value;
        }

        public TCost Value { get; set; }

        public GraphNode<T, TCost> From { get; protected set; }

        public GraphNode<T, TCost> To { get; protected set; }
    }

    public class GraphEdgeSet<T, TCost> : HashSet<GraphEdge<T, TCost>>
    {
        public TCost this[T to]
        {
            get
            {
                // TODO use comparer, not equals
                return this.FirstOrDefault(x => x.To.Value.Equals(to)).Value;
            }
            set
            {
                // TODO use comparer, not equals
                this.FirstOrDefault(x => x.To.Value.Equals(to)).Value = value;
                // TODO handle exception
                // TODO create edge if not exists
            }
        }
    }
}