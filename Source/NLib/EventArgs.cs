// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventArgs.cs" company=".">
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
    /// <see cref="EventArgs{T}"/> is the base class for classes containing generic event data.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    [Serializable]
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// The value.
        /// </summary>
        private readonly T value;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public EventArgs(T value)
        {
            this.value = value;
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
