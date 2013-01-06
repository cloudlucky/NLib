namespace NLib.Collections.Generic
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents a node in a collection.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface INode<out T>
    {
        /// <summary>
        /// Gets the neighbors.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        IEnumerable<INode<T>> Neighbors { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        T Value { get; }
    }
}
