// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tree.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic
{
    public class Tree<T> : TreeNode<T>
    {
    }

    public class TreeNode<T>
    {
        public T Value { get; set; }


    }
}
