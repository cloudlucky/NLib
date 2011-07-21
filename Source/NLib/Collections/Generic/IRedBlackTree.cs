// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRedBlackTree.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    /// <summary>
    /// Provides the base interface for the abstraction of red-black trees.
    /// </summary>
    /// <typeparam name="T">The type of elements in the tree.</typeparam>
    public interface IRedBlackTree<T> : IBinaryTree<T>
    {
        /// <summary>
        /// Gets the root.
        /// </summary>
        new IRedBlackTreeNode<T> RootNode { get; }
    }
}
