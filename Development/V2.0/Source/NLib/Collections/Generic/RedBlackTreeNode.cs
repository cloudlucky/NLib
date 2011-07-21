// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedBlackTreeNode.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    /// <summary>
    /// Represents a strongly typed tree node. 
    /// </summary>
    /// <typeparam name="T">The type of elements in the node.</typeparam>
    public class RedBlackTreeNode<T> : IRedBlackTreeNode<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTreeNode{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public RedBlackTreeNode(T value)
        {
            this.Value = value;
            this.IsRed = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTreeNode{T}"/> class.
        /// </summary>
        protected RedBlackTreeNode()
        {
        }

        /// <summary>
        /// Gets a value indicating whether this instance is leaf; has no child node.
        /// </summary>
        public bool IsLeaf
        {
            get { return this.Right == null && this.Left == null; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is black.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is black; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlack
        {
            get { return !this.IsRed; }
            set { this.IsRed = !value; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is root; has no parent node.
        /// </summary>
        public bool IsRoot
        {
            get { return this.Parent == null; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is red.
        /// </summary>
        public bool IsRed { get; set; }

        /// <summary>
        /// Gets the left.
        /// </summary>
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Left
        {
            get { return this.Left; }
        }

        /// <summary>
        /// Gets the left.
        /// </summary>
        IRedBlackTreeNode<T> IRedBlackTreeNode<T>.Left
        {
            get { return this.Left; }
        }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public RedBlackTreeNode<T> Left { get; set; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Parent
        {
            get { return this.Parent; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        IRedBlackTreeNode<T> IRedBlackTreeNode<T>.Parent
        {
            get { return this.Parent; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public RedBlackTreeNode<T> Parent { get; set; }

        /// <summary>
        /// Gets the right.
        /// </summary>
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Right
        {
            get { return this.Right; }
        }

        /// <summary>
        /// Gets the right.
        /// </summary>
        IRedBlackTreeNode<T> IRedBlackTreeNode<T>.Right
        {
            get { return this.Right; }
        }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public RedBlackTreeNode<T> Right { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; protected set; }
    }
}
