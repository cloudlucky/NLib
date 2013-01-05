namespace NLib
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq.Expressions;

    using NLib.Linq.Extensions;

    /// <summary>
    /// Defines extensions methods for <see cref="Check"/>.
    /// </summary>
    public static class CheckExtensions
    {
        /// <summary>
        /// Throws <see cref="System.ArgumentException"/> if the <paramref name="condition"/> is false.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentException(this Check check, bool condition, string paramName)
        {
            return ArgumentException(check, condition, paramName, string.Empty);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentException"/> if the <paramref name="condition"/> is false.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentException(this Check check, bool condition, string paramName, string message)
        {
            return Requires<ArgumentException>(check, condition, message, new { paramName });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentNullException(this Check check, object param, string paramName)
        {
            return ArgumentNullException(check, param, paramName, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="reference"/> is null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullException<T>(this Check check, Expression<Func<T?>> reference) where T : struct
        {
            return ArgumentNullException(check, reference, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="reference"/> is null.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullException(this Check check, Expression<Func<object>> reference)
        {
            return ArgumentNullException(check, reference, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentNullException(this Check check, object param, string paramName, string message)
        {
            return Requires<ArgumentNullException>(check, param != null, message, new { paramName });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException" /> if the <paramref name="reference" /> is null.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="check">The <see cref="Check" /> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check" /> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullException<T>(this Check check, Expression<Func<T?>> reference, string message) where T : struct
        {
            return Requires<ArgumentNullException>(check, reference.Compile()().HasValue, message, new { paramName = reference.GetParameterName() });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="reference"/> is null.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullException(this Check check, Expression<Func<object>> reference, string message)
        {
            return Requires<ArgumentNullException>(check, reference.Compile()() != null, message, new { paramName = reference.GetParameterName() });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null or empty.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentNullOrEmptyException(this Check check, string param, string paramName)
        {
            return ArgumentNullOrEmptyException(check, param, paramName, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="reference"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null or empty.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullOrEmptyException(this Check check, Expression<Func<string>> reference)
        {
            return ArgumentNullOrEmptyException(check, reference, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentNullOrEmptyException(this Check check, string param, string paramName, string message)
        {
            return Requires<ArgumentNullException>(check, !string.IsNullOrEmpty(param), message, new { paramName });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="reference"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullOrEmptyException(this Check check, Expression<Func<string>> reference, string message)
        {
            return Requires<ArgumentNullException>(check, !string.IsNullOrEmpty(reference.Compile()()), message, new { paramName = reference.GetParameterName() });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null or white space.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentNullOrWhiteSpaceException(this Check check, string param, string paramName)
        {
            return ArgumentNullOrWhiteSpaceException(check, param, paramName, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="reference"/> is null or <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null or white space.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullOrWhiteSpaceException(this Check check, Expression<Func<string>> reference)
        {
            return ArgumentNullOrWhiteSpaceException(check, reference, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or  <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null or white space.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check ArgumentNullOrWhiteSpaceException(this Check check, string param, string paramName, string message)
        {
            return Requires<ArgumentNullException>(check, !string.IsNullOrWhiteSpace(param), message, new { paramName });
        }

        /// <summary>
        /// Throws <see cref="System.NullReferenceException"/> if the <paramref name="reference"/> is null or  <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null or white space.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check ArgumentNullOrWhiteSpaceException(this Check check, Expression<Func<string>> reference, string message)
        {
            return Requires<ArgumentNullException>(check, !string.IsNullOrWhiteSpace(reference.Compile()()), message, new { paramName = reference.GetParameterName() });
        }

        /// <summary>
        /// Throws <see cref="System.NullReferenceException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check NotNull(this Check check, object param, string paramName)
        {
            return NotNull(check, param, paramName, null);
        }

        /// <summary>
        /// Throws <see cref="System.NullReferenceException"/> if the <paramref name="reference"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check NotNull(this Check check, Expression<Func<object>> reference)
        {
            return NotNull(check, reference, null);
        }

        /// <summary>
        /// Throws <see cref="System.NullReferenceException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="param">The parameter to check if it's null.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check NotNull(this Check check, object param, string paramName, string message)
        {
            return Requires<NullReferenceException>(check, param != null, string.Format(CultureInfo.CurrentCulture, "'{0}' is null. {1}", paramName, message));
        }

        /// <summary>
        /// Throws <see cref="System.NullReferenceException"/> if the <paramref name="reference"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="reference">The parameter to check if it's null.</param>
        /// <param name="message">The message.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Like Linq API")]
        public static Check NotNull(this Check check, Expression<Func<object>> reference, string message)
        {
            return Requires<NullReferenceException>(check, reference.Compile()() != null, message);
        }
        
        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, bool condition) where TException : Exception
        {
            return Requires<TException>(check, condition, string.Empty);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, Func<bool> condition) where TException : Exception
        {
            return Requires<TException>(check, condition, string.Empty);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="exception">The exception thrown if the <paramref name="condition"/> is false.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check Requires<TException>(this Check check, bool condition, TException exception) where TException : Exception
        {
            if (!condition)
            {
                throw exception;
            }

            return check;
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="exception">The exception thrown if the <paramref name="condition"/> is false.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        public static Check Requires<TException>(this Check check, Func<bool> condition, TException exception) where TException : Exception
        {
            return Requires(check, condition(), exception);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="message"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="message">The message to display if the <paramref name="condition"/> is false.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, bool condition, string message) where TException : Exception
        {
            return Requires<TException>(check, condition, message, null);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="message"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="message">The message to display if the <paramref name="condition"/> is false.</param>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, Func<bool> condition, string message) where TException : Exception
        {
            return Requires<TException>(check, condition, message, null);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="arguments"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="arguments">
        ///     The argument to initialize the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <example>
        /// Example:
        /// <code>
        /// Check.Current.Requires&lt;ArgumentNullException&gt;(false, new { message = "A message", paramName = "A parameter" });
        /// </code>
        /// </example>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, bool condition, object arguments) where TException : Exception
        {
            return Requires<TException>(check, condition, null, arguments);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="arguments"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="arguments">
        ///     The argument to initialize the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <example>
        /// Example:
        /// <code>
        /// Check.Current.Requires&lt;ArgumentNullException&gt;(false, new { message = "A message", paramName = "A parameter" });
        /// </code>
        /// </example>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, Func<bool> condition, object arguments) where TException : Exception
        {
            return Requires<TException>(check, condition, null, arguments);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="arguments"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="message">The message to display if the <paramref name="condition"/> is false.</param>
        /// <param name="arguments">
        ///     The argument to initialize the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <example>
        /// Example:
        /// <code>
        /// Check.Current.Requires&lt;ArgumentNullException&gt;(false, "A message", new { paramName = "A parameter" });
        /// </code>
        /// </example>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, bool condition, string message, object arguments) where TException : Exception
        {
            if (condition)
            {
                return check;
            }

            if (string.IsNullOrEmpty(message) && arguments == null)
            {
                Check.ThrowException<TException>();
            }

            if (arguments == null)
            {
                Check.ThrowException<TException>(message);
            }

            Check.ThrowException<TException>(message, arguments);

            throw new InvalidOperationException("An exception should be thrown...");
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="arguments"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="check">The <see cref="Check"/> instance that this method extends.</param>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="message">The message to display if the <paramref name="condition"/> is false.</param>
        /// <param name="arguments">
        ///     The argument to initialize the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <example>
        /// Example:
        /// <code>
        /// Check.Current.Requires&lt;ArgumentNullException&gt;(false, "A message", new { paramName = "A parameter" });
        /// </code>
        /// </example>
        /// <returns>The <see cref="Check"/> instance for method chaining.</returns>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static Check Requires<TException>(this Check check, Func<bool> condition, string message, object arguments) where TException : Exception
        {
            return Requires<TException>(check, condition(), message, arguments);
        }
    }
}