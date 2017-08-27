using System;
using System.Globalization;

namespace NLib
{
    /// <inheritdoc />
    /// <summary>
    /// The exception that is thrown when there is an attempt to dynamically access a constructor that does not exist.
    /// </summary>
    [Serializable]
    public class MissingConstructorException : MissingMethodException
    {
        /// <summary>
        /// The name of the class in which access to a nonexistent method was attempted.
        /// </summary>
        private readonly string className = string.Empty;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.MissingConstructorException" /> class.
        /// </summary>
        public MissingConstructorException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.MissingConstructorException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public MissingConstructorException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.MissingConstructorException" /> class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.
        /// </param>
        public MissingConstructorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NLib.MissingConstructorException" /> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="className">The name of the class in which access to a nonexistent method was attempted.</param>
        public MissingConstructorException(string message, string className)
            : base(message)
        {
            this.className = className;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Gets the text string showing the class name, the method name, and the signature of the missing method. This property is read-only.
        /// </summary>
        public override string Message => string.IsNullOrWhiteSpace(this.className) 
            ? base.Message 
            : string.Format(CultureInfo.CurrentCulture, "{0}: {1}", this.className, base.Message);
    }
}
