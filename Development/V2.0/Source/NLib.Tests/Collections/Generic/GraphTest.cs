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

            graph = null;
        }

        [Test]
        public void GenerateTest2()
        {
            Graph<string> graph = new Graph<string>();
            graph.AddNode("one");
            graph.AddNode("two");
            

            Node<string> nodeOne = graph.Nodes.FindByValue("one");
            Node<string> nodeTwo = graph.Nodes.FindByValue("two");

            Assert.AreEqual("one", nodeOne.Value);
            Assert.AreEqual("two", nodeTwo.Value);

            graph.AddDirectedEdge("one", "two");

            IEnumerator<string> enumGraph = graph.GetEnumerator();

               while (enumGraph.MoveNext())
               {
                  Assert.IsTrue((enumGraph.Current.Contains("one") || enumGraph.Current.Contains("two")));          
               }

        }

    }
}