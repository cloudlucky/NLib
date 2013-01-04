namespace NLib.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic.Extensions;
    using NLib.Collections.Generic.Resources;

    /// <summary>
    /// Represents a strongly typed tree of objects. 
    /// Provides methods to search and manipulate the tree.
    /// </summary>
    /// <remarks>
    /// For more information about Red-black tree, see <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&oldid=437925585]]>.
    /// The null/empty leaves are not represented in this implementation. The children are null if there's any.
    /// </remarks>
    /// <typeparam name="T">The type of elements in the tree.</typeparam>
    public class RedBlackTree<T> : IRedBlackTree<T>
    {
        /// <summary>
        /// The comparison.
        /// </summary>
        private readonly Comparison<T> currentComparer;

        /// <summary>
        /// Indicates whether allow duplicates is enabled or not.
        /// </summary>
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
        /// <param name="allowDuplicates">If true, the tree will add duplicates; otherwise the duplicates won't be added.</param>
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
            Check.Current.Requires<ArgumentNullException>(comparer != null || comparison != null, CollectionResource.Initialize_ArgumentNullException_ComparerAndComparison);

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
        public virtual bool AllowDuplicates
        {
            get
            {
                return this.allowDuplicates;
            }
        }

        /// <summary>
        /// Gets or sets the number of elements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        public virtual int Count { get; protected set; }

        /// <summary>
        /// Gets the root.
        /// </summary>
        IBinaryTreeNode<T> IBinaryTree<T>.RootNode
        {
            get
            {
                return this.RootNode;
            }
        }

        /// <summary>
        /// Gets the root.
        /// </summary>
        public virtual IRedBlackTreeNode<T> RootNode
        {
            get
            {
                return this.Root;
            }
        }

        /// <summary>
        /// Gets the maximum value of the <see cref="ICollection{T}"/>.
        /// </summary>
        public virtual T MaxValue
        {
            get
            {
                if (this.Count == 0)
                {
                    throw new NotSupportedException(CollectionResource.MaxValue_CollectionEmpty);
                }

                return this.GetMaxNode(this.Root).Value;
            }
        }

        /// <summary>
        /// Gets the minimum value of the <see cref="ICollection{T}"/>.
        /// </summary>
        public virtual T MinValue
        {
            get
            {
                if (this.Count == 0)
                {
                    throw new NotSupportedException(CollectionResource.MinValue_CollectionEmpty);
                }

                return this.GetMinNode(this.Root).Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ICollection{T}"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="ICollection{T}"/> is read-only; otherwise, false.</returns>
        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the root.
        /// </summary>
        protected virtual RedBlackTreeNode<T> Root { get; set; }

        /// <summary>
        /// Gets the comparer.
        /// </summary>
        protected virtual Comparison<T> Comparer
        {
            get
            {
                return this.currentComparer;
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"/> is read-only.</exception>
        public virtual void Add(T item)
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
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection != null)
            {
                collection.ForEach(this.Add);
            }
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"></see> is read-only. </exception>
        public virtual void Clear()
        {
            this.Root = null;
            this.Count = 0;
        }

        /// <summary>
        /// Determines whether the <see cref="ICollection{T}"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ICollection{T}"></see>.</param>
        /// <returns>
        /// true if item is found in the <see cref="ICollection{T}"></see>; otherwise, false.
        /// </returns>
        public virtual bool Contains(T item)
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
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            Check.Current.ArgumentNullException(array, "array")
                         .Requires<ArgumentOutOfRangeException>(arrayIndex >= 0, CollectionResource.CopyTo_ArgumentOutOfRangeException_ArrayIndex, new { paramName = "arrayIndex" })
                         .Requires<ArgumentException>(arrayIndex < array.Length && arrayIndex + this.Count <= array.Length, CollectionResource.CopyTo_ArgumentException_Array, new { paramName = "array" });

            if (this.Count > 0)
            {
                this.ForEach(i => array[arrayIndex++] = i);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection in <see cref="RedBlackTree{T}.InOrderTraversal"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}"></see> that can be used to iterate through the collection.
        /// </returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.InOrderTraversal().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Iterates through a collection from minimum value to maximum value.
        /// </summary>
        /// <returns>Returns the collection from minimum value to maximum value.</returns>
        public virtual IEnumerable<T> InOrderTraversal()
        {
            return this.InOrderTraversal(this.Root).Select(node => node.Value);
        }

        /// <summary>
        /// Iterates through a collection by level.
        /// </summary>
        /// <returns>Returns the collection by level.</returns>
        public virtual IEnumerable<T> LevelOrderTraversal()
        {
            return this.LevelOrderTraversal(this.Root).Select(node => node.Value);
        }

        /// <summary>
        /// Iterates through a collection from maximum value to minimum value.
        /// </summary>
        /// <returns>Returns the collection from maximum value to minimum value.</returns>
        public virtual IEnumerable<T> PostOrderTraversal()
        {
            return this.PostOrderTraversal(this.Root).Select(node => node.Value);
        }

        /// <summary>
        /// Iterates through a collection from root to leaves.
        /// </summary>
        /// <returns>Returns the collection from root to leaves.</returns>
        public virtual IEnumerable<T> PreOrderTraversal()
        {
            return this.PreOrderTraversal(this.Root).Select(node => node.Value);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"></see>.</param>
        /// <returns>
        /// true if item was successfully removed from the <see cref="ICollection{T}"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="ICollection{T}"></see>.
        /// </returns>
        /// <exception cref="NotSupportedException">The <see cref="ICollection{T}"></see> is read-only.</exception>
        public virtual bool Remove(T item)
        {
            RedBlackTreeNode<T> node;

            if (this.Contains(item, out node))
            {
                this.DeleteOneNode(node);
                this.Count--;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the <see cref="ICollection{T}"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ICollection{T}"></see>.</param>
        /// <param name="nodeFound">Contains the node if found; otherwise is null.</param>
        /// <returns>
        /// true if item is found in the <see cref="ICollection{T}"></see>; otherwise, false.
        /// </returns>
        protected virtual bool Contains(T item, out RedBlackTreeNode<T> nodeFound)
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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The nodes to delete.</param>
        protected virtual void DeleteOneNode(RedBlackTreeNode<T> node)
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

            UnlinkDeletedNode(node);
        }

        /// <summary>
        /// Gets the grand parent.
        /// </summary>
        /// <param name="node">The node to get the grand parent.</param>
        /// <returns>The node's grand parent.</returns>
        protected virtual RedBlackTreeNode<T> GetGrandParent(RedBlackTreeNode<T> node)
        {
            if (node != null && node.Parent != null)
            {
                return node.Parent.Parent;
            }

            return null;
        }

        /// <summary>
        /// Gets the maximum node of the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="node">The node to start.</param>
        /// <returns>The maximum node.</returns>
        protected virtual RedBlackTreeNode<T> GetMaxNode(RedBlackTreeNode<T> node)
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

        /// <summary>
        /// Gets the minimum node of the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="node">The node to start.</param>
        /// <returns>The minimum node.</returns>
        protected virtual RedBlackTreeNode<T> GetMinNode(RedBlackTreeNode<T> node)
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

        /// <summary>
        /// Gets the predecessor.
        /// </summary>
        /// <param name="node">The node to gets the predecessor.</param>
        /// <returns>The predecessor of the <paramref name="node"/>.</returns>
        protected virtual RedBlackTreeNode<T> GetPredecessor(RedBlackTreeNode<T> node)
        {
            if (node.Left != null)
            {
                return this.GetMaxNode(node.Left);
            }

            return null;
        }

        /// <summary>
        /// Gets the successor.
        /// </summary>
        /// <param name="node">The node to gets the successor.</param>
        /// <returns>The successor of the <paramref name="node"/>.</returns>
        protected virtual RedBlackTreeNode<T> GetSuccessor(RedBlackTreeNode<T> node)
        {
            if (node.Right != null)
            {
                return this.GetMinNode(node.Right);
            }

            return null;
        }

        /// <summary>
        /// Gets the successor if exists; otherwise gets the predecessor.
        /// Gets the predecessor if the successor is a black leaf.
        /// </summary>
        /// <param name="node">The node to gets the successor or predecessor.</param>
        /// <returns>The successor or the predecessor of the <paramref name="node"/>.</returns>
        protected virtual RedBlackTreeNode<T> GetSuccessorOrPredecessor(RedBlackTreeNode<T> node)
        {
            var successor = this.GetSuccessor(node);
            if (successor == null)
            {
                successor = this.GetPredecessor(node);
            }
            else
            {
                if (!successor.IsRed || !successor.IsLeaf)
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

        /// <summary>
        /// Gets the sibling.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The node's sibling.</returns>
        protected virtual RedBlackTreeNode<T> GetSibling(RedBlackTreeNode<T> node)
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

        /// <summary>
        /// Gets the uncle.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The node's uncle.</returns>
        protected virtual RedBlackTreeNode<T> GetUncle(RedBlackTreeNode<T> node)
        {
            var grandparent = this.GetGrandParent(node);

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

        /// <summary>
        /// Iterates through a collection from minimum node to maximum node.
        /// </summary>
        /// <param name="node">The node to start the iteration.</param>
        /// <returns>Returns the collection from minimum node to maximum node.</returns>
        protected virtual IEnumerable<RedBlackTreeNode<T>> InOrderTraversal(RedBlackTreeNode<T> node)
        {
            if (node != null)
            {
                var root = node;
                node = this.GetMinNode(node);
                do
                {
                    yield return node;

                    if (node.Right != null)
                    {
                        node = node.Right;

                        node = this.GetMinNode(node);
                    }
                    else
                    {
                        while (node != root && node == node.Parent.Right)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
                while (node != null);
            }
        }

        /// <summary>
        /// Iterates through a collection by level.
        /// </summary>
        /// <param name="node">The node to start the iteration.</param>
        /// <returns>Returns the collection by level.</returns>
        protected virtual IEnumerable<RedBlackTreeNode<T>> LevelOrderTraversal(RedBlackTreeNode<T> node)
        {
            if (node != null)
            {
                var q = new Queue<RedBlackTreeNode<T>>(this.Count);

                q.Enqueue(node);

                while (q.Count > 0)
                {
                    var n = q.Dequeue();

                    yield return n;

                    if (n.Left != null)
                    {
                        q.Enqueue(n.Left);
                    }

                    if (n.Right != null)
                    {
                        q.Enqueue(n.Right);
                    }
                }
            }
        }

        /// <summary>
        /// Iterates through a collection from node value to minimum node.
        /// </summary>
        /// <param name="node">The node to start the iteration.</param>
        /// <returns>Returns the collection from node value to node value.</returns>
        protected virtual IEnumerable<RedBlackTreeNode<T>> PostOrderTraversal(RedBlackTreeNode<T> node)
        {
            if (node != null)
            {
                var root = node;
                node = this.GetMaxNode(node);

                do
                {
                    yield return node;

                    if (node.Left != null)
                    {
                        node = node.Left;

                        node = this.GetMaxNode(node);
                    }
                    else
                    {
                        while (node != root && node == node.Parent.Left)
                        {
                            node = node.Parent;
                        }

                        node = node.Parent;
                    }
                }
                while (node != null);
            }
        }

        /// <summary>
        /// Iterates through a collection from root to leaves.
        /// </summary>
        /// <param name="node">The node to start the iteration.</param>
        /// <returns>Returns the collection from root to leaves.</returns>
        protected virtual IEnumerable<RedBlackTreeNode<T>> PreOrderTraversal(RedBlackTreeNode<T> node)
        {
            var root = node;

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
                    while (node != root && (node == node.Parent.Right || node.Parent.Right == null))
                    {
                        node = node.Parent;
                    }

                    node = node.IsRoot 
                             ? null 
                             : node.Parent.Right;
                }
            }
        }

        /// <summary>
        /// Replaces the <paramref name="oldNode"/> by the <paramref name="newNode"/>.
        /// </summary>
        /// <param name="oldNode">The old node.</param>
        /// <param name="newNode">The new node.</param>
        protected virtual void ReplaceNode(RedBlackTreeNode<T> oldNode, RedBlackTreeNode<T> newNode)
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
        }

        /// <summary>
        /// Rotates the left.
        /// </summary>
        /// <param name="node">The node.</param>
        protected virtual void RotateLeft(RedBlackTreeNode<T> node)
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

        /// <summary>
        /// Rotates the right.
        /// </summary>
        /// <param name="node">The node.</param>
        protected virtual void RotateRight(RedBlackTreeNode<T> node)
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

        /// <summary>
        /// Unlinks the deleted node from its parent and children.
        /// </summary>
        /// <param name="node">The node.</param>
        private static void UnlinkDeletedNode(RedBlackTreeNode<T> node)
        {
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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to delete.</param>
        private void DeleteCase1(RedBlackTreeNode<T> node)
        {
            if (node.Parent != null)
            {
                this.DeleteCase2(node);
            }
        }

        /// <summary>
        /// Sibling node is red. 
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to delete.</param>
        private void DeleteCase2(RedBlackTreeNode<T> node)
        {
            var sibling = this.GetSibling(node);

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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code came from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>
        /// </remarks>
        /// <param name="node">The node to delete.</param>
        private void DeleteCase3(RedBlackTreeNode<T> node)
        {
            var sibling = this.GetSibling(node);

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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to delete.</param>
        private void DeleteCase4(RedBlackTreeNode<T> node)
        {
            var sibling = this.GetSibling(node);

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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to delete.</param>
        private void DeleteCase5(RedBlackTreeNode<T> node)
        {
            var sibling = this.GetSibling(node);

            if (sibling.IsBlack)
            {
                if (node == node.Parent.Left && (sibling.Right == null || sibling.Right.IsBlack) && sibling.Left.IsRed)
                {
                    sibling.IsRed = true;
                    sibling.Left.IsBlack = true;
                    this.RotateRight(sibling);
                }
                else if (sibling.Right != null)
                {
                    if (node == node.Parent.Right && (sibling.Left == null || sibling.Left.IsBlack) && sibling.Right.IsRed)
                    {
                        sibling.IsRed = true;
                        sibling.Right.IsBlack = true;
                        this.RotateLeft(sibling);
                    }
                }
            }

            this.DeleteCase6(node);
        }

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to delete.</param>
        private void DeleteCase6(RedBlackTreeNode<T> node)
        {
            var sibling = this.GetSibling(node);

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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to insert.</param>
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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to insert.</param>
        private void InsertCase2(RedBlackTreeNode<T> node)
        {
            if (node.Parent.IsRed)
            {
                this.InsertCase3(node);
            }
        }

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to insert.</param>
        private void InsertCase3(RedBlackTreeNode<T> node)
        {
            var uncle = this.GetUncle(node);

            if (uncle != null && uncle.IsRed)
            {
                node.Parent.IsBlack = true;
                uncle.IsBlack = true;
                var grandparent = this.GetGrandParent(node);
                grandparent.IsRed = true;
                this.InsertCase1(grandparent);
            }
            else
            {
                this.InsertCase4(node);
            }
        }

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to insert.</param>
        private void InsertCase4(RedBlackTreeNode<T> node)
        {
            var grandparent = this.GetGrandParent(node);

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

        /// <summary>
        /// See <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]> for more detail.
        /// </summary>
        /// <remarks>
        /// Documentation and the code come from <![CDATA[http://en.wikipedia.org/w/index.php?title=Red-black_tree&amp;oldid=437925585]]>.
        /// </remarks>
        /// <param name="node">The node to insert.</param>
        private void InsertCase5(RedBlackTreeNode<T> node)
        {
            var grandparent = this.GetGrandParent(node);
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