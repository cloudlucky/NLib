// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyExtension.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib.Reflection.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Defines extensions methods for <see cref="Assembly"/>.
    /// </summary>
    public static class AssemblyExtension
    {
        /// <summary>
        /// Loads the specified manifest resource from this assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="name">The case-sensitive name of the manifest resource being requested.</param>
        /// <returns>A <see cref="string"/> representing the manifest resource</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="assembly"/> parameter is null.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="name"/> parameter is null.</exception>
        /// <exception cref="ArgumentException">The <paramref name="name"/> parameter is an empty string ("").</exception>
        /// <exception cref="System.IO.FileLoadException">A file that was found could not be loaded.</exception>
        /// <exception cref="System.IO.FileNotFoundException"><paramref name="name"/> was not found.</exception>
        /// <exception cref="System.BadImageFormatException"><paramref name="name"/> is not a valid assembly.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CheckError class do the check")]
        public static string GetManifestResourceString(this Assembly assembly, string name)
        {
            Check.ArgumentNullException(assembly, "assembly");

            var stream = assembly.GetManifestResourceStream(name);

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
