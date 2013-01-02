// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathHelper.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib
{
    using System;

    using NLib.Resources;

    /// <summary>
    /// Adds some methodes to <see cref="Math"/>.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Get the Factorial of the the specified <paramref name="nb"/>.
        /// </summary>
        /// <param name="nb">The number.</param>
        /// <returns>The factorial</returns>
        public static double Factorial(double nb)
        {
            Check.Current.Requires<ArgumentException>(nb >= 0, MathHelperResource.Factorial_ArgumentException_Nb, new { paramName = "nb" });
            
            var result = 1.0;

            for (var i = 1; i <= nb; ++i)
            {
                result *= i;
            }

            return result;
        }

        /// <summary>
        /// Get the Great Common Divisor (GCD)
        /// </summary>
        /// <param name="nb1">The first number.</param>
        /// <param name="nb2">The second number.</param>
        /// <returns>The Great Common Divisor</returns>
        public static long GreatCommonDivisor(long nb1, long nb2)
        {
            while (nb2 != 0)
            {
                var remainder = nb1 % nb2;
                nb1 = nb2;
                nb2 = remainder;
            }

            return nb1;
        }

        /// <summary>
        /// Get the Least Common Multiple (LCM)
        /// </summary>
        /// <param name="nb1">The first number.</param>
        /// <param name="nb2">The second number.</param>
        /// <returns>The Least Common Multiple</returns>
        public static long LeastCommonMultiple(long nb1, long nb2)
        {
            if (nb1 == 0 || nb2 == 0)
            {
                return 0;
            }

            var gcd = GreatCommonDivisor(nb1, nb2);

            return (nb1 * nb2) / gcd;
        }
    }
}