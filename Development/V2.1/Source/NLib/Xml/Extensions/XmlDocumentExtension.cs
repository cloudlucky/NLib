// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlDocumentExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Xml.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Defines extensions methodes for <see cref="XmlDocument"/>.
    /// </summary>
    public static class XmlDocumentExtension
    {
        /// <summary>
        /// Convert a <see cref="XmlDocument"/> to a <see cref="XDocument"/>.
        /// </summary>
        /// <param name="xmlDocument">The xml document to convert.</param>
        /// <returns>A new <see cref="XDocument"/>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="xmlDocument"/> must not be null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", Justification = "Utility method to convert.")]
        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            Check.ArgumentNullException(xmlDocument != null, "xmlDocument");

            using (var reader = new XmlNodeReader(xmlDocument))
            {
                reader.MoveToContent();

                return XDocument.Load(reader);
            }
        }
    }
}
