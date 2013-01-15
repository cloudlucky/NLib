namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Linq;

    using NLib.Collections.Generic;

    using Xunit;

    public class RedBlackTreeTest
    {
        [Fact]
        public void AddRange()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(array.OrderBy(x => x).ToArray(), tree.InorderTraversal().ToArray());

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

        [Fact]
        public void AddRange2()
        {
            var tree = new RedBlackTree<int>();
            tree.AddRange(Generator.Generate<int>(1000, x => ++x));

            Assert.Equal(1000, tree.Count);
        }

        [Fact]
        public void AddRange3()
        {
            var array = new[] { 100, 50, 25, 200, 300, 250, 299, 75, 98, 68, 236, 358, 402, 506, 89, 874, 258, 321, 12, 123, 1236, 987, 45, 852, 147, 369, 951, 159, 23, 58, 69 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(array.OrderBy(x => x).ToArray(), tree.InorderTraversal().ToArray());
        }

        [Fact]
        public void Clear()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);
            tree.Clear();

            Assert.Equal(0, tree.Count);
            Assert.Null(tree.RootNode);
        }

        [Fact]
        public void CopyTo()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            var output = new int[array.Length];
            tree.CopyTo(output, 0);

            Assert.Equal(array.OrderBy(x => x).ToArray(), output);
        }

        [Fact]
        public void Count()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(array.Length, tree.Count);
        }

        [Fact]
        public void MaxValue()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(27, tree.MaxValue);
        }

        [Fact]
        public void MaxValueEmptyTree()
        {
            var tree = new RedBlackTree<int>();
            var ex = Assert.Throws<NotSupportedException>(() => tree.MaxValue);
            Assert.Equal("The maximum value cannot be determined.", ex.Message);
        }

        [Fact]
        public void MinValue()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(1, tree.MinValue);
        }

        [Fact]
        public void MinValueEmptyTree()
        {
            var tree = new RedBlackTree<int>();
            Assert.Throws<NotSupportedException>(() => tree.MinValue);
        }

        [Fact]
        public void InOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(array.OrderBy(x => x).ToArray(), tree.InorderTraversal().ToArray());
        }

        [Fact]
        public void LevelOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);
            Assert.Equal(new[] { 13, 8, 17, 1, 11, 15, 25, 6, 9, 22, 27 }, tree.LevelOrderTraversal().ToArray());
        }

        [Fact]
        public void PostOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(array.OrderByDescending(x => x).ToArray(), tree.PostorderTraversal().ToArray());
        }

        [Fact]
        public void PreOrder()
        {
            var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
            var tree = new RedBlackTree<int>();
            tree.AddRange(array);

            Assert.Equal(new[] { 13, 8, 1, 6, 11, 9, 17, 15, 25, 22, 27 }, tree.PreorderTraversal().ToArray());
        }

        [Fact]
        public void Remove()
        {
            foreach (var itemToRemove in new[] { 6, 22, 27, 1, 11, 9, 15, 25, 8, 17, 13 })
            {
                var array = new[] { 13, 8, 17, 1, 11, 9, 15, 25, 6, 22, 27 };
                var tree = new RedBlackTree<int>();
                tree.AddRange(array);
                var removed = tree.Remove(itemToRemove);

                Assert.True(removed);
                Assert.Equal(array.Where(x => x != itemToRemove).OrderBy(x => x).ToArray(), tree.InorderTraversal().ToArray());

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
                        Assert.True(false);
                        break;
                }
            }
        }

        private void AssertNode<T>(T expectedValue, bool expectedIsRed, bool expectedIsLeaf, IRedBlackTreeNode<T> actualNode)
        {
            Assert.Equal(expectedValue, actualNode.Value);

            if (expectedIsRed)
            {
                Assert.False(actualNode.IsBlack);
                Assert.True(actualNode.IsRed);
            }
            else
            {
                Assert.True(actualNode.IsBlack);
                Assert.False(actualNode.IsRed);
            }

            if (expectedIsLeaf)
            {
                Assert.True(actualNode.IsLeaf);
            }
            else
            {
                Assert.False(actualNode.IsLeaf);
            }
        }
    }
}
