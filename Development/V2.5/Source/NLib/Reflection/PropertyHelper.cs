namespace NLib.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents support for property reflection.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <typeparam name="TKey">The property's type.</typeparam>
    public class PropertyHelper<T, TKey>
    {
        /// <summary>
        /// The reflection helper.
        /// </summary>
        private readonly ReflectionHelper<T> reflectionHelper;

        /// <summary>
        /// The property info.
        /// </summary>
        private readonly PropertyInfo propertyInfo;

        /// <summary>
        /// The property's type.
        /// </summary>
        private readonly Type propertyInfoType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyHelper{T, TKey}"/> class.
        /// </summary>
        /// <param name="reflectionHelper">The reflection helper.</param>
        /// <param name="propertyInfo">The property info.</param>
        public PropertyHelper(ReflectionHelper<T> reflectionHelper, PropertyInfo propertyInfo)
        {
            this.reflectionHelper = reflectionHelper;
            this.propertyInfo = propertyInfo;
            this.propertyInfoType = propertyInfo.PropertyType;
        }

        /// <summary>
        /// Gets the reflection helper.
        /// </summary>
        public ReflectionHelper<T> ReflectionHelper
        {
            get { return this.reflectionHelper; }
        }

        /// <summary>
        /// Gets the property info.
        /// </summary>
        public PropertyInfo PropertyInfo
        {
            get { return this.propertyInfo; }
        }

        /// <summary>
        /// Gets the type of the property info.
        /// </summary>
        public Type PropertyInfoType
        {
            get { return this.propertyInfoType; }
        }
    }
}
