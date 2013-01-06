namespace NLib.Xml.Linq.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// Defines extensions methods for <see cref="XDocument"/>.
    /// </summary>
    public static class XDocumentExtensions
    {
        /// <summary>
        /// Convert a <see cref="XDocument"/> to a <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="xdocument">The <see cref="XDocument"/> to convert.</param>
        /// <returns>A new <see cref="XmlDocument"/>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="xdocument"/> must not be null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", Justification = "Utility method to convert.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "The spelling is correct.")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "XDocument is the equivalent of the XmlDocument.")]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public static XmlDocument ToXmlDocument(this XDocument xdocument)
        {
            Check.Current.ArgumentNullException(xdocument, "xdocument");

            using (var reader = xdocument.CreateReader())
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(reader);
                
                return xmlDocument;
            }
        }
    }
}
