// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualityComparison.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <typeparam name="T"/> to compare.</param>
    /// <param name="y">The second object of type <typeparam name="T"/> to compare.</param>
    /// <returns>true if the specified objects are equal; otherwise, false.</returns>
    /// <typeparam name="T">The item type.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
    public delegate bool EqualityComparison<in T>(T x, T y);
}
