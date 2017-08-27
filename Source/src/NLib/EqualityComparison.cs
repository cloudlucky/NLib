using System.Diagnostics.CodeAnalysis;

namespace NLib
{
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
