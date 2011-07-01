// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IListExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Collections.Generic.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NLib.Collections.Generic.Extensions.Resources;

    /// <summary>
    /// Defines extensions methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class IListExtension
    {
        /// <summary>
        /// Swaps values at <paramref name="index1"/> and <paramref name="index2"/> in the specified <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="list"/></typeparam>
        /// <param name="list">The list to swap de the values.</param>
        /// <param name="index1">The index1.</param>
        /// <param name="index2">The index2.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public static void SwapValues<T>(this IList<T> list, int index1, int index2)
        {
            Check.ArgumentNullException(list, "list");
            Check.Requires<IndexOutOfRangeException>(index1 >= 0 && index1 < list.Count, IListExtensionResource.SwapValues_IndexOutOfRangeException_Index1);
            Check.Requires<IndexOutOfRangeException>(index2 >= 0 && index2 < list.Count, IListExtensionResource.SwapValues_IndexOutOfRangeException_Index2);

            var o = list[index1];
            list[index1] = list[index2];
            list[index2] = o;
        }
    }
}
