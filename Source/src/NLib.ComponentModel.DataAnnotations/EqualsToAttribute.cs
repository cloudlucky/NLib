using System;
using System.Diagnostics.CodeAnalysis;

using NLib.ComponentModel.DataAnnotations.Resources;

namespace NLib.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Provides an attribute that compares two properties of a model.
    /// Validate if the two properties are equal.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Reviewed. It's OK. Like MVC attributes.")]
    public class EqualsToAttribute : CompareBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualsToAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">Name of the other property.</param>
        public EqualsToAttribute(string otherPropertyName)
            : base(otherPropertyName, DataAnnotationsResource.EqualsToAttribute_ValidationError)
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
            return currentValue.CompareTo(otherValue) == 0;
        }
    }
}
