namespace NLib
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Contains static methods for representing program check.
    /// </summary>
    public sealed class Check
    {
        /// <summary>
        /// Unique instance.
        /// </summary>
        private static readonly Lazy<Check> Instance = new Lazy<Check>(() => new Check());

        /// <summary>
        /// Prevents a default instance of the <see cref="Check" /> class from being created.
        /// </summary>
        private Check()
        {
        }

        /// <summary>
        /// Gets the current instance of <see cref="Check"/>.
        /// </summary>
        public static Check Current
        {
            get { return Instance.Value; }
        }

        /// <summary>
        /// Throw exception of type <typeparamref name="TException"/>.
        /// </summary>
        /// <typeparam name="TException">The exception to throw.</typeparam>
        /// <exception cref="MissingConstructorException">The <typeparamref name="TException"/> don't have default constructor.</exception>
        //[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static void ThrowException<TException>() where TException : Exception
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
        public static void ThrowException<TException>(string message) where TException : Exception
        {
            var exception = typeof(TException);

            try
            {
                throw (TException)Activator.CreateInstance(exception, new object[] { message });
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
        ///     The argument to initialize the exception.
        ///     The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.
        /// </param>
        /// <exception cref="MissingConstructorException">The properties of the <paramref name="arguments"/> must match in name (case-sensitive), type and number of parameters.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "It doesn't make sense to provide TypeParameter because it will be created by Reflection")]
        public static void ThrowException<TException>(string message, object arguments) where TException : Exception
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
                            var p1 = argumentsProperties.FirstOrDefault(x => x.Name == p2.Name);

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
