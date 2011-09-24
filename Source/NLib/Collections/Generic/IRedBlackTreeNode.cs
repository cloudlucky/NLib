// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRedBlackTreeNode.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    /// <summary>
    /// Provides the base interface for the abstraction of red-black tree's nodes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the node.</typeparam>
    public interface IRedBlackTreeNode<T> : IBinaryTreeNode<T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is black.
        /// </summary>
        bool IsBlack { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is red.
        /// </summary>
        bool IsRed { get; }

        /// <summary>
        /// Gets the left.
        /// </summary>
        new IRedBlackTreeNode<T> Left { get; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        new IRedBlackTreeNode<T> Parent { get; }

        /// <summary>
        /// Gets the right.
        /// </summary>
        new IRedBlackTreeNode<T> Right { get; }
    }
}
