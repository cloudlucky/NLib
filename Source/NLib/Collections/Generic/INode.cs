namespace NLib.Collections.Generic
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a node in a collection.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface INode<out T>
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
