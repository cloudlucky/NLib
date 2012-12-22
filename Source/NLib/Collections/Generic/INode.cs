// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INode.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    using System.Collections.Generic;

    public interface INode<T>
    {
        /// <summary>
        /// Gets the neighbors.
        /// </summary>
        IEnumerable<INode<T>> Neighbors { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        T Value { get; }
    }
}
