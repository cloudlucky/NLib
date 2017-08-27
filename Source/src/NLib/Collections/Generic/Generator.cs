namespace NLib.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NLib.Collections.Generic.Resources;

    /// <summary>
    /// Provide a set of methods to generate collection.
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// Generates a collection of <typeparamref name="T"/> with a specific number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="count">The number of elements.</param>
        /// <param name="func">The function that receive the last added element and return the next element to add.</param>
        /// <returns>A collection of <typeparamref name="T"/> generated with a specific number of elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="count"/> is less than zero.</exception>
        public static IEnumerable<T> Generate<T>(int count, Func<T, T> func)
        {
            return Generate(count, func, default(T));
        }

        /// <summary>
        /// Generates a collection of <typeparamref name="T"/> with a specific number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="count">The number of elements.</param>
        /// <param name="func">The function that receive the last added element and return the next element to add.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A collection of <typeparamref name="T"/> generated with a specific number of elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="count"/> is less than zero.</exception>
        public static IEnumerable<T> Generate<T>(int count, Func<T, T> func, T defaultValue)
        {
            return Generate(count, func, defaultValue, new Generator());
        }

        /// <summary>
        /// Generates a collection of <typeparamref name="T"/> with a specific number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="count">The number of elements.</param>
        /// <param name="func">The function that receive the last added element and return the next element to add.</param>
        /// <param name="provider">The provider to generate each element of the collection.</param>
        /// <returns>A collection of <typeparamref name="T"/> generated with a specific number of elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="provider"/> is null.</exception>
        public static IEnumerable<T> Generate<T>(int count, Func<T, T> func, Generator provider)
        {
            return Generate(count, func, default(T), provider);
        }

        /// <summary>
        /// Generates a collection of <typeparamref name="T"/> with a specific number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="count">The number of elements.</param>
        /// <param name="func">The function that receive the last added element and return the next element to add.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="provider">The provider to generate each element of the collection.</param>
        /// <returns>A collection of <typeparamref name="T"/> generated with a specific number of elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="count"/> is less than zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="provider"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2", Justification = "CheckError class do the check")]
        public static IEnumerable<T> Generate<T>(int count, Func<T, T> func, T defaultValue, Generator provider)
        {
            Check.Current.ArgumentNullException(provider, "provider");

            return provider.Generate(func, count, defaultValue);
        }

        /// <summary>
        /// Generates a collection of <typeparamref name="T"/> with a specific number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="func">The function that receive the last added element and return the next element to add.</param>
        /// <param name="count">The number of elements.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A collection of <typeparamref name="T"/> generated with a specific number of elements.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="count"/> is less than zero.</exception>
        public virtual IEnumerable<T> Generate<T>(Func<T, T> func, int count, T defaultValue)
        {
            Check.Current.ArgumentNullException(func, "func")
                         .Requires<ArgumentException>(count >= 0, GeneratorResource.Generate_ArgumentException_Count);

            var last = defaultValue;

            for (var i = 0; i < count; ++i)
            {
                last = func(last);
                yield return last;
            }
        }
    }
}
