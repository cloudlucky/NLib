namespace NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules
{
    /// <summary>
    /// Provides a container for a non equality validation rule that is sent to the browser.
    /// </summary>
    public class ModelClientValidationNotEqualsToRule : ModelClientValidationCompareRuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelClientValidationNotEqualsToRule"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="otherPropertyName">Name of the other property.</param>
        public ModelClientValidationNotEqualsToRule(string errorMessage, string otherPropertyName)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationType = "notequalsto";
            this.ValidationParameters["other"] = ModelClientValidationCompareRuleBase.FormatPropertyForClientValidation(otherPropertyName);
        }
    }
}
