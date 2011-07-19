// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedBlackTree.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NLib.Collections.Generic.Extensions;
    using NLib.Collections.Generic.Resources;
    using NLib.Extensions;

    public interface IRedBlackTreeNode<T>
    {
        RedBlackTreeNode<T> Parent { get; }

        RedBlackTreeNode<T> Left { get; }

        RedBlackTreeNode<T> Right { get; }

        T Value { get; }

        bool IsRed { get; }

        bool IsBlack { get; }

        bool IsLeaf { get; }

        bool IsRoot { get; }
    }

    public class RedBlackTreeNode<T> : IRedBlackTreeNode<T>
    {
        protected RedBlackTreeNode()
        {
        }

        public RedBlackTreeNode(T value)
        {
            this.Value = value;
            this.IsRed = true;
        }

        public RedBlackTreeNode<T> Parent { get; set; }

        public RedBlackTreeNode<T> Left { get; set; }

        public RedBlackTreeNode<T> Right { get; set; }

        public T Value { get; protected set; }

        public bool IsRed { get; set; }

        public bool IsBlack
        {
            get { return !this.IsRed; }
            set { this.IsRed = !value; }
        }

        public bool IsLeaf
        {
            get { return this.Right == null && this.Left == null; }
        }

        public bool IsRoot
        {
            get { return this.Parent == null; }
        }
    }

    // http://en.wikipedia.org/wiki/Red-black_tree
    public class RedBlackTree<T> : ICollection<T>
    {
        private readonly Comparison<T> currentComparer;
        private readonly bool allowDuplicates;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        public RedBlackTree()
            : this(Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="allowDuplicates">If true, the tree will add duplicates; otherwise the duplicates won't be added.</param>
        public RedBlackTree(bool allowDuplicates)
            : this(Comparer<T>.Default, allowDuplicates)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public RedBlackTree(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="allowDuplicates">If true, the tree will add duplcates; otherwise the duplicates won't be added.</param>
        public RedBlackTree(IEnumerable<T> collection, bool allowDuplicates)
            : this(collection, Comparer<T>.Default, allowDuplicates)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public RedBlackTree(IComparer<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="allowDuplicates">If true, the tree will add duplicates; otherwise the duplicates won't be added.</param>
        public RedBlackTree(IComparer<T> comparer, bool allowDuplicates)
            : this(null, comparer, allowDuplicates)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public RedBlackTree(Comparison<T> comparer)
            : this(null, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="allowDuplicates">If true, the tree will add duplicates; otherwise the duplicates won't be added.</param>
        public RedBlackTree(Comparison<T> comparer, bool allowDuplicates)
            : this(null, comparer, allowDuplicates)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public RedBlackTree(IEnumerable<T> collection, IComparer<T> comparer)
            : this(collection, null, comparer, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="allowDuplicates">If true, the tree will add duplicates; otherwise the duplicates won't be added.</param>
        public RedBlackTree(IEnumerable<T> collection, IComparer<T> comparer, bool allowDuplicates)
            : this(collection, null, comparer, allowDuplicates)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        public RedBlackTree(IEnumerable<T> collection, Comparison<T> comparer)
            : this(collection, comparer, null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="allowDuplicates">If true, the tree will add duplicates; otherwise the duplicates won't be added.</param>
        public RedBlackTree(IEnumerable<T> collection, Comparison<T> comparer, bool allowDuplicates)
            : this(collection, comparer, null, allowDuplicates)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTree{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="comparison">The comparer. If null <paramref name="comparer"/> will be use.</param>
        /// <param name="comparer">The comparer. If null <paramref name="comparison"/> will be use.</param>
        /// <param name="allowDuplicates">If true, the tree will add duplicates; otherwise the duplicates won't be added.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> and <paramref name="comparison"/> are null.</exception>
        protected RedBlackTree(IEnumerable<T> collection, Comparison<T> comparison, IComparer<T> comparer, bool allowDuplicates)
        {
            Check.Requires<ArgumentNullException>(comparer != null || comparison != null, CollectionResource.Initialize_ArgumentNullException_ComparerAndComparison);

            this.currentComparer = comparison ?? comparer.Compare;
            this.allowDuplicates = allowDuplicates;
            this.AddRange(collection);
        }

        /// <summary>
        /// Gets a value indicating whether allow duplicates is enabled or not.
        /// </summary>
        /// <value>
        /// If true, the tree will add duplicates; otherwise the duplicates won't be added.
        /// </value>
        public bool AllowDuplicates
        {
            get { return this.allowDuplicates; }
        }

        /// <summary>
        /// Gets or sets the number of elements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        public int Count { get; protected set; }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        public T MaxValue
        {
            get
            {
                var node = this.MaxNode(this.Root);

                if (node == null)
                {
                    throw new NotSupportedException(CollectionResource.MaxValue_CollectionEmpty);
                }

                return node.Value;
            }
        }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public T MinValue
        {
            get
            {
                var node = this.MaxNode(this.Root);

                if (node == null)
                {
                    throw new NotSupportedException(CollectionResource.MinValue_CollectionEmpty);
                }

                return this.MinNode(this.Root).Value;
            }
        }

        /// <summary>
        /// Gets the root node.
        /// </summary>
        public IRedBlackTreeNode<T> RootNode
        {
            get { return this.Root; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="ICollection{T}"/> is read-only; otherwise, false.</returns>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the root.
        /// </summary>
        protected RedBlackTreeNode<T> Root { get; set; }

        /// <summary>
        /// Gets the comparer.
        /// </summary>
        protected Comparison<T> Comparer
        {
            get { return this.currentComparer; }
        }

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        /// <exception cref="System.NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public void Add(T item)
        {
            var node = new RedBlackTreeNode<T>(item);

            if (this.Root == null)
            {
                this.InsertCase1(node);
            }
            else
            {
                var root = this.Root;

                while (true)
                {
                    var c = this.Comparer(node.Value, root.Value);

                    if (!this.AllowDuplicates && c == 0)
                    {
                        return;
                    }

                    if (c < 0)
                    {
                        if (root.Left != null)
                        {
                            root = root.Left;
                        }
                        else
                        {
                            root.Left = node;
                            node.Parent = root;

                            break;
                        }
                    }
                    else
                    {
                        if (root.Right != null)
                        {
                            root = root.Right;
                        }
                        else
                        {
                            root.Right = node;
                            node.Parent = root;

                            break;
                        }
                    }
                }

                this.InsertCase1(node);
            }

            this.Count++;
        }

        /// <summary>
        /// Adds the elements of the specified collection in the tree.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public virtual void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                collection.ForEach(this.Add);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only. </exception>
        public void Clear()
        {
            this.Root = null;
            this.Count = 0;
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if item is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            RedBlackTreeNode<T> node;

            return this.Contains(item, out node);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{T}"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="array"/> is multidimensional.
        ///     -or-
        ///     <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        ///     -or-
        ///     The number of elements in the source <see cref="ICollection{T}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        ///     -or-
        ///     Type <paramref name="array"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Check.ArgumentNullException(array, "array");
            Check.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, CollectionResource.CopyTo_ArgumentOutOfRangeException_ArrayIndex, new { paramName = "arrayIndex" });
            Check.Requires<ArgumentException>(arrayIndex < array.Length && arrayIndex + this.Count <= array.Length, CollectionResource.CopyTo_ArgumentException_Array, new { paramName = "array" });

            if (this.Count > 0)
            {
                this.ForEach(i => array[arrayIndex++] = i);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.InOrderTraversal().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerable<T> InOrderTraversal()
        {
            return this.InOrderTraversal(this.Root).Select(node => node.Value);
        }

        public IEnumerable<T> LevelOrderTraversal()
        {
            return this.LevelOrderTraversal(this.Root).Select(node => node.Value);
        }

        public IEnumerable<T> PostOrderTraversal()
        {
            return this.PostOrderTraversal(this.Root).Select(node => node.Value);
        }

        public IEnumerable<T> PreOrderTraversal()
        {
            return this.PreOrderTraversal(this.Root).Select(node => node.Value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public bool Remove(T item)
        {
            RedBlackTreeNode<T> node;

            if (this.Contains(item, out node))
            {
                this.DeleteOneChild(node);
                this.Count--;
                return true;
            }

            return false;
        }

        protected bool Contains(T item, out RedBlackTreeNode<T> nodeFound)
        {
            var node = this.Root;

            while (node != null)
            {
                var c = this.Comparer(item, node.Value);

                if (c < 0)
                {
                    node = node.Left;
                }
                else if (c > 0)
                {
                    node = node.Right;
                }
                else
                {
                    nodeFound = node;
                    return true;
                }
            }

            nodeFound = null;

            return false;
        }

        protected void DeleteOneChild(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> successor = null;
            if (node.Left != null || node.Right != null)
            {
                successor = this.GetSuccessorOrPredecessor(node);

                this.ReplaceNode(node, successor);

                if (this.Root == node)
                {
                    this.Root = successor;
                }
            }

            if (node.IsBlack)
            {
                if (successor != null && successor.IsRed)
                {
                    successor.IsBlack = true;
                }
                else
                {
                    this.DeleteCase1(node);
                }
            }

            if (node.Parent.Left == node)
            {
                if (node.Left != null)
                {
                    node.Parent.Left = node.Left;
                    node.Left.Parent = node.Parent;
                }
                else if (node.Right != null)
                {
                    node.Parent.Left = node.Right;
                    node.Right.Parent = node.Parent;
                }
                else
                {
                    node.Parent.Left = null;
                }
            }
            else
            {
                if (node.Left != null)
                {
                    node.Parent.Right = node.Left;
                    node.Left.Parent = node.Parent;
                }
                else if (node.Right != null)
                {
                    node.Parent.Right = node.Right;
                    node.Right.Parent = node.Parent;
                }
                else
                {
                    node.Parent.Right = null;
                }
            }
        }

        protected RedBlackTreeNode<T> GetSuccessorOrPredecessor(RedBlackTreeNode<T> node)
        {
            var successor = this.GetSuccessor(node);
            if (successor == null)
            {
                successor = this.GetPredecessor(node);
            }
            else
            {
                if (!successor.IsRed && (!successor.IsRed || !successor.IsLeaf))
                {
                    var predecessor = this.GetPredecessor(node);

                    if (predecessor != null)
                    {
                        successor = predecessor;
                    }
                }
            }

            return successor;
        }

        protected RedBlackTreeNode<T> GetSuccessor(RedBlackTreeNode<T> node)
        {
            if (node.Right != null)
            {
                return this.MinNode(node.Right);
            }

            return null;
        }

        protected RedBlackTreeNode<T> GetPredecessor(RedBlackTreeNode<T> node)
        {
            if (node.Left != null)
            {
                return this.MaxNode(node.Left);
            }

            return null;
        }

        protected IEnumerable<RedBlackTreeNode<T>> InOrderTraversal(RedBlackTreeNode<T> node)
        {
            if (node != null)
            {
                node = this.MinNode(node);
                do
                {
                    yield return node;

                    if (node.Right != null)
                    {
                        node = node.Right;

                        node = this.MinNode(node);
                    }
                    else
                    {
                        // TODO le root est le node en parametre et non le root du tree
                        while (!node.IsRoot && node == node.Parent.Right)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
                while (node != null);
            }
        }

        protected IEnumerable<RedBlackTreeNode<T>> LevelOrderTraversal(RedBlackTreeNode<T> node)
        {
            throw new NotImplementedException();
        }

        protected RedBlackTreeNode<T> MaxNode(RedBlackTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }

            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }

        protected RedBlackTreeNode<T> MinNode(RedBlackTreeNode<T> node)
        {
            if (node == null)
            {
                return null;
            }

            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        protected IEnumerable<RedBlackTreeNode<T>> PostOrderTraversal(RedBlackTreeNode<T> node)
        {
            if (node != null)
            {
                node = this.MaxNode(node);
                do
                {
                    yield return node;

                    if (node.Left != null)
                    {
                        node = node.Left;

                        node = this.MaxNode(node);
                    }
                    else
                    {
                        // TODO le root est le node en parametre et non le root du tree
                        while (!node.IsRoot && node == node.Parent.Left)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
                while (node != null);
            }
        }

        protected IEnumerable<RedBlackTreeNode<T>> PreOrderTraversal(RedBlackTreeNode<T> node)
        {
            while (node != null)
            {
                yield return node;

                if (node.Left != null)
                {
                    node = node.Left;
                }
                else if (node.Right != null)
                {
                    node = node.Right;
                }
                else
                {
                    // TODO le root est le node en parametre et non le root du tree
                    while (!node.IsRoot && (node == node.Parent.Right || node.Parent.Right == null))
                    {
                        node = node.Parent;
                    }

                    node = node.IsRoot ? null : node.Parent.Right;
                }
            }
        }

        protected void ReplaceNode(RedBlackTreeNode<T> oldNode, RedBlackTreeNode<T> newNode)
        {
            var newNodeParent = newNode.Parent;
            var newNodeLeft = newNode.Left;
            var newNodeRight = newNode.Right;

            var oldNodeParent = oldNode.Parent;
            var oldNodeLeft = oldNode.Left;
            var oldNodeRight = oldNode.Right;

            newNode.Parent = newNode == oldNodeParent ? oldNode : oldNodeParent;
            if (oldNodeParent != null)
            {
                if (oldNodeParent.Left == oldNode)
                {
                    oldNodeParent.Left = newNode;
                }
                else
                {
                    oldNodeParent.Right = newNode;
                }
            }
            oldNode.Parent = oldNode == newNodeParent ? newNode : newNodeParent;
            if (newNodeParent != null)
            {
                if (newNodeParent.Left == newNode)
                {
                    newNodeParent.Left = oldNode;
                }
                else
                {
                    newNodeParent.Right = oldNode;
                }
            }

            newNode.Left = newNode == oldNodeLeft ? oldNode : oldNodeLeft;
            if (newNode != oldNodeLeft && oldNodeLeft != null)
            {
                oldNodeLeft.Parent = newNode;
            }
            oldNode.Left = oldNode == newNodeLeft ? newNode : newNodeLeft;
            if (oldNode != newNodeLeft && newNodeLeft != null)
            {
                newNodeLeft.Parent = oldNodeLeft;
            }

            newNode.Right = newNode == oldNodeRight ? oldNode : oldNodeRight;
            if (newNode != oldNodeRight && oldNodeRight != null)
            {
                oldNodeRight.Parent = newNode;
            }
            oldNode.Right = oldNode == newNodeRight ? newNode : newNodeRight;
            if (oldNode != newNodeRight && newNodeRight != null)
            {
                newNodeRight.Parent = oldNodeRight;
            }

            //var newNodeParent = newNode.Parent;
            //newNode.Parent.Right = oldNode;
            //newNode.Parent.Right = oldNode;
            //newNode.Parent = oldNode.Parent;

            //var newNodeLeft = newNode.Left;
            //newNode.Left = oldNode.Left;
            //if (oldNode.Left != null)
            //{
            //    oldNode.Left.Parent = newNode;
            //}
            //oldNode.Left = null;

            //var newNodeRight = newNode.Right;
            //newNode.Right = oldNode.Right;
            //if (oldNode.Right != null)
            //{
            //    oldNode.Right.Parent = newNode;
            //}
            //oldNode.Right = null;

            //if (oldNode.Parent != null)
            //{
            //    if (oldNode.Parent.Left == oldNode)
            //    {
            //        oldNode.Parent.Left = newNode;
            //    }
            //    else
            //    {
            //        oldNode.Parent.Right = newNode;
            //    }
            //}

            //oldNode.Parent = newNodeParent;
            //oldNode.Left = newNodeLeft;
            //oldNode.Right = newNodeRight;
        }

        protected RedBlackTreeNode<T> Sibling(RedBlackTreeNode<T> node)
        {
            if (node.IsRoot)
            {
                return null;
            }

            if (node == node.Parent.Left)
            {
                return node.Parent.Right;
            }

            return node.Parent.Left;
        }

        protected void RotateLeft(RedBlackTreeNode<T> node)
        {
            var parent = node.Parent;
            var right = node.Right;

            if (node == this.Root)
            {
                this.Root = right;
            }
            else
            {
                if (parent.Left == node)
                {
                    parent.Left = right;
                }
                else
                {
                    parent.Right = right;
                }
            }

            right.Parent = parent;
            node.Parent = right;

            node.Right = right.Left;
            if (node.Right != null)
            {
                node.Right.Parent = node;
            }

            right.Left = node;
        }

        protected void RotateRight(RedBlackTreeNode<T> node)
        {
            var parent = node.Parent;
            var left = node.Left;

            if (node.IsRoot)
            {
                this.Root = left;
            }
            else
            {
                if (parent.Left == node)
                {
                    parent.Left = left;
                }
                else
                {
                    parent.Right = left;
                }
            }

            left.Parent = parent;
            node.Parent = left;

            node.Left = left.Right;
            if (node.Left != null)
            {
                node.Left.Parent = node;
            }

            left.Right = node;
        }

        protected RedBlackTreeNode<T> GrandParent(RedBlackTreeNode<T> node)
        {
            if (node != null && node.Parent != null)
            {
                return node.Parent.Parent;
            }

            return null;
        }

        protected RedBlackTreeNode<T> Uncle(RedBlackTreeNode<T> node)
        {
            var grandparent = this.GrandParent(node);

            if (grandparent == null)
            {
                return null;
            }

            if (node.Parent == grandparent.Left)
            {
                return grandparent.Right;
            }

            return grandparent.Left;
        }

        private void DeleteCase1(RedBlackTreeNode<T> node)
        {
            if (node.Parent != null)
            {
                this.DeleteCase2(node);
            }
        }

        private void DeleteCase2(RedBlackTreeNode<T> node)
        {
            var sibling = this.Sibling(node);

            if (sibling.IsRed)
            {
                node.Parent.IsRed = true;
                sibling.IsBlack = true;

                if (node == node.Parent.Left)
                {
                    this.RotateLeft(node.Parent);
                }
                else
                {
                    this.RotateRight(node.Parent);
                }
            }

            this.DeleteCase3(node);
        }

        private void DeleteCase3(RedBlackTreeNode<T> node)
        {
            var sibling = this.Sibling(node);

            if (node.Parent.IsBlack && sibling.IsBlack && (sibling.Left == null || sibling.Left.IsBlack) && (sibling.Right == null || sibling.Right.IsBlack))
            {
                sibling.IsRed = true;
                this.DeleteCase1(node.Parent);
            }
            else
            {
                this.DeleteCase4(node);
            }
        }

        private void DeleteCase4(RedBlackTreeNode<T> node)
        {
            var sibling = this.Sibling(node);

            if (node.Parent.IsRed && sibling.IsBlack && (sibling.Left == null || sibling.Left.IsBlack) && (sibling.Right == null || sibling.Right.IsBlack))
            {
                sibling.IsRed = true;
                node.Parent.IsBlack = true;
            }
            else
            {
                this.DeleteCase5(node);
            }
        }

        private void DeleteCase5(RedBlackTreeNode<T> node)
        {
            var sibling = this.Sibling(node);

            if (sibling.IsBlack)
            {
                if (node == node.Parent.Left && (sibling.Right == null || sibling.Right.IsBlack) && sibling.Left.IsRed)
                {
                    sibling.IsRed = true;
                    sibling.Left.IsBlack = true;
                    this.RotateRight(sibling);
                }
                else if (node == node.Parent.Right && (sibling.Left == null || sibling.Left.IsBlack) && sibling.Right.IsRed)
                {
                    sibling.IsRed = true;
                    sibling.Right.IsBlack = true;
                    this.RotateLeft(sibling);
                }
            }

            this.DeleteCase6(node);
        }

        private void DeleteCase6(RedBlackTreeNode<T> node)
        {
            var sibling = this.Sibling(node);

            sibling.IsRed = node.Parent.IsRed;
            node.Parent.IsBlack = true;

            if (node == node.Parent.Left)
            {
                sibling.Right.IsBlack = true;
                this.RotateLeft(node.Parent);
            }
            else
            {
                sibling.Left.IsBlack = true;
                this.RotateRight(node.Parent);
            }
        }

        private void InsertCase1(RedBlackTreeNode<T> node)
        {
            if (node.Parent == null)
            {
                this.Root = node;
                node.IsBlack = true;
            }
            else
            {
                this.InsertCase2(node);
            }
        }

        private void InsertCase2(RedBlackTreeNode<T> node)
        {
            if (node.Parent.IsRed)
            {
                this.InsertCase3(node);
            }
        }

        private void InsertCase3(RedBlackTreeNode<T> node)
        {
            var uncle = this.Uncle(node);

            if (uncle != null && uncle.IsRed)
            {
                node.Parent.IsBlack = true;
                uncle.IsBlack = true;
                var grandparent = this.GrandParent(node);
                grandparent.IsRed = true;
                this.InsertCase1(grandparent);
            }
            else
            {
                this.InsertCase4(node);
            }
        }

        private void InsertCase4(RedBlackTreeNode<T> node)
        {
            var grandparent = this.GrandParent(node);

            if (node == node.Parent.Right && node.Parent == grandparent.Left)
            {
                this.RotateLeft(node.Parent);
                node = node.Left;
            }
            else if (node == node.Parent.Left && node.Parent == grandparent.Right)
            {
                this.RotateRight(node.Parent);
                node = node.Right;
            }

            this.InsertCase5(node);
        }

        private void InsertCase5(RedBlackTreeNode<T> node)
        {
            var grandparent = this.GrandParent(node);
            node.Parent.IsBlack = true;
            grandparent.IsRed = true;

            if (node == node.Parent.Left && node.Parent == grandparent.Left)
            {
                this.RotateRight(grandparent);
            }
            else
            {
                this.RotateLeft(grandparent);
            }
        }
    }
}
