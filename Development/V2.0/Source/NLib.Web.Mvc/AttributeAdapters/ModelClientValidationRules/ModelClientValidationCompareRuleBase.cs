// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelClientValidationCompareRuleBase.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Web.Mvc.AttributeAdapters.ModelClientValidationRules
{
    using System.Web.Mvc;

    /// <summary>
    /// Provides a base container for any compare validation rule that is sent to the browser.
    /// </summary>
    public abstract class ModelClientValidationCompareRuleBase : ModelClientValidationRule
    {
        /// <summary>
        /// Formats the property for client validation.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The property formatted for client validation.</returns>
        public static string FormatPropertyForClientValidation(string property)
        {
            CheckError.ArgumentNullOrWhiteSpaceException(property, property);

            return "*." + property;
        }
    }
}
