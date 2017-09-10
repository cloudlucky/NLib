using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NLib.Collections.Generic
{
    /// <inheritdoc />
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

        /// <inheritdoc />
        public bool IsLeaf => this.Right == null && this.Left == null;

        /// <inheritdoc />
        public bool IsRoot => this.Parent == null;

        /// <inheritdoc />
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Left => this.Left;

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public BinaryTreeNode<T> Left { get; set; }

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

        /// <inheritdoc />
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Parent => this.Parent;

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public BinaryTreeNode<T> Parent { get; set; }

        /// <inheritdoc />
        IBinaryTreeNode<T> IBinaryTreeNode<T>.Right => this.Right;

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public BinaryTreeNode<T> Right { get; set; }

        /// <inheritdoc />
        public T Value { get; protected set; }
    }
}