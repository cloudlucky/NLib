using System.Diagnostics.CodeAnalysis;

namespace NLib.Collections.Generic
{
    /// <inheritdoc />
    /// <summary>
    /// Provides the base interface for the abstraction of red-black trees.
    /// </summary>
    /// <typeparam name="T">The type of elements in the tree.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "No need to finish with Collection suffix")]
    public interface IRedBlackTree<T> : IBinaryTree<T>
    {
        /// <summary>
        /// Gets the root.
        /// </summary>
        new IRedBlackTreeNode<T> RootNode { get; }
    }
}
