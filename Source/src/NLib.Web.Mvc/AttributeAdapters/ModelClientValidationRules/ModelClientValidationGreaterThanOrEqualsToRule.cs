namespace NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules
{
    /// <summary>
    /// Provides a container for a greater than or equality validation rule that is sent to the browser.
    /// </summary>
    public class ModelClientValidationGreaterThanOrEqualsToRule : ModelClientValidationCompareRuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelClientValidationGreaterThanOrEqualsToRule"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="otherPropertyName">Name of the other property.</param>
        public ModelClientValidationGreaterThanOrEqualsToRule(string errorMessage, string otherPropertyName)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationType = "greaterthanorequalsto";
            this.ValidationParameters["other"] = ModelClientValidationCompareRuleBase.FormatPropertyForClientValidation(otherPropertyName);
        }
    }
}
