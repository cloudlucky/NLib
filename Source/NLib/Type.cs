// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Type.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib
{
    using System;

    /// <summary>
    /// Represents strongly-typed declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    public class Type<T>
    {
        /// <summary>
        /// The type of the current.
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// The value.
        /// </summary>
        private readonly T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Type{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Type(T value)
        {
            CheckError.ArgumentNullException(value, "value");

            this.type = value.GetType();
            this.value = value;
        }

        /// <summary>
        /// Gets the type of the current.
        /// </summary>
        public Type CurrentType
        {
            get { return this.type; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value
        {
            get { return this.value; }
        }
    }
}
