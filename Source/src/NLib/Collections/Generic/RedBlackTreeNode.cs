namespace NLib.Collections.Generic
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <inheritdoc />
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

        /// <inheritdoc />
        public bool IsLeaf => this.Right == null && this.Left == null;

        /// <inheritdoc />
        public bool IsBlack
        {
            get => !this.IsRed;
            set => this.IsRed = !value;
        }

        /// <inheritdoc />
        public bool IsRoot => this.Parent == null;

        /// <inheritdoc />
        public bool IsRed { get; set; }

        /// <inheritdoc />
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Left => this.Left;

        /// <inheritdoc />
        IRedBlackTreeNode<T> IRedBlackTreeNode<T>.Left => this.Left;

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Reviewed. It's OK.")]
        IEnumerable<INode<T>> INode<T>.Neighbors
        {
            get
            {
                yield return this.Left;
                yield return this.Right;
            }
        }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public RedBlackTreeNode<T> Left { get; set; }

        /// <inheritdoc />
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Parent => this.Parent;

        /// <inheritdoc />
        IRedBlackTreeNode<T> IRedBlackTreeNode<T>.Parent => this.Parent;

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public RedBlackTreeNode<T> Parent { get; set; }

        /// <inheritdoc />
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Right => this.Right;

        /// <inheritdoc />
        IRedBlackTreeNode<T> IRedBlackTreeNode<T>.Right => this.Right;

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public RedBlackTreeNode<T> Right { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public T Value { get; protected set; }
    }
}
