namespace NLib.Patterns
{
    using System;

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
        public static T Current
        {
            get { return Instance.Value; }
        }
    }
}
