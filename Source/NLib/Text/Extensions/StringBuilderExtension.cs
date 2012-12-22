// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringBuilderExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Text.Extensions
{
    using System;
    using System.Text;

    /// <summary>
    /// Defines extensions methods for <see cref="StringBuilder"/>.
    /// </summary>
    public static class StringBuilderExtension
    {
        public static void AppendLineFormat(this StringBuilder sb, string format, params object[] args)
        {
            sb.AppendFormat(format, args);
            sb.AppendLine();
        }

        public static void AppendLineFormat(this StringBuilder sb, IFormatProvider provider, string format, params object[] args)
        {
            sb.AppendFormat(provider, format, args);
            sb.AppendLine();
        }
    }
}
