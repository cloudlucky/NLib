// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Check.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Contains static methods for representing program check.
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="condition">The conditional expression to test.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static void Requires<TException>(bool condition) where TException : Exception
        {
            Requires<TException>(condition, string.Empty);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="exception">The exception thrown if the <paramref name="condition"/> is false.</param>
        public static void Requires<TException>(bool condition, TException exception) where TException : Exception
        {
            if (!condition)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="message"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="message">The message to display if the <paramref name="condition"/> is false.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static void Requires<TException>(bool condition, string message) where TException : Exception
        {
            Requires<TException>(condition, message, null);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="arguments"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="arguments">
        ///     The argument to initialise the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <example>
        /// Example:
        /// <code>
        /// Check.Requires&lt;ArgumentNullException&gt;(false, new { message = "A message", paramName = "A parameter" });
        /// </code>
        /// </example>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static void Requires<TException>(bool condition, object arguments) where TException : Exception
        {
            Requires<TException>(condition, null, arguments);
        }

        /// <summary>
        /// Specifies a check contract for the enclosing method or property, and throws an exception with the provided <paramref name="arguments"/> if the <paramref name="condition"/> for the contract fails.
        /// </summary>
        /// <typeparam name="TException">The exception to throw if the <paramref name="condition"/> is false.</typeparam>
        /// <param name="condition">The conditional expression to test.</param>
        /// <param name="message">The message to display if the <paramref name="condition"/> is false.</param>
        /// <param name="arguments">
        ///     The argument to initialise the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <example>
        /// Example:
        /// <code>
        /// Check.Requires&lt;ArgumentNullException&gt;(false, "A message", new { paramName = "A parameter" });
        /// </code>
        /// </example>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static void Requires<TException>(bool condition, string message, object arguments) where TException : Exception
        {
            if (condition)
            {
                return;
            }

            if (string.IsNullOrEmpty(message) && arguments == null)
            {
                RequiresSimple<TException>();
            }

            if (arguments == null)
            {
                RequiresWithMessage<TException>(message);
            }

            RequiresComplexe<TException>(message, arguments);
        }

        /// <summary>
        /// Throw exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The exception to throw.</typeparam>
        /// <exception cref="MissingConstructorException">The <typeparamref name="TException"/> don't have default constructor.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        private static void RequiresSimple<TException>() where TException : Exception
        {
            var exception = typeof(TException);

            throw (TException)Activator.CreateInstance(exception);
        }

        /// <summary>
        /// Throw exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The exception to throw</typeparam>
        /// <param name="message">The message to display</param>
        /// <exception cref="MissingConstructorException">The <typeparamref name="TException"/> don't have a constructor with one parameter of type string.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        private static void RequiresWithMessage<TException>(string message) where TException : Exception
        {
            var exception = typeof(TException);

            try
            {
                throw (TException)Activator.CreateInstance(exception, new[] { message });
            }
            catch (MissingMethodException ex)
            {
                throw new MissingConstructorException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Throw exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The exception to throw</typeparam>
        /// <param name="message">The message to display</param>
        /// <param name="arguments">
        ///     The argument to initialise the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        private static void RequiresComplexe<TException>(string message, object arguments) where TException : Exception
        {
            var exception = typeof(TException);
            var argumentsType = arguments.GetType();
            var argumentsProperties = argumentsType.GetProperties();
            var argumentsPropertiesLength = argumentsProperties.Length + (message == null ? 0 : 1);
            var l = new List<object>();
            var constructorParametersLength = 0;

            foreach (var parameters in exception.GetConstructors().Select(c => c.GetParameters()))
            {
                constructorParametersLength = parameters.Length;

                if (constructorParametersLength == argumentsPropertiesLength)
                {
                    bool constructorFound = true;
                    foreach (var p in parameters)
                    {
                        if (message != null && p.Name == "message")
                        {
                            l.Add(message);
                        }
                        else
                        {
                            var p2 = p;
                            var p1 = argumentsProperties.Where(x => x.Name == p2.Name).FirstOrDefault();

                            if (p1 == null || !p1.PropertyType.IsAssignableFrom(p.ParameterType))
                            {
                                l.Clear();
                                constructorFound = false;
                                break; // parameters doesn't match with the current constructor. continue to another one.
                            }

                            l.Add(p1.GetValue(arguments, null));
                        }
                    }

                    if (constructorFound)
                    {
                        break;
                    }
                }
            }

            if (constructorParametersLength != l.Count)
            {
                throw new MissingConstructorException("Constructor parameters length or type doesn't match with arguments.", exception.FullName);
            }

            throw (TException)Activator.CreateInstance(exception, l.ToArray());
        }
    }
}
