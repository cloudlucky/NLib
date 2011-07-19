namespace NLib.Tests.Collections.Generic
{
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

            CollectionAssert.AreEquivalent(array.OrderBy(x => x).ToArray(), tree.InOrderTraversal().ToArray());

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
        public void InOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEquivalent(array.OrderBy(x => x).ToArray(), tree.InOrderTraversal().ToArray());
        }

        [Test]
        [ExpectedException(typeof(System.NotImplementedException))]
        public void LevelOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEquivalent(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 9, 22, 27 }, tree.LevelOrderTraversal().ToArray());
        }

        [Test]
        public void PostOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEquivalent(array.OrderByDescending(x => x).ToArray(), tree.PostOrderTraversal().ToArray());
        }

        [Test]
        public void PreOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            CollectionAssert.AreEquivalent(new[] { 13, 8, 1, 6, 11, 9, 17, 15, 25, 22, 27 }, tree.PreOrderTraversal().ToArray());
        }

        [Test]
        [Sequential]
        public void Remove([Values(6, 22, 27, 1, 11, 9, 15, 25, 8, 17, 13)] int itemToRemove)
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);
            var removed = tree.Remove(itemToRemove);

            Assert.IsTrue(removed);
            CollectionAssert.AreEquivalent(array.Where(x => x != itemToRemove).OrderBy(x => x).ToArray(), tree.InOrderTraversal().ToArray());

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
