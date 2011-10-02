// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGraphEdge.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    /// <summary>
    /// Provides the base interface for the abstraction of a graph edge.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <typeparam name="TCost">The type of the cost.</typeparam>
    public interface IGraphEdge<T, TCost>
    {
        /// <summary>
        /// Gets the from node.
        /// </summary>
        IGraphNode<T, TCost> From { get; }

        /// <summary>
        /// Gets the to node.
        /// </summary>
        IGraphNode<T, TCost> To { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        TCost Value { get; }
    }
}