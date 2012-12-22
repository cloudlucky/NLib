// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LessThanAttributeAdapter.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Web.Mvc.AttributeAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using NLib.ComponentModel.DataAnnotations;
    using NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules;

    /// <summary>
    /// Provides an adapter for the <see cref="LessThanAttribute"/> attribute.
    /// </summary>
    public class LessThanAttributeAdapter : DataAnnotationsModelValidator<LessThanAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanAttributeAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public LessThanAttributeAdapter(ModelMetadata metadata, ControllerContext context, LessThanAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>
        /// Retrieves a collection of client validation rules.
        /// </summary>
        /// <returns>
        /// A collection of client validation rules.
        /// </returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            yield return new ModelClientValidationLessThanRule(this.Attribute.FormatErrorMessage(this.Metadata.GetDisplayName()), this.Attribute.OtherPropertyName);
        }
    }
}
