namespace NLib.Text.Extensions
{
    using System;
    using System.Text;

    /// <summary>
    /// Defines extensions methods for <see cref="StringBuilder"/>.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends the format string and adds a line.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object to format.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="sb"/> parameter is null.</exception>
        public static void AppendLineFormat(this StringBuilder sb, string format, params object[] args)
        {
            Check.Current.ArgumentNullException(sb, "sb");

            sb.AppendFormat(format, args);
            sb.AppendLine();
        }

        /// <summary>
        /// Appends the format string and adds a line.
        /// </summary>
        /// <param name="sb">The string builder.</param>
        /// <param name="provider">An object that supplied culture-specific formatting information.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object to format.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="sb" /> parameter is null.</exception>
        public static void AppendLineFormat(this StringBuilder sb, IFormatProvider provider, string format, params object[] args)
        {
            Check.Current.ArgumentNullException(sb, "sb");

            sb.AppendFormat(provider, format, args);
            sb.AppendLine();
        }
    }
}
