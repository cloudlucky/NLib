﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckError.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib
{
    using System;

    /// <summary>
    /// Helper methodes for <see cref="Check"/>.
    /// </summary>
    [Obsolete("Use NLib.Check class instead", true)]
    public static class CheckError
    {
        /// <summary>
        /// Throws <see cref="System.ArgumentException"/> if the <paramref name="condition"/> is false.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">Name of the param.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentException(bool condition, string paramName)
        {
            ArgumentException(condition, paramName, string.Empty);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentException"/> if the <paramref name="condition"/> is false.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="message">The message.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentException(bool condition, string paramName, string message)
        {
            Check.Requires<ArgumentException>(condition, message, new { paramName });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null.
        /// </summary>
        /// <param name="param">The param to check if it's null.</param>
        /// <param name="paramName">Name of the param.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentNullException(object param, string paramName)
        {
            ArgumentNullException(param, paramName, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null.
        /// </summary>
        /// <param name="param">The param to check if it's null.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="message">The message.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentNullException(object param, string paramName, string message)
        {
            Check.Requires<ArgumentNullException>(param != null, message, new { paramName });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="param">The param to check if it's null.</param>
        /// <param name="paramName">Name of the param.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentNullOrEmptyException(string param, string paramName)
        {
            ArgumentNullOrEmptyException(param, paramName, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrEmpty(string)"/>.
        /// </summary>
        /// <param name="param">The param to check if it's null.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="message">The message.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentNullOrEmptyException(string param, string paramName, string message)
        {
            Check.Requires<ArgumentNullException>(!string.IsNullOrEmpty(param), message, new { paramName });
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="param">The param to check if it's null.</param>
        /// <param name="paramName">Name of the param.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentNullOrWhiteSpaceException(string param, string paramName)
        {
            ArgumentNullOrWhiteSpaceException(param, paramName, null);
        }

        /// <summary>
        /// Throws <see cref="System.ArgumentNullException"/> if the <paramref name="param"/> is null or  <see cref="string.IsNullOrWhiteSpace(string)"/>.
        /// </summary>
        /// <param name="param">The param to check if it's null.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <param name="message">The message.</param>
        [Obsolete("Use NLib.Check.ArgumentException method instead", true)]
        public static void ArgumentNullOrWhiteSpaceException(string param, string paramName, string message)
        {
            Check.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(param), message, new { paramName });
        }
    }
}