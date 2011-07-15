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
            Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
            Assert.IsNull(node.Neighbors.FindByValue("three"));
            Assert.IsNull(node.Neighbors.FindByValue("two"));

        }

        [Test]
        public void GenerateTest5()
        {
            var graph = new Graph<string>();
            graph.AddNode("one");
            graph.AddNode("two");
            graph.AddNode("three");

            graph.AddDirectedEdge("one", "two", 1);
            graph.AddDirectedEdge("two", "three", 2);
            graph.AddUndirectedEdge("three", "one", 3);

            GraphNode<string> node = graph.Nodes.FindByValue("one");

            Assert.AreEqual("two", node.Neighbors.FindByValue("two").Value);
            Assert.AreEqual(1, node.Costs[0]);

            node = node.Neighbors.FindByValue("two");
            Assert.AreEqual("three", node.Neighbors.FindByValue("three").Value);
            Assert.AreEqual(2, node.Costs[0]);

            node = node.Neighbors.FindByValue("three");
            Assert.AreEqual("one", node.Neighbors.FindByValue("one").Value);
            Assert.AreEqual(3, node.Costs[0]);
      
        }

        [Test]
        public void GenerateTest6()
        {
           var graph = new Graph<string>();
           graph.AddNode("one");
           graph.AddDirectedEdge("one", "one", 1);
           GraphNode<string> node;

           for (int i = 0; i < 10; i++)
           {
               node = graph.Nodes.ElementAt(0);
               Assert.AreEqual("one", node.Neighbors.ElementAt(0).Value);
           }

        }
       
    }
}