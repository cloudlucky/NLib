namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;
    using NLib.Collections.Generic.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class GraphTest
    {
        [Test]
        [Category("Serie Graph Add")]
        public void AddNodeTest()
        {
            var graph = new Graph<string, int>();
            graph.Add("A");
            graph.Add("B");

            Assert.IsNotNull(graph["A"]);
            Assert.IsNotNull(graph["B"]);
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest()
        {
            var graph = new Graph<string, int> { "A", "B" };

            graph.AddDirectedEdge("A", "B", 12);

            Assert.IsNotNull(graph["A"]);
            Assert.IsNotNull(graph["B"]);
            Assert.AreEqual(12, graph["A", "B"]);
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest2()
        {
            var graph = new Graph<string, int> { "A", "B" };

            graph.AddDirectedEdge("A", "B");
            graph["A", "B"] = 20;

            Assert.AreEqual(20, graph["A", "B"]);
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest3()
        {
            var graph = new Graph<string, int> { "TO", "FROM" };

            graph.AddUndirectedEdge("FROM", "TO", 10);
            Assert.AreEqual(10, graph.GetEdge("FROM", "TO").Value);
            Assert.AreEqual(10, graph.GetEdge("TO", "FROM").Value);
           
            graph.AddUndirectedEdge("FROM", "TO", 20);
            Assert.AreEqual(20, graph.GetEdge("FROM", "TO").Value);
            Assert.AreEqual(20, graph.GetEdge("TO", "FROM").Value);
             
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest4()
        {
            var graph = new Graph<string, int> { "FROM", "TO" };

            graph.AddDirectedEdge("FROM", "TO", 10);
            graph.AddDirectedEdge("TO", "FROM", 10);
            Assert.AreEqual(10, graph.GetEdge("FROM", "TO").Value);
            Assert.AreEqual(10, graph.GetEdge("TO", "FROM").Value);

            graph.AddDirectedEdge("FROM", "TO", 20);
            Assert.AreEqual(20, graph.GetEdge("FROM", "TO").Value);
            Assert.AreEqual(10, graph.GetEdge("TO", "FROM").Value);
            
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest5()
        {
            var graph = new Graph<string, int> { "FROM", "TO" };
            graph.AddDirectedEdge("FROM","TO", 1);
            Assert.Pass();
            graph.AddUndirectedEdge("TO","FROM",10);
            Assert.Fail();
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest6()
        {
            var graph = new Graph<string, int> { "FROM", "TO" };
            graph.AddDirectedEdge("TO", "FROM", 1);
            Assert.Pass();
            graph.AddUndirectedEdge("TO", "FROM", 10);
            Assert.Fail();
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest7()
        {
            var graph = new Graph<string, int> { "FROM", "TO" };
            graph.AddDirectedEdge("TO", "FROM", 1);
            Assert.Pass();
            graph.AddUndirectedEdge("FROM", "TO", 10);
            Assert.Fail();
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest8()
        {
            var graph = new Graph<string, int> { "FROM", "TO" };
            graph.AddUndirectedEdge("FROM", "TO", 10);
            Assert.Pass();
            graph.AddDirectedEdge("TO", "FROM", 1);
            Assert.Fail();
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest9()
        {
           var graph = new Graph<string, int> { "TO", "FROM" };
           graph.AddDirectedEdge("TO", "FROM", 1);
           Assert.AreEqual(1, graph.GetEdge("TO", "FROM").Value);
           graph.AddDirectedEdge("TO", "FROM", 10);
           Assert.AreEqual(10, graph.GetEdge("TO", "FROM").Value); 
        }

        [Test]
        [Category("Serie Graph Add")]
        public void AddEdgeTest10()
        {
            var graph = new Graph<string, int> { "TO", "FROM" };
            graph.AddUndirectedEdge("TO", "FROM", 1);
            Assert.AreEqual(1, graph.GetEdge("TO", "FROM").Value);
            Assert.AreEqual(1, graph.GetEdge("FROM", "TO").Value);
            graph["TO", "FROM"] = 10;
           
            Assert.AreEqual(10, graph.GetEdge("FROM", "TO").Value);
            Assert.AreEqual(10, graph.GetEdge("TO", "FROM").Value);

            graph["FROM", "TO"] = 1;
            Assert.AreEqual(1, graph.GetEdge("FROM", "TO").Value);
            Assert.AreEqual(1, graph.GetEdge("TO", "FROM").Value);
        }

        [Test]
        [Category("Serie Graph Add")]
        [Ignore("Not Implemented")]
        public void AddEdgeTest11()
        {
            var graph = new Graph<string, int> { "TO", "FROM" };
            graph.AddUndirectedEdge("TO", "FROM", 1);
            Assert.AreEqual(1, graph.GetEdge("TO", "FROM").Value);
            Assert.AreEqual(1, graph.GetEdge("FROM", "TO").Value);

            graph.GetEdge("TO", "FROM").Value = 10;
            
            Assert.AreEqual(10, graph.GetEdge("FROM", "TO").Value);
            Assert.AreEqual(10, graph.GetEdge("TO", "FROM").Value);

        }

        [Test]
        [Category("Serie Graph Remove")]
        public void RemoveEdgeTest()
        {
            var graph = new Graph<string, int>();
            graph.Add("A");
            graph.Add("B");
            graph.AddUndirectedEdge("A","B",0);
            Assert.NotNull(graph.GetEdge("A", "B"));
            Assert.AreEqual(0, graph.GetEdge("A", "B").Value);
            graph.RemoveEdge(graph.GetEdge("A","B"));
            Assert.Null(graph.GetEdge("A","B"));
        }

        [Test]
        [Category("Serie Graph Remove")]
        public void RemoveEdgeTest2()
        {
            var graph = new Graph<string, int> { "A", "B" };
            graph.AddUndirectedEdge("A", "B", 0);
            Assert.NotNull(graph.GetEdge("B", "A"));
            Assert.AreEqual(0, graph.GetEdge("A", "B").Value);
            graph.RemoveEdge(graph.GetEdge("A", "B"));
            Assert.Null(graph.GetEdge("B", "A"));
        }


        [Test]
        [Category("Serie Graph Other")]
        public void ValueUndirectedEdgeTest2()
        {
            var graph = new Graph<string, int> { "A", "B" };

            graph.AddUndirectedEdge("A", "B", 10);
            Assert.NotNull(graph.GetEdge("A", "B"));
            Assert.AreEqual(10, graph.GetEdge("A", "B").Value);

            var edge = graph.GetEdge("B", "A");
            edge.Value = 20;
         
            Assert.AreNotEqual(20, graph.GetEdge("A","B").Value);
            graph.AddEdge(edge);
            Assert.AreEqual(20, graph.GetEdge("A", "B").Value);
            
        }

        [Test]
        [Category("Serie Graph Other")]
        public void EnumerableTest()
        {
            var graph = new Graph<string, int> { "A", "B", "C" };

            CollectionAssert.AreEquivalent(new[] { "A", "B", "C" }, graph);
        }

        [Test]
        [Category("Serie Graph Other")]
        public void GetAllNodesTest()
        {
            var graph = new Graph<string, int> { "A", "B", "C" };

            CollectionAssert.AreEquivalent(new[] { "A", "B", "C" }, graph.Nodes.Select(x => x.Value));
        }

        [Test]
        [Category("Serie Graph Other")]
        public void EdgesTest()
        {
            var graph = new Graph<string, int> { "A", "B", "C" };

            graph.AddDirectedEdge("A", "B", 5);
            graph.AddDirectedEdge("A", "C", 10);

            Assert.AreEqual(2, graph["A"].Edges.Count());
            Assert.AreEqual(5, graph["A"].Edges.Min(x => x.Value));
            Assert.AreEqual(10, graph["A"].Edges.Max(x => x.Value));
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void DepthFirstTraversalTest()
        {
            var graph = new Graph<string, int> { "A", "B", "C", "D", "E", "F", "G", "H" };

            graph.AddDirectedEdge("A", "B", 0);
            graph.AddDirectedEdge("A", "H", 0);
            graph.AddDirectedEdge("H", "H", 0);
            graph.AddDirectedEdge("H", "G", 0);
            graph.AddDirectedEdge("B", "C", 0);
            graph.AddDirectedEdge("C", "D", 0);
            graph.AddDirectedEdge("C", "E", 0);
            graph.AddDirectedEdge("C", "F", 0);
            graph.AddDirectedEdge("F", "G", 0);
            graph.AddDirectedEdge("G", "A", 0);

            string strSequence = string.Empty;
            foreach (var node in graph.DepthFirstTraversal("A"))
            {
                strSequence = strSequence + node.Value;
            }
            Assert.AreEqual("ABCDEFGH", strSequence);
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void BreadthFirstTraversalTest()
        {
            var graph = new Graph<string, int> { "A", "B", "C", "D", "E", "F", "G", "H" };

            graph.AddUndirectedEdge("A", "B", 0);
            graph.AddUndirectedEdge("A", "H", 0);
            graph.AddUndirectedEdge("H", "H", 0);
            graph.AddUndirectedEdge("H", "G", 0);
            graph.AddUndirectedEdge("B", "C", 0);
            graph.AddUndirectedEdge("C", "D", 0);
            graph.AddUndirectedEdge("C", "E", 0);
            graph.AddUndirectedEdge("C", "F", 0);
            graph.AddUndirectedEdge("F", "G", 0);
            graph.AddUndirectedEdge("G", "A", 0);

            string strSequence = string.Empty;
            foreach (var node in graph.BreathFirstTraversal("A"))
            {
                strSequence = strSequence + node.Value;
            }
            Assert.AreEqual("ABHGCFDE", strSequence);

        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void FordFulkersonAlgorithmTest()
        {
            var graph = new Graph<long> { 1, 2, 3, 4, 5, 6 };
            graph.AddDirectedEdge(1, 2, 8);
            graph.AddDirectedEdge(1, 3, 6);
            graph.AddDirectedEdge(2, 4, 4);
            graph.AddDirectedEdge(2, 5, 8);
            graph.AddDirectedEdge(3, 5, 6);
            graph.AddDirectedEdge(5, 4, 2);
            graph.AddDirectedEdge(5, 6, 8);
            graph.AddDirectedEdge(4, 6, 6);

            var start = graph.GetNode(1);
            var terminated = graph.GetNode(6);

            var flowMax = graph.FordFulkersonAlgorithm(start, terminated);
            Assert.AreEqual(new Number(14), flowMax);
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void FordFulkersonAlgorithmTest2()
        {
            var graph = new Graph<int> { 1, 2, 3, 4, 5, 6 };
            graph.AddDirectedEdge(1, 2, 8);
            graph.AddDirectedEdge(1, 3, 6);
            graph.AddDirectedEdge(2, 4, 4);
            graph.AddDirectedEdge(2, 5, 8);
            graph.AddDirectedEdge(3, 5, 6);
            graph.AddDirectedEdge(5, 4, 2);
            graph.AddDirectedEdge(5, 6, 8);
            graph.AddDirectedEdge(4, 6, 6);

            var start = graph.GetNode(1);
            var terminated = graph.GetNode(6);

            var flowMax = graph.FordFulkersonAlgorithm(start, terminated);
            Assert.AreEqual(new Number(14), flowMax);
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void FordFulkersonAlgorithmTest3()
        {
            var graph = new Graph<char, Number> { 's', 'o', 'p', 'r', 'q', 't' };
            graph.AddDirectedEdge('s', 'o', 3);
            graph.AddDirectedEdge('s', 'p', 3);
            graph.AddDirectedEdge('o', 'p', 2);
            graph.AddDirectedEdge('o', 'q', 3);
            graph.AddDirectedEdge('p', 'r', 2);
            graph.AddDirectedEdge('r', 't', 3);
            graph.AddDirectedEdge('q', 'r', 4);
            graph.AddDirectedEdge('q', 't', 2);

            var flowMax = graph.FordFulkersonAlgorithm('s', 't');
            Assert.AreEqual(new Number(5), flowMax);
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void FordFulkersonAlgorithmTestType()
        {
            var graph = new Graph<string, Number> { "a", "b", "c", "d", "e", "f" };
            graph.AddDirectedEdge("a", "b", 8);
            graph.AddDirectedEdge("a", "c", 6);
            graph.AddDirectedEdge("b", "d", 4);
            graph.AddDirectedEdge("b", "e", 8);
            graph.AddDirectedEdge("c", "e", 6);
            graph.AddDirectedEdge("e", "d", 2);
            graph.AddDirectedEdge("e", "f", 8);
            graph.AddDirectedEdge("d", "f", 6);

            var start = graph.GetNode("a");
            var terminated = graph.GetNode("f");
            var flowMax = graph.FordFulkersonAlgorithm(start, terminated);

            Assert.AreEqual(new Number(14), flowMax);
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void FordFulkersonAlgorithmTestBottleneck()
        {
            var graph = new Graph<string, Number> { "s", "a", "b", "t" };
            graph.AddDirectedEdge("s", "a", 20);
            graph.AddDirectedEdge("s", "b", 10);
            graph.AddDirectedEdge("a", "t", 10);
            graph.AddDirectedEdge("a", "b", 30);
            graph.AddDirectedEdge("b", "t", 20);

            var start = graph.GetNode("s");
            var terminated = graph.GetNode("t");
            var flowMax = graph.FordFulkersonAlgorithm(start, terminated);

            Assert.AreEqual(new Number(30), flowMax);
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void FordFulkersonAlgorithmTestBottleneck2()
        {
            var graph = new Graph<string, Number> { "s", "a", "b", "c", "d", "e", "f", "g", "h", "i", "t" };
            graph.AddDirectedEdge("s", "a", 100);
            graph.AddDirectedEdge("a", "b", 100);
            graph.AddDirectedEdge("c", "d", 30);
            graph.AddDirectedEdge("d", "f", 30);
            graph.AddDirectedEdge("e", "f", 40);
            graph.AddDirectedEdge("f", "s", 40);
            graph.AddDirectedEdge("b", "g", 50);
            graph.AddDirectedEdge("g", "h", 60);
            graph.AddDirectedEdge("h", "i", 50);
            graph.AddDirectedEdge("b", "c", 60);
            graph.AddDirectedEdge("b", "e", 70);
            graph.AddDirectedEdge("i", "t", 80);

            var start = graph.GetNode("s");
            var terminated = graph.GetNode("t");
            var flowMax = graph.FordFulkersonAlgorithm(start, terminated);

            Assert.AreEqual(new Number(50), flowMax);
        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void FindPathTest()
        {
            var graph = new Graph<string, Number> { "s", "a", "b", "c", "t" };
            graph.AddDirectedEdge("s","a");
            graph.AddDirectedEdge("a","b");
            graph.AddDirectedEdge("b", "c");
            graph.AddDirectedEdge("a", "t");

            var start = graph.GetNode("s");
            var terminated = graph.GetNode("t");
            var path = graph.FindPath(start, terminated);

            Assert.AreEqual(2, path.Count());
            
            Assert.AreEqual("a", path.Peek().From.Value);
            Assert.AreEqual("t", path.Peek().To.Value);
            path.Pop();

            Assert.AreEqual("s", path.Peek().From.Value);
            Assert.AreEqual("a", path.Peek().To.Value);
            path.Pop();

        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void DjkstraTest1()
        {
            var graph = new Graph<string, Number> { "A", "B", "C", "D", "E", "F", "G", "H" };
            graph.AddDirectedEdge("B", "A", 3);
            graph.AddDirectedEdge("B", "D", 6);
            graph.AddDirectedEdge("B", "C", 2);
            graph.AddDirectedEdge("A", "D", 2);
            graph.AddDirectedEdge("A", "E", 5);
            graph.AddDirectedEdge("A", "C", 8);
            graph.AddDirectedEdge("D", "C", 4);
            graph.AddDirectedEdge("D", "F", 4);
            graph.AddDirectedEdge("D", "H", 2);
            graph.AddDirectedEdge("D", "G", 0);
            graph.AddDirectedEdge("E", "C", 3);
            graph.AddDirectedEdge("E", "F", 3);
            graph.AddDirectedEdge("F", "E", 2);
            graph.AddDirectedEdge("G", "H", 1);
            graph.AddDirectedEdge("G", "E", 2);
            graph.AddDirectedEdge("H", "G", 2);
            graph.AddDirectedEdge("H", "F", 1);
            graph.AddDirectedEdge("H", "C", 2);

            var start = graph.GetNode("B");
 
            IDictionary<string, Number> distance = new Dictionary<string, Number>();
            IDictionary<string, string> previous = new Dictionary<string, string>();

            graph.Djkstra(start, distance, previous);

            Assert.AreEqual(new Number(3), distance["A"]);
            Assert.AreEqual(new Number(0), distance["B"]);
            Assert.AreEqual(new Number(2), distance["C"]);
            Assert.AreEqual(new Number(5), distance["D"]);
            Assert.AreEqual(new Number(7), distance["E"]);
            Assert.AreEqual(new Number(7), distance["F"]);
            Assert.AreEqual(new Number(5), distance["G"]);
            Assert.AreEqual(new Number(6), distance["H"]);

            Assert.AreEqual("B", previous["A"]);
            Assert.AreEqual("B", previous["B"]);
            Assert.AreEqual("B", previous["C"]);
            Assert.AreEqual("A", previous["D"]);
            Assert.AreEqual("G", previous["E"]);
            Assert.AreEqual("H", previous["F"]);
            Assert.AreEqual("D", previous["G"]);
            Assert.AreEqual("G", previous["H"]);

        }

        [Test]
        [Category("Serie Graph Algorithm")]
        public void DjkstraTest2()
        {
            var graph = new Graph<string, Number> { "A" };
            graph.AddDirectedEdge("A", "A", -2);

            var start = graph.GetNode("A");

            IDictionary<string, Number> distance = new Dictionary<string, Number>();
            IDictionary<string, string> previous = new Dictionary<string, string>();

            graph.Djkstra(start, distance, previous);

            Assert.AreEqual(new Number(-2),distance["A"]);
            Assert.AreEqual("A", previous["A"]);

        }

        [Test]
        [Category("Serie Graph Other")]
        public void CloneGraphTest()
        {
            var graph = new Graph<string, Number> { "A", "B","C"};
            graph.AddDirectedEdge("A","B",1);
            graph.AddDirectedEdge("B","A",-1);
            graph.GetEdge("B", "A").Marked = false;
            graph["A"].Marker = false;
            Assert.AreEqual(new Number(1), graph.GetEdge("A", "B").Value);
            Assert.AreEqual(new Number(-1), graph.GetEdge("B","A").Value);
            Assert.AreEqual(false, graph.GetEdge("B", "A").Marked);
            Assert.AreEqual(false, graph["A"].Marker);
            var graph2 = (Graph<string, Number>)graph.Clone();
            graph2.AddUndirectedEdge("B", "B", 100);
            graph2.GetEdge("B", "A").Marked = true;

            graph2["A"].Marker = true;

            Assert.AreEqual(new Number(100), graph2.GetEdge("B", "B").Value);
            Assert.AreEqual(true, graph2.GetEdge("B", "A").Marked);
            Assert.AreEqual(true, graph2["A"].Marker);
            Assert.AreEqual(new Number(1), graph.GetEdge("A", "B").Value);
            Assert.AreEqual(new Number(-1), graph.GetEdge("B", "A").Value);
            Assert.AreEqual(false, graph.GetEdge("B", "A").Marked);
            Assert.AreEqual(false, graph["A"].Marker);

        }

        [Test]
        [Category("Serie Graph Other")]
        public void GraphEdgeFactoryTest()
        {
            var nodeA = new GraphNode<string, Number>("NodeA");
            var nodeB = new GraphNode<string, Number>("NodeB");

            var factory = GraphEdgeFactory.GetFactory("NLib.Collections.Generic.UndirectedEdgeFactory");
            var edge = factory.Create<string, Number>();
            Assert.IsTrue(edge.GetType().Name.Contains("UndirectedEdge"));

            edge = factory.Create(nodeA, nodeB, 12345);
            Assert.IsTrue(edge.Value == 12345 && (edge.To.Value.Equals("NodeB") && edge.From.Value.Equals("NodeA")));

            factory = GraphEdgeFactory.GetFactory("NLib.Collections.Generic.DirectEdgeFactory");
            edge = factory.Create<string, Number>();
            Assert.IsTrue(edge.GetType().Name.Contains("DirectedEdge"));

            edge = factory.Create(nodeA, nodeB, 12345);
            Assert.IsTrue(edge.Value == 12345 && (edge.To.Value.Equals("NodeB") && edge.From.Value.Equals("NodeA")));
        }

    }
}
