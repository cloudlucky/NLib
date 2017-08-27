using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using NLib.Collections.Generic.Extensions.Resources;

namespace NLib.Extensions
{
    /// <summary>
    /// Defines extensions methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class ArrayExtensions
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
            Check.Current.ArgumentNullException(array, nameof(array))
                         .Requires<IndexOutOfRangeException>(index1 >= 0 && index1 < array.Length, IListExtensionResource.SwapValues_IndexOutOfRangeException_Index1)
                         .Requires<IndexOutOfRangeException>(index2 >= 0 && index2 < array.Length, IListExtensionResource.SwapValues_IndexOutOfRangeException_Index2);

            var o = array[index1];
            array[index1] = array[index2];
            array[index2] = o;
        }
    }
}
