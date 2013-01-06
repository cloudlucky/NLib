namespace NLib.Web.Mvc.AttributeAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using NLib.ComponentModel.DataAnnotations;
    using NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules;

    /// <summary>
    /// Provides an adapter for the <see cref="GreaterThanOrEqualsToAttribute"/> attribute.
    /// </summary>
    public class GreaterThanOrEqualsToAttributeAdapter : DataAnnotationsModelValidator<GreaterThanOrEqualsToAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqualsToAttributeAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public GreaterThanOrEqualsToAttributeAdapter(ModelMetadata metadata, ControllerContext context, GreaterThanOrEqualsToAttribute attribute)
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
            yield return new ModelClientValidationGreaterThanOrEqualsToRule(this.Attribute.FormatErrorMessage(this.Metadata.GetDisplayName()), this.Attribute.OtherPropertyName);
        }
    }
}
