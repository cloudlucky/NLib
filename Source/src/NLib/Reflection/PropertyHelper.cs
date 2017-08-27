using System;
using System.Reflection;

namespace NLib.Reflection
{
    /// <summary>
    /// Represents support for property reflection.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <typeparam name="TKey">The property's type.</typeparam>
    public class PropertyHelper<T, TKey>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyHelper{T, TKey}"/> class.
        /// </summary>
        /// <param name="reflectionHelper">The reflection helper.</param>
        /// <param name="propertyInfo">The property info.</param>
        public PropertyHelper(ReflectionHelper<T> reflectionHelper, PropertyInfo propertyInfo)
        {
            this.ReflectionHelper = reflectionHelper;
            this.PropertyInfo = propertyInfo;
            this.PropertyInfoType = propertyInfo.PropertyType;
        }

        /// <summary>
        /// Gets the reflection helper.
        /// </summary>
        public ReflectionHelper<T> ReflectionHelper { get; }

        /// <summary>
        /// Gets the property info.
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Gets the type of the property info.
        /// </summary>
        public Type PropertyInfoType { get; }
    }
}
