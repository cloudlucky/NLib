using System;
using System.Reflection;

namespace NLib.Reflection
{
    /// <summary>
    /// Represents support for property reflection.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <typeparam name="TKey">The field's type.</typeparam>
    public class FieldHelper<T, TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldHelper{T, TKey}"/> class.
        /// </summary>
        /// <param name="reflectionHelper">The reflection helper.</param>
        /// <param name="fieldInfo">The field info.</param>
        public FieldHelper(ReflectionHelper<T> reflectionHelper, FieldInfo fieldInfo)
        {
            this.ReflectionHelper = reflectionHelper;
            this.FieldInfo = fieldInfo;
            this.FieldInfoType = fieldInfo.FieldType;
        }

        /// <summary>
        /// Gets the reflection helper.
        /// </summary>
        public ReflectionHelper<T> ReflectionHelper { get; }

        /// <summary>
        /// Gets the field info.
        /// </summary>
        public FieldInfo FieldInfo { get; }

        /// <summary>
        /// Gets the type of the field info.
        /// </summary>
        public Type FieldInfoType { get; }
    }
}
