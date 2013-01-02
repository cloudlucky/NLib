// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareBaseAttribute.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.ComponentModel.DataAnnotations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using NLib.ComponentModel.DataAnnotations.Resources;

    /// <summary>
    /// Provides a base attribute that compares two properties of a model.
    /// </summary>
    public abstract class CompareBaseAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareBaseAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">Name of the other property.</param>
        protected CompareBaseAttribute(string otherPropertyName)
            : this(otherPropertyName, (string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareBaseAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">Name of the other property.</param>
        /// <param name="errorMessage">The error message.</param>
        protected CompareBaseAttribute(string otherPropertyName, string errorMessage)
            : this(otherPropertyName, () => errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareBaseAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">Name of the other property.</param>
        /// <param name="errorMessageAccessor">The error message accessor.</param>
        protected CompareBaseAttribute(string otherPropertyName, Func<string> errorMessageAccessor)
            : base(errorMessageAccessor)
        {
            this.OtherPropertyName = otherPropertyName;
        }

        /// <summary>
        /// Gets the name of the other property.
        /// </summary>
        public string OtherPropertyName { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the two properties must be the same type or not.
        /// </summary>
        public bool MustBeSameType { get; set; }

        /// <summary>
        /// Applies formatting to an error message, based on the data field where the error occurred.
        /// </summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>An instance of the formatted error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString, name, this.OtherPropertyName);
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        /// <exception cref="ValidationException">The <paramref name="value"/> does not implement <see cref="IComparable"/>.</exception>
        /// <exception cref="ValidationException">The <see cref="OtherPropertyName"/> does not implement <see cref="IComparable"/>.</exception>
        /// <exception cref="ValidationException">The <see cref="MustBeSameType"/> is true and the type of <paramref name="value"/> and <see cref="OtherPropertyName"/> are different.</exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Check.Current.ArgumentNullException(validationContext, "validationContext");

            var otherValue = this.GetOtherProperty(validationContext);

            if (value == null && otherValue == null)
            {
                return ValidationResult.Success;
            }

            var currentValueComparable = value as IComparable;
            Check.Current.Requires<ValidationException>(currentValueComparable != null, new { errorMessage = string.Format(DataAnnotationsResource.CompareBaseAttribute_DoesNotImplementIComparable, validationContext.DisplayName), validatingAttribute = this, value });
            
            if (!this.MustBeSameType)
            {
                var currentValueType = currentValueComparable.GetType();
                if (!otherValue.GetType().IsAssignableFrom(currentValueType))
                {
                    try
                    {
                        otherValue = Convert.ChangeType(otherValue, currentValueType, CultureInfo.CurrentCulture);
                    }
                    catch (FormatException ex)
                    {
                        throw new ValidationException(string.Format(DataAnnotationsResource.CompareBaseAttribute_PropertiesCannotBeCompared, validationContext.DisplayName, this.OtherPropertyName), ex);
                    }
                }
            }
            else
            {
                var currentValueType = currentValueComparable.GetType();
                var otherValueType = otherValue.GetType();

                if (!currentValueType.IsAssignableFrom(otherValueType) || !otherValueType.IsAssignableFrom(currentValueType))
                {
                    throw new ValidationException(string.Format(DataAnnotationsResource.CompareBaseAttribute_TypeMissMatch, validationContext.DisplayName, currentValueType.Name, this.OtherPropertyName, otherValueType.Name));
                }
            }

            return this.IsValid(currentValueComparable, otherValue)
                ? ValidationResult.Success
                : new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName ?? validationContext.MemberName));
        }

        /// <summary>
        /// Gets the other property.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The other property.</returns>
        /// <exception cref="ValidationException">The <see cref="OtherPropertyName"/> is not found.</exception>
        protected object GetOtherProperty(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(this.OtherPropertyName);

            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

                return secondValue;
            }

            throw new ValidationException(string.Format(DataAnnotationsResource.CompareBaseAttribute_UnknownProperty, this.OtherPropertyName));
        }

        /// <summary>
        /// Determines whether the specified current value is valid.
        /// </summary>
        /// <param name="currentValue">The current value.</param>
        /// <param name="otherValue">The other value.</param>
        /// <returns>
        ///   <c>true</c> if the specified current value is valid; otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool IsValid(IComparable currentValue, object otherValue);
    }
}