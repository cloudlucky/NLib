namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Linq;

    using NLib.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class RedBlackTreeTest
    {
        [Test]
        public void AddRange()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEqual(array.OrderBy(x => x).ToArray(), tree.InOrderTraversal().ToArray());

            var root = tree.RootNode;

            // Left part of the tree
            this.AssertNode(8, true, false, root.Left);
            this.AssertNode(1, false, false, root.Left.Left);
            this.AssertNode(6, true, true, root.Left.Left.Right);
            this.AssertNode(11, false, false, root.Left.Right);
            this.AssertNode(9, true, true, root.Left.Right.Left);

            this.AssertNode(13, false, false, root);

            // Right part of the tree
            this.AssertNode(17, true, false, root.Right);
            this.AssertNode(15, false, true, root.Right.Left);
            this.AssertNode(25, false, false, root.Right.Right);
            this.AssertNode(22, true, true, root.Right.Right.Left);
            this.AssertNode(27, true, true, root.Right.Right.Right);
        }

        [Test]
        public void AddRange2()
        {
            var tree = new RedBlackTree<int>();
            tree.AddRange(Generator.Generate<int>(1000, x => ++x));

            Assert.AreEqual(1000, tree.Count);
        }

        [Test]
        public void AddRange3()
        {
            var array = new[] { 100, 50, 25, 200, 300, 250, 299, 75, 98, 68, 236, 358, 402, 506, 89, 874, 258, 321, 12, 123, 1236, 987, 45, 852, 147, 369, 951, 159, 23, 58, 69 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEqual(array.OrderBy(x => x).ToArray(), tree.InOrderTraversal().ToArray());
        }

        [Test]
        public void Clear()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);
            tree.Clear();

            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.RootNode);
        }

        [Test]
        public void CopyTo()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            var output = new int[array.Length];
            tree.CopyTo(output, 0);

            CollectionAssert.AreEqual(array.OrderBy(x => x).ToArray(), output);
        }

        [Test]
        public void Count()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.AreEqual(array.Length, tree.Count);
        }

        [Test]
        public void MaxValue()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.AreEqual(27, tree.MaxValue);
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException), ExpectedMessage = "The maximum value cannot be determined.")]
        public void MaxValueEmptyTree()
        {
            var tree = new RedBlackTree<int>();
            var max = tree.MaxValue;
            Assert.Fail();
        }

        [Test]
        public void MinValue()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.AreEqual(1, tree.MinValue);
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException), ExpectedMessage = "The minimum value cannot be determined.")]
        public void MinValueEmptyTree()
        {
            var tree = new RedBlackTree<int>();
            var min = tree.MinValue;
            Assert.Fail();
        }

        [Test]
        public void InOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEqual(array.OrderBy(x => x).ToArray(), tree.InOrderTraversal().ToArray());
        }

        [Test]
        public void LevelOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);
            CollectionAssert.AreEqual(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 9, 22, 27 }, tree.LevelOrderTraversal().ToArray());
        }

        [Test]
        public void PostOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEqual(array.OrderByDescending(x => x).ToArray(), tree.PostOrderTraversal().ToArray());
        }

        [Test]
        public void PreOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEqual(new[] { 13, 8, 1, 6, 11, 9, 17, 15, 25, 22, 27 }, tree.PreOrderTraversal().ToArray());
        }

        [Test]
        public void Remove()
        {
            foreach (var itemToRemove in new[] { 6, 22, 27, 1, 11, 9, 15, 25, 8, 17, 13 })
            {
                var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
                var tree = new RedBlackTree<int>();
                tree.AddRange(array);
                var removed = tree.Remove(itemToRemove);

                Assert.IsTrue(removed);
                CollectionAssert.AreEqual(array.Where(x => x != itemToRemove).OrderBy(x => x).ToArray(), tree.InOrderTraversal().ToArray());

                switch (itemToRemove)
                {
                    case 6:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, true, root.Left.Left);
                            this.AssertNode(11, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    case 22:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(11, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    case 27:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(11, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                        }

                        break;
                    case 1:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(6, false, true, root.Left.Left);
                            this.AssertNode(11, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    case 11:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(9, false, true, root.Left.Right);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    case 9:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(11, false, true, root.Left.Right);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    case 15:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(11, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(25, true, false, root.Right);
                            this.AssertNode(17, false, false, root.Right.Left);
                            this.AssertNode(22, true, true, root.Right.Left.Right);
                            this.AssertNode(27, false, true, root.Right.Right);
                        }

                        break;
                    case 25:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(11, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                            this.AssertNode(27, false, false, root.Right.Right);
                        }

                        break;
                    case 8:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(11, false, true, root.Left.Right);
                            this.AssertNode(9, true, false, root.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    case 17:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(8, true, false, root.Left);
                            this.AssertNode(1, false, false, root.Left.Left);
                            this.AssertNode(6, true, true, root.Left.Left.Right);
                            this.AssertNode(11, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Left);

                            this.AssertNode(13, false, false, root);

                            // Right part of the tree
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, false, root.Right);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    case 13:
                        {
                            var root = tree.RootNode;

                            // Left part of the tree
                            this.AssertNode(6, true, false, root.Left);
                            this.AssertNode(1, false, true, root.Left.Left);
                            this.AssertNode(8, false, false, root.Left.Right);
                            this.AssertNode(9, true, true, root.Left.Right.Right);

                            this.AssertNode(11, false, false, root);

                            // Right part of the tree
                            this.AssertNode(17, true, false, root.Right);
                            this.AssertNode(15, false, true, root.Right.Left);
                            this.AssertNode(25, false, false, root.Right.Right);
                            this.AssertNode(22, true, true, root.Right.Right.Left);
                            this.AssertNode(27, true, true, root.Right.Right.Right);
                        }

                        break;
                    default:
                        Assert.Fail();
                        break;
                }
            }
        }

        private void AssertNode<T>(T expectedValue, bool expectedIsRed, bool expectedIsLeaf, IRedBlackTreeNode<T> actualNode)
        {
            Assert.AreEqual(expectedValue, actualNode.Value);

            if (expectedIsRed)
            {
                Assert.IsFalse(actualNode.IsBlack);
                Assert.IsTrue(actualNode.IsRed);
            }
            else
            {
                Assert.IsTrue(actualNode.IsBlack);
                Assert.IsFalse(actualNode.IsRed);
            }

            if (expectedIsLeaf)
            {
                Assert.IsTrue(actualNode.IsLeaf);
            }
            else
            {
                Assert.IsFalse(actualNode.IsLeaf);
            }
        }
    }
}
