using System;
using System.Diagnostics.CodeAnalysis;

namespace NLib.Patterns
{
    /// <summary>
    /// Create a unique instance of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to create a unique instance</typeparam>
    public static class LazySingleton<T> 
        where T : new()
    {
        /// <summary>
        /// Unique instance.
        /// </summary>
        private static readonly Lazy<T> Instance = new Lazy<T>(() => new T());

        /// <summary>
        /// Gets the current instance of <typeparam name="T"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Reviewd. It's OK.")]
        public static T Current => Instance.Value;
    }
}
