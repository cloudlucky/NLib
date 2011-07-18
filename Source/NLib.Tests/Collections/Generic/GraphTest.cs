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
        public void GenerateTest0()
        {
            var graph = new Graph<string>(null);
            Assert.IsNotNull(graph.Nodes);

            var setNodes = new GraphNodeList<string>();
            var nodes = new GraphNode<string>("one");
            setNodes.Add(nodes);
            Assert.AreEqual("one", setNodes.FindByValue("one").Value);
            Assert.AreEqual(1, setNodes.Count());
            var graphNotNull = new Graph<string>(setNodes);
            Assert.IsNotNull(graphNotNull.Nodes);
            Assert.IsTrue(graphNotNull.Contains("one"));
            Assert.IsTrue(graphNotNull.Remove("one"));
            Assert.IsEmpty(graphNotNull.Nodes);

            graphNotNull.AddNode(new GraphNode<string>("two"));
            Assert.AreEqual(1, graphNotNull.Count());
            Assert.IsTrue(graphNotNull.Nodes.Contains(graphNotNull.Nodes.FindByValue("two")));
        }

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

            var enumNodes = graph.GetEnumerator();
            int nbNode = 0; 
            while (enumNodes.MoveNext() )
            {
                nbNode++;
            }
            Assert.AreEqual(6, nbNode);
            Assert.AreEqual(6, graph.Count());

            Assert.IsTrue(graph.Contains("one"));
            Assert.IsTrue(graph.Contains("two"));
            Assert.IsTrue(graph.Contains("three"));
            Assert.IsTrue(graph.Contains("four"));
            Assert.IsTrue(graph.Contains("five"));
            Assert.IsTrue(graph.Contains("six"));

            Assert.IsTrue(graph.Remove("three"));
            Assert.AreEqual(5, graph.Count());

            Assert.IsFalse(graph.Remove("seven"));

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

             graph.AddNode("three");
             graph.AddNode("four");
             graph.AddNode("five");
             graph.AddNode("six");

            var node1 = new GraphNode<string>("one");
            var node2 = new GraphNode<string>("two");
            
            graph.AddNode(node1);
            graph.AddNode(node2);

            graph.AddDirectedEdge(node1, node2);
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

            Assert.IsTrue(graph.Remove("three"));
      
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

           Assert.IsTrue(graph.Remove("one"));

        }

        [Test]
        public void GenerateTest7()
        {
            var node = new GraphNode<bool>();
            var nodes = new GraphNodeList<bool>();
          
        }

       
    }
}