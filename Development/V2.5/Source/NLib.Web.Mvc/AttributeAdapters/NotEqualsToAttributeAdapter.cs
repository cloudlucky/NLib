namespace NLib.Web.Mvc.AttributeAdapters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using NLib.ComponentModel.DataAnnotations;
    using NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules;

    /// <summary>
    /// Provides an adapter for the <see cref="NotEqualsToAttribute"/> attribute.
    /// </summary>
    public class NotEqualsToAttributeAdapter : DataAnnotationsModelValidator<NotEqualsToAttribute>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqualsToAttributeAdapter"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="context">The context.</param>
        /// <param name="attribute">The attribute.</param>
        public NotEqualsToAttributeAdapter(ModelMetadata metadata, ControllerContext context, NotEqualsToAttribute attribute)
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
            yield return new ModelClientValidationNotEqualsToRule(this.Attribute.FormatErrorMessage(this.Metadata.GetDisplayName()), this.Attribute.OtherPropertyName);
        }
    }
}
