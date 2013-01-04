namespace NLib.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a strongly typed tree node. 
    /// </summary>
    /// <typeparam name="T">The type of elements in the node.</typeparam>
    public class BinaryTreeNode<T> : IBinaryTreeNode<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public BinaryTreeNode(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        protected BinaryTreeNode()
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
        /// Gets a value indicating whether this instance is root; has no parent node.
        /// </summary>
        public bool IsRoot
        {
            get { return this.Parent == null; }
        }

        /// <summary>
        /// Gets the left.
        /// </summary>
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Left
        {
            get { return this.Left; }
        }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public BinaryTreeNode<T> Left { get; set; }

        /// <summary>
        /// Gets the neighbors.
        /// </summary>
        IEnumerable<INode<T>> INode<T>.Neighbors
        {
            get
            {
                yield return this.Left;
                yield return this.Right;
            }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Parent
        {
            get { return this.Parent; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public BinaryTreeNode<T> Parent { get; set; }

        /// <summary>
        /// Gets the right.
        /// </summary>
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Right
        {
            get { return this.Right; }
        }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public BinaryTreeNode<T> Right { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; protected set; }
    }
}