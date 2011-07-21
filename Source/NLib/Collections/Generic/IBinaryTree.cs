// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBinaryTree.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides the base interface for the abstraction of binary trees.
    /// </summary>
    /// <typeparam name="T">The type of elements in the tree.</typeparam>
    public interface IBinaryTree<T> : ICollection<T>
    {
        /// <summary>
        /// Gets a value indicating whether allow duplicates is enabled or not.
        /// </summary>
        /// <value>
        /// If true, the tree will add duplicates; otherwise the duplicates won't be added.
        /// </value>
        bool AllowDuplicates { get; }

        /// <summary>
        /// Gets the root.
        /// </summary>
        IBinaryTreeNode<T> RootNode { get; }

        /// <summary>
        /// Gets the maximum value of the <see cref="ICollection{T}"/>.
        /// </summary>
        T MaxValue { get; }

        /// <summary>
        /// Gets the minimum value of the <see cref="ICollection{T}"/>.
        /// </summary>
        T MinValue { get; }

        /// <summary>
        /// Adds the elements of the specified collection in the tree.
        /// </summary>
        /// <param name="collection">The collection.</param>
        void AddRange(IEnumerable<T> collection);

        /// <summary>
        /// Iterates throught a collection from minimum value to maximum value.
        /// </summary>
        /// <returns>Returns the collection from minimum value to maximum value.</returns>
        IEnumerable<T> InOrderTraversal();

        /// <summary>
        /// Iterates throught a collection by level.
        /// </summary>
        /// <returns>Returns the collection by level.</returns>
        IEnumerable<T> LevelOrderTraversal();

        /// <summary>
        /// Iterates throught a collection from maximum value to minimum value.
        /// </summary>
        /// <returns>Returns the collection from maximum value to minimum value.</returns>
        IEnumerable<T> PostOrderTraversal();

        /// <summary>
        /// Iterates throught a collection from root to leaves.
        /// </summary>
        /// <returns>Returns the collection from root to leaves.</returns>
        IEnumerable<T> PreOrderTraversal();
    }
}
