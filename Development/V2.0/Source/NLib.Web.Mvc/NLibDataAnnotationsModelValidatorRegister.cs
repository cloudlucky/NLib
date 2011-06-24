// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLibDataAnnotationsModelValidatorRegister.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Web.Mvc
{
    using System.Web.Mvc;

    using NLib.ComponentModel.DataAnnotations;
    using NLib.Web.Mvc.AttributeAdapters;

    /// <summary>
    /// Helper class to register DataAnnotations model validator.
    /// </summary>
    public class NLibDataAnnotationsModelValidatorRegister
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
