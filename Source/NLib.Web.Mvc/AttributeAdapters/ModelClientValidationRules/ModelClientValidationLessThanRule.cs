namespace NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules
{
    /// <summary>
    /// Provides a container for a less than validation rule that is sent to the browser.
    /// </summary>
    public class ModelClientValidationLessThanRule : ModelClientValidationCompareRuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelClientValidationLessThanRule"/> class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="otherPropertyName">Name of the other property.</param>
        public ModelClientValidationLessThanRule(string errorMessage, string otherPropertyName)
        {
            this.ErrorMessage = errorMessage;
            this.ValidationType = "lessthan";
            this.ValidationParameters["other"] = ModelClientValidationCompareRuleBase.FormatPropertyForClientValidation(otherPropertyName);
        }
    }
}
