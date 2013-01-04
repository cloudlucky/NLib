namespace NLib.ComponentModel.DataAnnotations
{
    using System;

    using NLib.ComponentModel.DataAnnotations.Resources;

    /// <summary>
    /// Provides an attribute that compares two properties of a model.
    /// Validate if the two properties are not equal.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEqualsToAttribute : CompareBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqualsToAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">Name of the other property.</param>
        public NotEqualsToAttribute(string otherPropertyName)
            : base(otherPropertyName, DataAnnotationsResource.NotEqualsToAttribute_ValidationError)
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
            return currentValue.CompareTo(otherValue) != 0;
        }
    }
}
