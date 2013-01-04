namespace NLib.ComponentModel.DataAnnotations
{
    using System;

    using NLib.ComponentModel.DataAnnotations.Resources;

    /// <summary>
    /// Provides an attribute that compares two properties of a model.
    /// Validate if the current property is less than the other property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LessThanAttribute : CompareBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">Name of the other property.</param>
        public LessThanAttribute(string otherPropertyName)
            : base(otherPropertyName, DataAnnotationsResource.LessThanAttribute_ValidationError)
        {
        }

        /// <summary>
        /// Determines whether the specified current value is valid.
        /// </summary>
        /// <param name="currentValue">The current value.</param>
        /// <param name="otherValue">The other value.</param>
        /// <returns>
        ///   <c>true</c> if the specified current value is valid; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsValid(IComparable currentValue, object otherValue)
        {
            return currentValue.CompareTo(otherValue) < 0;
        }
    }
}
