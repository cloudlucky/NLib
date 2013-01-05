﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Reflection
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using NLib.Extensions;

    /// <summary>
    /// Represents support for reflection.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    public class ReflectionHelper<T>
    {
        /// <summary>
        /// The value.
        /// </summary>
        private readonly T value;

        /// <summary>
        /// The type of the value.
        /// </summary>
        private readonly Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionHelper{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ReflectionHelper(T value)
        {
            this.value = value;
            this.type = value.GetType();
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        public Type Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// Gets the field by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The field if exists; otherwise null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        public FieldInfo GetField(string name)
        {
            Check.ArgumentNullException(name, "name");

            return TypeExtension.GetField(this.Type, name);
        }

        /// <summary>
        /// Gets the property by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The property if exists; otherwise null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is null.</exception>
        public PropertyInfo GetProperty(string name)
        {
            Check.ArgumentNullException(name, "name");

            return TypeExtension.GetProperty(this.Type, name);
        }

        /// <summary>
        /// Gets the member by selector.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The member if exists; otherwise null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        public MemberInfo GetMemberInfo<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            Check.ArgumentNullException(keySelector, "keySelector");

            return this.Type.GetMemberInfo(keySelector);
        }
    }
}