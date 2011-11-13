namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;
    using NLib.Collections.Generic.Extensions;

    using NUnit.Framework;
    using System.Collections;

    [TestFixture]
    public class GraphTest
    {
        [Test]
        public void AddNodeTest()
        {
            var graph = new Graph<string, int>();
            graph.Add("A");
            graph.Add("B");

            Assert.IsNotNull(graph["A"]);
            Assert.IsNotNull(graph["B"]);
        }

        [Test]
        public void AddEdgeTest()
        {
            var graph = new Graph<string, int>();
            graph.Add("A");
            graph.Add("B");

            graph.AddDirectedEdge("A", "B", 12);

            Assert.IsNotNull(graph["A"]);
            Assert.IsNotNull(graph["B"]);
            Assert.AreEqual(12, graph["A", "B"]);
        }

        [Test]
        public void AddEdgeTest2()
        {
            var graph = new Graph<string, int>();
            graph.Add("A");
            graph.Add("B");

            graph.AddDirectedEdge("A", "B");
            graph["A", "B"] = 20;

            Assert.AreEqual(20, graph["A", "B"]);
        }

        [Test]
        public void EnumerableTest()
        {
            var graph = new Graph<string, int> { "A", "B", "C" };

            CollectionAssert.AreEquivalent(new[] { "A", "B", "C" }, graph);
        }

        [Test]
        public void GetAllNodesTest()
        {
            var graph = new Graph<string, int> { "A", "B", "C" };

            CollectionAssert.AreEquivalent(new[] { "A", "B", "C" }, graph.Nodes.Select(x => x.Value));
        }

        [Test]
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
        public void DepthFirstTraversalTest()
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
            foreach (var node in graph.DepthFirstTraversal("A"))
            {
                strSequence = strSequence + node.Value;
            }
            Assert.AreEqual("ABCDEFGH", strSequence);
        }

        [Test]
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
        public void FordFulkersonAlgorithmTestType()
        {
            var graph = new Graph<string> { "a", "b", "c", "d", "e", "f" };
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
        public void FordFulkersonAlgorithmTestBottleneck()
        {
            var graph = new Graph<string> { "s", "a", "b", "t" };
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


        //[Test]
        //public void AddNode()
        //{
        //    var graph = new Graph<string>();
        //    Assert.AreEqual(0, graph.Count);

        //    graph.Add("one");
        //    Assert.IsTrue(graph.Contains("one"));
        //    Assert.AreEqual(1, graph.Count);

        //    var graphNotNull = new Graph<string> { "one" };
        //    Assert.IsTrue(graphNotNull.Contains("one"));
        //    Assert.IsTrue(graphNotNull.Remove("one"));
        //    Assert.AreEqual(0, graph.Count);

        //    graphNotNull.Add(new GraphNode<string>("two"));
        //    Assert.AreEqual(1, graphNotNull.Count);
        //}

        //[Test]
        //public void CountTest1()
        //{
        //    var graph = new Graph<string> { "one", "two", "three", "four", "five", "six" };

        //    var enumNodes = graph.GetEnumerator();
        //    int nbNode = 0;

        //    while (enumNodes.MoveNext())
        //    {
        //        nbNode++;
        //    }

        //    Assert.AreEqual(6, nbNode);
        //    Assert.AreEqual(6, graph.Count);

        //    Assert.IsTrue(graph.Contains("one"));
        //    Assert.IsTrue(graph.Contains("two"));
        //    Assert.IsTrue(graph.Contains("three"));
        //    Assert.IsTrue(graph.Contains("four"));
        //    Assert.IsTrue(graph.Contains("five"));
        //    Assert.IsTrue(graph.Contains("six"));

        //    Assert.IsTrue(graph.Remove("three"));
        //    Assert.AreEqual(5, graph.Count);

        //    Assert.IsFalse(graph.Remove("seven"));

        //    graph.Clear();
        //    Assert.AreEqual(0, graph.Count);
        //}

        //[Test]
        //public void FindByValueTest2()
        //{
        //    var graph = new Graph<string> { "one", "two" };

        //    var nodeOne = graph.GetNodeByValue("one");
        //    var nodeTwo = graph.GetNodeByValue("two");

        //    Assert.AreEqual("one", nodeOne.Value);
        //    Assert.AreEqual("two", nodeTwo.Value);
        //}

        //[Test]
        //public void AddUnidirectedEdgeTest3()
        //{
        //    var graph = new Graph<string> { "one", "two", "three" };

        //    graph.AddUndirectedEdge("one", "two");
        //    graph.AddUndirectedEdge("two", "three");
        //    graph.AddUndirectedEdge("three", "one");

        //    var node = graph.GetNodeByValue("one");

        //    Assert.AreEqual("two", graph.GetNodeNeighbor("one", "two").Value);
        //    Assert.AreEqual("three", node.Edges.First(x => x.To.Value == "three").To.Value);
        //    Assert.IsNull(node.Edges.FirstOrDefault(x => x.To.Value == "one"));

        //    node = node.Neighbors.FindByValue("two");

        //    Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
        //    Assert.AreEqual("three", node.Neighbors.FindByValue("three").Value);
        //    Assert.IsNull(node.Neighbors.FindByValue("two"));

        //    node = node.Neighbors.FindByValue("three");

        //    Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
        //    Assert.AreEqual("two", node.Neighbors.FindByValue("two").Value);
        //    Assert.IsNull(node.Neighbors.FindByValue("three"));
        //}

        //[Test]
        //public void AddDirectEdgeTest4()
        //{
        //    var graph = new Graph<string> { "three", "four", "five", "six" };

        //    var node1 = new GraphNode<string>("one");
        //    var node2 = new GraphNode<string>("two");

        //    graph.Add(node1);
        //    graph.Add(node2);
        //    graph.AddDirectedEdge("one", "two");
        //    graph.AddDirectedEdge("two", "three");
        //    graph.AddDirectedEdge("three", "one");

        //    var node = graph.GetNodeByValue("one");

        //    Assert.AreEqual("two", node.Neighbors.FindByValue("two").Value);
        //    Assert.IsNull(node.Neighbors.FindByValue("one"));
        //    Assert.IsNull(node.Neighbors.FindByValue("three"));

        //    node = node.Neighbors.FindByValue("two");
        //    Assert.AreEqual("three", node.Neighbors.FindByValue("three").Value);
        //    Assert.IsNull(node.Neighbors.FindByValue("two"));
        //    Assert.IsNull(node.Neighbors.FindByValue("one"));

        //    node = node.Neighbors.FindByValue("three");
        //    Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
        //    Assert.IsNull(node.Neighbors.FindByValue("three"));
        //    Assert.IsNull(node.Neighbors.FindByValue("two"));
        //}

        //[Test]
        //public void AddDirectEdgeTest5()
        //{
        //    var graph = new Graph<string>();
        //    graph.Add("one");
        //    graph.Add("two");
        //    graph.Add("three");

        //    graph.AddDirectedEdge("one", "two", 1);
        //    graph.AddDirectedEdge("two", "three", 2);
        //    graph.AddUndirectedEdge("three", "one", 3);

        //    var node = graph.GetNodeByValue("one");

        //    Assert.AreEqual("two", node.Neighbors.FindByValue("two").Value);
        //    Assert.AreEqual(1, node.Costs.ElementAt(0));

        //    node = node.Neighbors.FindByValue("two");
        //    Assert.AreEqual("three", node.Neighbors.FindByValue("three").Value);
        //    Assert.AreEqual(2, node.Costs.ElementAt(0));

        //    node = node.Neighbors.FindByValue("three");
        //    Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
        //    Assert.AreEqual(3, node.Costs.ElementAt(0));

        //    Assert.IsTrue(graph.Remove("three"));
        //}

        //[Test]
        //public void AddDirectEdgeTest6()
        //{
        //    var graph = new Graph<string>();
        //    graph.Add("one");
        //    graph.AddDirectedEdge("one", "one", 1);

        //    for (int i = 0; i < 10; i++)
        //    {
        //        var node = graph.GetNodeByValue("one");
        //        Assert.AreEqual("one", node.Neighbors.ElementAt(0).Value);
        //    }

        //    Assert.IsTrue(graph.Remove("one"));
        //}

        //[Test]
        //public void NodeTest8()
        //{
        //    var graph = new Graph<string>();
        //    graph.Add("a");
        //    Assert.AreEqual(0, graph.GetNodeByValue("a").Neighbors.Count());

        //    try
        //    {
        //        graph.AddDirectedEdge("a", "b");
        //        Assert.Fail();
        //    }
        //    catch
        //    {
        //        Assert.IsTrue(true);
        //    }

        //    try
        //    {
        //        graph.AddDirectedEdge("b", "a");
        //        Assert.Fail();
        //    }
        //    catch
        //    {
        //        Assert.IsTrue(true);
        //    }

        //    try
        //    {
        //        graph.AddDirectedEdge("b", "b");
        //        Assert.Fail();
        //    }
        //    catch
        //    {
        //        Assert.IsTrue(true);
        //    }

        //    try
        //    {
        //        graph.AddDirectedEdge("a", "a");
        //        Assert.IsTrue(true);
        //    }
        //    catch
        //    {
        //        Assert.Fail();
        //    }
        //}

        //[Test]
        //public void RemoveTest9()
        //{
        //    var graph = new Graph<string>();
        //    graph.Add("one");
        //    graph.Add("two");
        //    graph.Add("three");

        //    graph.AddDirectedEdge("one", "one");
        //    graph.AddDirectedEdge("two", "one");
        //    graph.AddDirectedEdge("three", "one");

        //    Assert.IsTrue(graph.Contains("one"));
        //    Assert.IsTrue(graph.Contains("two"));
        //    Assert.IsTrue(graph.Contains("three"));

        //    graph.Remove("one");
        //    Assert.IsFalse(graph.Contains("one"));
        //}
    }
}
