namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NLib.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class GraphTest
    {
        [Test]
        public void GenerateTest1()
        {
            Graph<string> graph = new Graph<string>();

            graph.AddNode("one");
            graph.AddNode("two");
            graph.AddNode("three");
            graph.AddNode("four");
            graph.AddNode("five");
            graph.AddNode("six");

            Assert.AreEqual(6, graph.Count());

            Assert.IsTrue(graph.Contains("one"));
            Assert.IsTrue(graph.Contains("two"));
            Assert.IsTrue(graph.Contains("three"));
            Assert.IsTrue(graph.Contains("four"));
            Assert.IsTrue(graph.Contains("five"));
            Assert.IsTrue(graph.Contains("six"));

            Assert.IsTrue(graph.Remove("three"));
            Assert.AreEqual(5, graph.Count());

            graph.Clear();
            Assert.AreEqual(0, graph.Count());

        }

        [Test]
        public void GenerateTest2()
        {
            var graph = new Graph<string>();
            graph.AddNode("one");
            graph.AddNode("two");

            var nodeOne = graph.Nodes.FindByValue("one");
            var nodeTwo = graph.Nodes.FindByValue("two");

            Assert.AreEqual("one", nodeOne.Value);
            Assert.AreEqual("two", nodeTwo.Value);
        }

        [Test]
        public void GenerateTest3()
        {
            var graph = new Graph<string>();

            graph.AddNode("one");
            graph.AddNode("two");
            graph.AddNode("three");

            graph.AddUndirectedEdge("one", "two");
            graph.AddUndirectedEdge("two", "three");
            graph.AddUndirectedEdge("three", "one");

            GraphNode<string> node = graph.Nodes.FindByValue("one");

            Assert.AreEqual("two", node.Neighbors.FindByValue("two").Value);
            Assert.AreEqual("three", node.Neighbors.FindByValue("three").Value);
            Assert.IsNull(node.Neighbors.FindByValue("one"));

            node = node.Neighbors.FindByValue("two");

            Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
            Assert.AreEqual("three", node.Neighbors.FindByValue("three").Value);
            Assert.IsNull(node.Neighbors.FindByValue("two"));

            node = node.Neighbors.FindByValue("three");

            Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
            Assert.AreEqual("two", node.Neighbors.FindByValue("two").Value);
            Assert.IsNull(node.Neighbors.FindByValue("three"));    
        }

        [Test]
        public void GenerateTest4()
        {
             var graph = new Graph<string>();

             graph.AddNode("one");
             graph.AddNode("two");
             graph.AddNode("three");
             graph.AddNode("four");
             graph.AddNode("five");
             graph.AddNode("six");

             graph.AddDirectedEdge("one", "two");
             graph.AddDirectedEdge("two", "three");
             graph.AddDirectedEdge("three", "one");
            

            GraphNode<string> node = graph.Nodes.FindByValue("one");

            Assert.AreEqual("two", node.Neighbors.FindByValue("two").Value);
            Assert.IsNull(node.Neighbors.FindByValue("one"));
            Assert.IsNull(node.Neighbors.FindByValue("three"));

            node = node.Neighbors.FindByValue("two");
            Assert.AreEqual("three", node.Neighbors.FindByValue("three").Value);
            Assert.IsNull(node.Neighbors.FindByValue("two"));
            Assert.IsNull(node.Neighbors.FindByValue("one"));

            node = node.Neighbors.FindByValue("three");
            Assert.AreEqual( "one" , node.Neighbors.FindByValue("one").Value);
            Assert.IsNull(node.Neighbors.FindByValue("three"));
            Assert.IsNull(node.Neighbors.FindByValue("two"));

        }

        // TODO The cost of each edge
        // TODO An edge for the same node 
    }
}