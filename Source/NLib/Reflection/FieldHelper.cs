namespace NLib.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents support for property reflection.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <typeparam name="TKey">The field's type.</typeparam>
    public class FieldHelper<T, TKey>
    {
        /// <summary>
        /// The reflection helper.
        /// </summary>
        private readonly ReflectionHelper<T> reflectionHelper;

        /// <summary>
        /// The field.
        /// </summary>
        private readonly FieldInfo fieldInfo;

        /// <summary>
        /// The field's type
        /// </summary>
        private readonly Type fieldInfoType;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldHelper{T, TKey}"/> class.
        /// </summary>
        /// <param name="reflectionHelper">The reflection helper.</param>
        /// <param name="fieldInfo">The field info.</param>
        public FieldHelper(ReflectionHelper<T> reflectionHelper, FieldInfo fieldInfo)
        {
            this.reflectionHelper = reflectionHelper;
            this.fieldInfo = fieldInfo;
            this.fieldInfoType = fieldInfo.FieldType;
        }

        /// <summary>
        /// Gets the reflection helper.
        /// </summary>
        public ReflectionHelper<T> ReflectionHelper
        {
            get { return this.reflectionHelper; }
        }

        /// <summary>
        /// Gets the field info.
        /// </summary>
        public FieldInfo FieldInfo
        {
            get { return this.fieldInfo; }
        }

        /// <summary>
        /// Gets the type of the field info.
        /// </summary>
        public Type FieldInfoType
        {
            get { return this.fieldInfoType; }
        }
    }
}
