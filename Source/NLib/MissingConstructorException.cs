namespace NLib
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConstructorException"/> class.
        /// </summary>
        public MissingConstructorException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConstructorException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public MissingConstructorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConstructorException"/> class with a specified error message 
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConstructorException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="className">The name of the class in which access to a nonexistent method was attempted.</param>
        public MissingConstructorException(string message, string className)
            : base(message)
        {
            this.className = className;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingConstructorException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected MissingConstructorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets the text string showing the class name, the method name, and the signature of the missing method. This property is read-only.
        /// </summary>
        public override string Message
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.className) 
                    ? base.Message 
                    : string.Format(CultureInfo.CurrentCulture, "{0}: {1}", this.className, base.Message);
            }
        }

        /// <summary>
        /// Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object with the class name, the member name, the signature of the missing member, and additional exception information.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> object is null. </exception>
        /// <PermissionSet>
        /// <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
