namespace NLib.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

        public abstract class GraphEdgeFactory
        {
             public static GraphEdgeFactory GetFactory(string typeName)
             {
                Type type = Type.GetType(typeName);

                Object objEdgeFactory = type.InvokeMember(null,
                                               BindingFlags.DeclaredOnly |
                                               BindingFlags.Public | 
                                               BindingFlags.NonPublic |
                                               BindingFlags.Instance |
                                               BindingFlags.CreateInstance,
                                               null,null, null);

                return ((GraphEdgeFactory)objEdgeFactory);
             }
            
             public abstract GraphEdge<T, TCost> Create<T, TCost>();
             public abstract GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to);
             public abstract GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value);
              
        }

        class UndirectedEdgeFactory : GraphEdgeFactory
        {

            public override GraphEdge<T, TCost> Create<T, TCost>()
            {
                return (new UndirectedEdge<T, TCost>());
            }

            public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
            {
                return (new UndirectedEdge<T, TCost>(from, to));
            }

            public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
            {
                return (new UndirectedEdge<T, TCost>(from, to, value));
            }

        }

        class DirectEdgeFactory : GraphEdgeFactory
        {

            public override GraphEdge<T, TCost> Create<T, TCost>()
            {
                return (new DirectedEdge<T, TCost>());
            }

            public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to)
            {
                return (new DirectedEdge<T, TCost>(from, to));
            }

            public override GraphEdge<T, TCost> Create<T, TCost>(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value)
            {
                return ( new DirectedEdge<T, TCost>(from, to, value) );
            }

        }

        public class UndirectedEdge<T, TCost> :  GraphEdge<T, TCost>
        {

            public UndirectedEdge() { }

            public UndirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to) : base(from, to) { }

            public UndirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to, TCost value) : base(from, to, value) { }

            private byte inverter;
            public override TCost Value
            {
                get
                {
                    return base.Value;
                }
                set
                {
                    if (inverter == 0)
                    {
                        inverter = 1;
                        base.Value = value;
                    }
                    else
                    {
                        inverter = 0;
                        base.Value = value;
                        this.To.Edges.FirstOrDefault(e => e.To == this.From).Value = value;
                    }
                }
            }
        }

        public class DirectedEdge<T, TCost> :  GraphEdge<T, TCost>
        {    
            public DirectedEdge() { }
            public DirectedEdge(GraphNode<T, TCost> from, GraphNode<T, TCost> to) : base(from, to) { }
            public DirectedEdge(GraphNode<T,TCost> from, GraphNode<T,TCost> to, TCost value) : base(from, to, value) { }
        }

}
