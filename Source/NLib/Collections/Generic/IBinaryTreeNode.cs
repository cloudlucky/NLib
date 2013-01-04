namespace NLib.Collections.Generic
{
    /// <summary>
    /// Provides the base interface for the abstraction of binary tree's nodes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the node.</typeparam>
    public interface IBinaryTreeNode<T> : INode<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is leaf; has no child node.
        /// </summary>
        bool IsLeaf { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is root; has no parent node.
        /// </summary>
        bool IsRoot { get; }

        /// <summary>
        /// Gets the left.
        /// </summary>
        IBinaryTreeNode<T> Left { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        IBinaryTreeNode<T> Parent { get; }

        /// <summary>
        /// Gets the right.
        /// </summary>
        IBinaryTreeNode<T> Right { get; }
    }
}
