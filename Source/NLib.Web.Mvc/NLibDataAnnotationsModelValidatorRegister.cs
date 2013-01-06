namespace NLib.Web.Mvc
{
    using System.Web.Mvc;

    using NLib.ComponentModel.DataAnnotations;
    using NLib.Web.Mvc.AttributeAdapters;

    /// <summary>
    /// Helper class to register DataAnnotations model validator.
    /// </summary>
    public static class NLibDataAnnotationsModelValidatorRegister
    {
        /// <summary>
        /// Registers all NLib adapters.
        /// </summary>
        public static void RegisterAllNLibAdapter()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EqualsToAttribute), typeof(EqualsToAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(GreaterThanAttribute), typeof(GreaterThanAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(GreaterThanOrEqualsToAttribute), typeof(GreaterThanOrEqualsToAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LessThanAttribute), typeof(LessThanAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LessThanOrEqualsToAttribute), typeof(LessThanOrEqualsToAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NotEqualsToAttribute), typeof(NotEqualsToAttributeAdapter));
        }
    }
}
