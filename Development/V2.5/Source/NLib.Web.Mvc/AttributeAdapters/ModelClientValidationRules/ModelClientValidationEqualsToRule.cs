namespace NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules
{
    /// <summary>
    /// Provides a container for an equality validation rule that is sent to the browser.
    /// </summary>
    public class ModelClientValidationEqualsToRule : ModelClientValidationCompareRuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelClientValidationEqualsToRule"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="otherPropertyName">Name of the other property.</param>
        public ModelClientValidationEqualsToRule(string errorMessage, string otherPropertyName)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationType = "equalsto";
            this.ValidationParameters["other"] = ModelClientValidationCompareRuleBase.FormatPropertyForClientValidation(otherPropertyName);
        }
    }
}
