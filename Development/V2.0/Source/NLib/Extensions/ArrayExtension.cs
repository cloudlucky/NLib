// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NLib.Collections.Generic.Extensions.Resources;

    /// <summary>
    /// Defines extensions methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Swaps values at <paramref name="index1"/> and <paramref name="index2"/> in the specified <paramref name="array"/>.
        /// </summary>
        /// <typeparam name="T">The type of the <paramref name="array"/></typeparam>
        /// <param name="array">The array to swap de the values.</param>
        /// <param name="index1">The index1.</param>
        /// <param name="index2">The index2.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public static void SwapValues<T>(this T[] array, int index1, int index2)
        {
            CheckError.ArgumentNullException(array, "array");
            Check.Requires<IndexOutOfRangeException>(index1 >= 0 && index1 < array.Length, IListExtensionResource.SwapValues_IndexOutOfRangeException_Index1);
            Check.Requires<IndexOutOfRangeException>(index2 >= 0 && index2 < array.Length, IListExtensionResource.SwapValues_IndexOutOfRangeException_Index2);

            var o = array[index1];
            array[index1] = array[index2];
            array[index2] = o;
        }
    }
}
