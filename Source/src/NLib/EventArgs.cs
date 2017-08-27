using System;

namespace NLib
{
    /// <inheritdoc />
    /// <summary>
    /// <see cref="T:NLib.EventArgs`1" /> is the base class for classes containing generic event data.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    [Serializable]
    public class EventArgs<T> : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.EventArgs`1" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public EventArgs(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }
    }
}
