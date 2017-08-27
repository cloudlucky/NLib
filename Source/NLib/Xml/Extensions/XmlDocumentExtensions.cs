namespace NLib.Xml.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Defines extensions methods for <see cref="XmlDocument"/>.
    /// </summary>
    public static class XmlDocumentExtensions
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
            Check.Current.ArgumentNullException(xmlDocument, "xmlDocument");

            return XDocument.Parse(xmlDocument.OuterXml);
        }
    }
}
