// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RationalNumber.cs" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NLib
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Xml.Serialization;

    using NLib.Resources;

    /// <summary>
    /// Class allowing management of fraction.
    /// The following operators are defined: +, -, *, /, ==, !=, etc. 
    /// </summary>
    [DebuggerDisplay("{Numerator} / {Denominator}")]
    [Serializable]
    [XmlRoot]
    [CLSCompliant(false)]
    public struct RationalNumber : IConvertible, IComparable<RationalNumber>, IEquatable<RationalNumber>, IComparable
    {
        /// <summary>
        /// Represents the number one (1 / 1).
        /// </summary>
        public static readonly RationalNumber One = 1;

        /// <summary>
        /// Represents the number zero (0 / 1).
        /// </summary>
        public static readonly RationalNumber Zero = 0;

        /// <summary>
        /// Represents a value that is not a number (NaN) (0 / 0).
        /// </summary>
        public static readonly RationalNumber NaN;

        /// <summary>
        /// Represents the positive infinite.
        /// </summary>
        public static readonly RationalNumber PositiveInfinity = new RationalNumber { Numerator = 1, Denominator = 0 };

        /// <summary>
        /// Represents the negative infinite.
        /// </summary>
        public static readonly RationalNumber NegativeInfinity = new RationalNumber { Numerator = -1, Denominator = 0 };

        /// <summary>
        /// Represents the smallest possible value of a <see cref="RationalNumber"/>. This field is constant.
        /// </summary>
        public static readonly RationalNumber MinValue = double.MinValue;

        /// <summary>
        /// Represents the biggest possible value of a <see cref="RationalNumber"/>. This field is constant.
        /// </summary>
        public static readonly RationalNumber MaxValue = double.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="RationalNumber"/> struct.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        public RationalNumber(long numerator)
            : this(numerator, 1L)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RationalNumber"/> struct.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <exception cref="DivideByZeroException">The denominator must be non-zero.</exception>
        public RationalNumber(long numerator, long denominator)
            : this()
        {
            this.Initialize(numerator, denominator);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RationalNumber"/> struct.
        /// </summary>
        /// <param name="s">The <see cref="string"/> to convert.</param>
        /// <example>
        /// Example 1
        /// <code>
        /// var r = new RationalNumber("1");
        /// </code>
        /// Example 2
        /// <code>
        /// var r = new RationalNumber("1 / 2");
        /// var r2 = new RationalNumber("1/2");
        /// </code>
        /// Example 3
        /// <code>
        /// var r = new RationalNumber("1 / 1 / 2");
        /// var r2 = new RationalNumber("1/1/2");
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public RationalNumber(string s)
            : this(s, CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RationalNumber"/> struct.
        /// </summary>
        /// <param name="s">The <see cref="string"/> to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <example>
        /// Example 1
        /// <code>
        /// var r = new RationalNumber("1", CultureInfo.CurrentCulture);
        /// </code>
        /// Example 2
        /// <code>
        /// var r = new RationalNumber("1 / 2", CultureInfo.CurrentCulture);
        /// var r2 = new RationalNumber("1/2", CultureInfo.CurrentCulture);
        /// </code>
        /// Example 3
        /// <code>
        /// var r = new RationalNumber("1 / 1 / 2", CultureInfo.CurrentCulture);
        /// var r2 = new RationalNumber("1/1/2", CultureInfo.CurrentCulture);
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public RationalNumber(string s, IFormatProvider provider)
            : this()
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                this.Initialize(0, 1);
            }
            else
            {
                var parts = s.Split('/');

                Check.Current.Requires<ArgumentException>(parts.Length <= 3, RationalNumberResource.RationalNumber_ArgumentException_S);

                var power = 0;
                int numerator;
                var denominator = 1;

                switch (parts.Length)
                {
                    case 3:
                        power = Convert.ToInt32(parts[0].Trim(), provider);
                        numerator = Convert.ToInt32(parts[1].Trim(), provider);
                        denominator = Convert.ToInt32(parts[2].Trim(), provider);
                        break;
                    case 2:
                        numerator = Convert.ToInt32(parts[0].Trim(), provider);
                        denominator = Convert.ToInt32(parts[1].Trim(), provider);
                        break;
                    case 1:
                        numerator = Convert.ToInt32(parts[0].Trim(), provider);
                        break;
                    default:
                        throw new ArithmeticException(RationalNumberResource.RationalNumber_ArithmeticException);
                }

                this.Initialize((power * denominator) + numerator, denominator);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RationalNumber"/> struct.
        /// </summary>
        /// <param name="d">The <see cref="decimal"/> to convert.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public RationalNumber(decimal d)
            : this()
        {
            this.Initialize(Convert.ToDouble(d));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RationalNumber"/> struct.
        /// </summary>
        /// <param name="d">The <see cref="double"/> to convert.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public RationalNumber(double d)
            : this()
        {
            this.Initialize(d);
        }

        /// <summary>
        /// Gets the numerator.
        /// </summary>
        [XmlAttribute]
        public long Numerator { get; private set; }

        /// <summary>
        /// Gets the denominator.
        /// </summary>
        [XmlAttribute]
        public long Denominator { get; private set; }

        /// <summary>
        /// Adds two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>A <see cref="RationalNumber"/> value that is the sum of <paramref name="r1"/> and <paramref name="r2"/>.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Add(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber((r1.Numerator * r2.Denominator) + (r2.Numerator * r1.Denominator), r1.Denominator * r2.Denominator);
        }

        /// <summary>
        /// Decrements the <see cref="RationalNumber.Numerator"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> operand.</param>
        /// <returns>The value of d decremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="RationalNumber.Numerator"/> is less than <see cref="long.MinValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Decrement(RationalNumber r)
        {
            return r - 1;
        }

        /// <summary>
        /// Divides two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>A <see cref="RationalNumber"/> value that is the result of dividing r1 by r2.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Divide(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber(r1.Numerator * r2.Denominator, r2.Numerator * r1.Denominator);
        }

        /// <summary>
        /// Returns a value indicating whether two specified instances of <see cref="RationalNumber"/> represent the same value.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>true if r1 and r2 are equal; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool Equals(RationalNumber r1, RationalNumber r2)
        {
            return r1.Numerator == r2.Numerator && r1.Denominator == r2.Denominator;
        }

        /// <summary>
        /// Increments the <see cref="RationalNumber.Numerator"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> operand.</param>
        /// <returns>The value of d incremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="RationalNumber.Numerator"/> is greater than <see cref="long.MaxValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Increment(RationalNumber r)
        {
            return r + 1;
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative or positive infinity
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>true if <paramref name="r"/> evaluates to <see cref="RationalNumber.PositiveInfinity"/> or <see cref="RationalNumber.NegativeInfinity"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsInfinity(RationalNumber r)
        {
            return IsNegativeInfinity(r) || IsPositiveInfinity(r);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative infinity.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>
        /// true if <paramref name="r"/> evaluates to <see cref="RationalNumber.NegativeInfinity"/>; otherwise, false.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsNegativeInfinity(RationalNumber r)
        {
            return r == NegativeInfinity;
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>
        /// true if <paramref name="r"/> evaluates to <see cref="RationalNumber.PositiveInfinity"/>; otherwise, false.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsPositiveInfinity(RationalNumber r)
        {
            return r == PositiveInfinity;
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to a value that is not a number (<see cref="RationalNumber.NaN"/>).
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>true if <paramref name="r"/> evaluates to <see cref="RationalNumber.NaN"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsNaN(RationalNumber r)
        {
            return r == NaN;
        }

        /// <summary>
        /// Converts the string representation of a number to its rational number equivalent.
        /// </summary>
        /// <param name="s">A string containing a rational number to convert. </param>
        /// <returns>A rational number equivalent to the number contained in <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
        public static RationalNumber Parse(string s)
        {
            return Parse(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the string representation of a number to its rational number equivalent.
        /// </summary>
        /// <param name="s">A <see cref="string"/> containing a rational number to convert. </param>
        /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        /// <returns>A rational number equivalent to the number contained in <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
        public static RationalNumber Parse(string s, IFormatProvider provider)
        {
            Check.Current.ArgumentNullException(s, "s");

            return new RationalNumber(s, provider);
        }

        /// <summary>
        /// Returns the value of the <see cref="RationalNumber"/> operand (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> operand.</param>
        /// <returns>The value of the operand, <paramref name="r"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Plus(RationalNumber r)
        {
            return r;
        }

        /// <summary>
        /// Multiplies two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/> (the multiplicand).</param>
        /// <param name="r2">A <see cref="RationalNumber"/> (the multiplier).</param>
        /// <returns>A <see cref="RationalNumber"/> that is the result of multiplying d1 and d2.</returns>
        /// <exception cref="OverflowException">The numerator/denominator is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/> ."</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Multiply(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber(r1.Numerator * r2.Numerator, r1.Denominator * r2.Denominator);
        }

        /// <summary>
        /// Returns the remainder resulting from dividing two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/> (the dividend).</param>
        /// <param name="r2">A <see cref="RationalNumber"/> (the divisor).</param>
        /// <returns>The <see cref="RationalNumber"/> remainder resulting from dividing <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="DivideByZeroException">d2 is zero</exception>
        /// <exception cref="OverflowException">The return value is less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Mod(RationalNumber r1, RationalNumber r2)
        {
            return r1.ToDecimal() % r2.ToDecimal();
        }

        /// <summary>
        /// Returns the result of multiplying the specified <see cref="RationalNumber"/> value by negative one.
        /// </summary>
        /// <param name="r">A <see cref="RationalNumber"/>.</param>
        /// <returns>A <see cref="RationalNumber"/> with the value of d, but the opposite sign.  -or- Zero, if <paramref name="r"/> is zero.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Negate(RationalNumber r)
        {
            return new RationalNumber(r.Numerator * -1, r.Denominator);
        }

        /// <summary>
        /// Reverses the numerator with the denominator.
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> to reverse.</param>
        /// <returns>A new <see cref="RationalNumber"/>.</returns>
        /// <exception cref="DivideByZeroException">The numerator must not be zero.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Reverse(RationalNumber r)
        {
            Check.Current.Requires<DivideByZeroException>(r.Numerator != 0, RationalNumberResource.Reverse_DivideByZeroException_R);

            return new RationalNumber(r.Denominator, r.Numerator);
        }

        /// <summary>
        /// Subtracts one specified <see cref="RationalNumber"/> value from another.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/> (the minuend).</param>
        /// <param name="r2">A <see cref="RationalNumber"/> (the subtrahend).</param>
        /// <returns>The <see cref="RationalNumber"/> result of subtracting d2 from d1.</returns>
        /// <exception cref="OverflowException">The numerator/denominator is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/> ."</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber Subtract(RationalNumber r1, RationalNumber r2)
        {
            return new RationalNumber((r1.Numerator * r2.Denominator) - (r2.Numerator * r1.Denominator), r1.Denominator * r2.Denominator);
        }

        /// <summary>
        /// Converts the string representation of a number to its rational number equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A <see cref="string"/> containing a number to convert. </param>
        /// <param name="result">When this method returns, contains the rational number value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or <see cref="RationalNumber.Zero"/> if the conversion failed. The conversion fails if the <paramref name="s"/> parameter is null, is not of the correct format, or represents a number less than <see cref="RationalNumber.MinValue"/> or greater than <see cref="RationalNumber.MaxValue"/>. This parameter is passed uninitialized.</param>
        /// <returns></returns>
        public static bool TryParse(string s, out RationalNumber result)
        {
            return TryParse(s, CultureInfo.CurrentCulture, out result);
        }

        /// <summary>
        /// Converts the string representation of a number to its rational number equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A <see cref="string"/> containing a number to convert. </param>
        /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        /// <param name="result">When this method returns, contains the rational number value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or <see cref="RationalNumber.Zero"/> if the conversion failed. The conversion fails if the <paramref name="s"/> parameter is null, is not of the correct format, or represents a number less than <see cref="RationalNumber.MinValue"/> or greater than <see cref="RationalNumber.MaxValue"/>. This parameter is passed uninitialized.</param>
        /// <returns></returns>
        public static bool TryParse(string s, IFormatProvider provider, out RationalNumber result)
        {
            result = Zero;

            if (s != null)
            {
                try
                {
                    result = new RationalNumber(s, provider);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="decimal"/> to <see cref="RationalNumber"/>.
        /// </summary>
        /// <param name="value">The <see cref="decimal"/>.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RationalNumber(decimal value)
        {
            return new RationalNumber(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="RationalNumber"/>.
        /// </summary>
        /// <param name="value">The <see cref="double"/>.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RationalNumber(double value)
        {
            return new RationalNumber(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="long"/> to <see cref="RationalNumber"/>.
        /// </summary>
        /// <param name="value">The <see cref="long"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator RationalNumber(long value)
        {
            return new RationalNumber(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="RationalNumber"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/>.</param>
        /// <returns>The result of the conversion.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "The constructor will handle the culture")]
        public static implicit operator RationalNumber(string value)
        {
            return new RationalNumber(value);
        }

        /// <summary>
        /// Subtracts two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>The result of the operator.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator -(RationalNumber r1, RationalNumber r2)
        {
            return Subtract(r1, r2);
        }

        /// <summary>
        /// Negates the value of the specified <see cref="RationalNumber"/> operand.
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> operand.</param>
        /// <returns>The result of the operator.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator -(RationalNumber r)
        {
            return Negate(r);
        }

        /// <summary>
        /// Decrements the <see cref="RationalNumber.Numerator"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> operand.</param>
        /// <returns>The value of d decremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="RationalNumber.Numerator"/> is less than <see cref="long.MinValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator --(RationalNumber r)
        {
            return Decrement(r);
        }

        /// <summary>
        /// Returns a value indicating whether two instances of <see cref="RationalNumber"/> are not equal.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>true if <paramref name="r1"/> and <paramref name="r2"/> are not equal; otherwise, false..</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator !=(RationalNumber r1, RationalNumber r2)
        {
            return !Equals(r1, r2);
        }

        /// <summary>
        /// Returns the remainder resulting from dividing two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/> (the dividend).</param>
        /// <param name="r2">A <see cref="RationalNumber"/> (the divisor).</param>
        /// <returns>The <see cref="RationalNumber"/> remainder resulting from dividing <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="DivideByZeroException">d2 is zero</exception>
        /// <exception cref="OverflowException">The return value is less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator %(RationalNumber r1, RationalNumber r2)
        {
            return Mod(r1, r2);
        }

        /// <summary>
        /// Multiplies two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>The <see cref="RationalNumber"/> result of multiplying <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="OverflowException">The return value is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator *(RationalNumber r1, RationalNumber r2)
        {
            return Multiply(r1, r2);
        }

        /// <summary>
        /// Divides two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/> (the dividend).</param>
        /// <param name="r2">A <see cref="RationalNumber"/> (the divisor).</param>
        /// <returns>The <see cref="RationalNumber"/> result of <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="DivideByZeroException">d2 is zero</exception>
        /// <exception cref="OverflowException">The return value is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator /(RationalNumber r1, RationalNumber r2)
        {
            return Divide(r1, r2);
        }

        /// <summary>
        /// Adds two specified <see cref="RationalNumber"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>The <see cref="RationalNumber"/> result of <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="OverflowException">The return value is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator +(RationalNumber r1, RationalNumber r2)
        {
            return Add(r1, r2);
        }

        /// <summary>
        /// Returns the value of the <see cref="RationalNumber"/> operand (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> operand.</param>
        /// <returns>The value of the operand, <paramref name="r"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator +(RationalNumber r)
        {
            return Plus(r);
        }

        /// <summary>
        /// Increments the <see cref="RationalNumber.Numerator"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="RationalNumber"/> operand.</param>
        /// <returns>The value of d incremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="RationalNumber.Numerator"/> is greater than <see cref="long.MaxValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static RationalNumber operator ++(RationalNumber r)
        {
            return Increment(r);
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="RationalNumber.Numerator"/> is less than another specified <see cref="RationalNumber.Numerator"/>.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>true if <paramref name="r1"/> is less than <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator <(RationalNumber r1, RationalNumber r2)
        {
            return r1.ToDouble() < r2.ToDouble();
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="RationalNumber.Numerator"/> is less than or equal to another specified <see cref="RationalNumber.Numerator"/>.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>true if <paramref name="r1"/> is less than or to equal <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator <=(RationalNumber r1, RationalNumber r2)
        {
            return r1.ToDouble() <= r2.ToDouble();
        }

        /// <summary>
        /// Returns a value indicating whether two instances of <see cref="RationalNumber.Numerator"/> are equal.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>true if <paramref name="r1"/> is less than or to equal <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator ==(RationalNumber r1, RationalNumber r2)
        {
            return Equals(r1, r2);
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="RationalNumber.Numerator"/> is greater than another specified <see cref="RationalNumber.Numerator"/>.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>true if <paramref name="r1"/> is greater than <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator >(RationalNumber r1, RationalNumber r2)
        {
            return r1.ToDouble() > r2.ToDouble();
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="RationalNumber.Numerator"/> is greater than or equal to another specified <see cref="RationalNumber.Numerator"/>.
        /// </summary>
        /// <param name="r1">A <see cref="RationalNumber"/>.</param>
        /// <param name="r2">A <see cref="RationalNumber"/>.</param>
        /// <returns>true if <paramref name="r1"/> is greater than or to equal <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator >=(RationalNumber r1, RationalNumber r2)
        {
            return r1.ToDouble() >= r2.ToDouble();
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="obj"/> is not the same type as this instance. </exception>
        public int CompareTo(object obj)
        {
            try
            {
                return this.CompareTo((RationalNumber)obj);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException(RationalNumberResource.CompareTo_InvalidCastException_Obj, ex);
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="other"/>. Zero This instance is equal to <paramref name="other"/>. Greater than zero This instance is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(RationalNumber other)
        {
            return this.ToDouble().CompareTo(other.ToDouble());
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            try
            {
                return this.Equals((RationalNumber)obj);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="RationalNumber"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="RationalNumber"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="RationalNumber"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(RationalNumber other)
        {
            return this.Numerator == other.Numerator && this.Denominator == other.Denominator;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Numerator.GetHashCode() ^ this.Denominator.GetHashCode();
        }

        /// <summary>
        /// Negates this instance.
        /// </summary>
        /// <returns>A new negative <see cref="RationalNumber"/></returns>
        public RationalNumber Negate()
        {
            return Negate(this);
        }

        /// <summary>
        /// Reverses the numerator with the denominator.
        /// </summary>
        /// <returns>A new <see cref="RationalNumber"/>.</returns>
        /// <exception cref="DivideByZeroException">The numerator must not be zero.</exception>
        public RationalNumber Reverse()
        {
            return Reverse(this);
        }

        /// <summary>
        /// Convert to <see cref="decimal"/>.
        /// </summary>
        /// <returns>The <see cref="decimal"/> value of the current instance.</returns>
        public decimal ToDecimal()
        {
            return decimal.Divide(this.Numerator, this.Denominator);
        }

        /// <summary>
        /// Convert to <see cref="double"/>.
        /// </summary>
        /// <returns>The <see cref="double"/> value of the current instance.</returns>
        public double ToDouble()
        {
            return Convert.ToDouble(this.Numerator) / Convert.ToDouble(this.Denominator);
        }

        /// <summary>
        /// Convert to <see cref="long"/>.
        /// </summary>
        /// <returns>The <see cref="long"/> value of the current instance.</returns>
        public long ToInt64()
        {
            return this.Numerator / this.Denominator;
        }

        /// <summary>
        /// Convert to <see cref="float"/>.
        /// </summary>
        /// <returns>The <see cref="float"/> value of the current instance.</returns>
        public float ToSingle()
        {
            return Convert.ToSingle(this.Numerator) / Convert.ToSingle(this.Denominator);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToString(null);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <param name="longForm">if set to <c>true</c> se representative is in 3 parts {0} / {1} / {2}; otherwise is in two parts {0} / {1}.</param>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public string ToString(bool longForm)
        {
            return this.ToString(longForm, null);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public string ToString(IFormatProvider provider)
        {
            return this.ToString(false, provider);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <param name="longForm">if set to <c>true</c> se representative is in 3 parts {0} / {1} / {2}; otherwise is in two parts {0} / {1}.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public string ToString(bool longForm, IFormatProvider provider)
        {
            if (longForm && (this.Numerator > this.Denominator || this.Numerator < this.Denominator * -1))
            {
                var nb = this.Numerator;

                if (this.Numerator < 0)
                {
                    while (nb % this.Denominator != 0)
                    {
                        ++nb;
                    }

                    return string.Format(provider, "{0} / {1} / {2}", nb / this.Denominator, (this.Numerator * -1) + nb, this.Denominator);
                }

                while (nb % this.Denominator != 0)
                {
                    --nb;
                }

                return string.Format(provider, "{0} / {1} / {2}", nb / this.Denominator, this.Numerator - nb, this.Denominator);
            }

            return string.Format(provider, "{0} / {1}", this.Numerator, this.Denominator);
        }

        /// <summary>
        /// Returns the <see cref="T:System.TypeCode"/> for this instance.
        /// </summary>
        /// <returns>
        /// The enumerated constant that is the <see cref="T:System.TypeCode"/> of the class or value type that implements this interface.
        /// </returns>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Double;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A Boolean value equivalent to the value of this instance.
        /// </returns>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(this.ToInt64(), provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 8-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(this.ToDouble(), provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A Unicode character equivalent to the value of this instance.
        /// </returns>
        /// <exception cref="NotSupportedException">The conversion is not supported.</exception>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.DateTime"/> using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> instance equivalent to the value of this instance.
        /// </returns>
        /// <exception cref="NotSupportedException">The conversion is not supported.</exception>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.Decimal"/> number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="T:System.Decimal"/> number equivalent to the value of this instance.
        /// </returns>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return this.ToDecimal();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A double-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return this.ToDouble();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 16-bit signed integer equivalent to the value of this instance.
        /// </returns>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(this.ToInt64(), provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 32-bit signed integer equivalent to the value of this instance.
        /// </returns>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(this.ToInt64(), provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 64-bit signed integer equivalent to the value of this instance.
        /// </returns>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return this.ToInt64();
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 8-bit signed integer equivalent to the value of this instance.
        /// </returns>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(this.ToDouble(), provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A single-precision floating-point number equivalent to the value of this instance.
        /// </returns>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return this.ToSingle();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return this.ToString(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an <see cref="T:System.Object"/> of the specified <see cref="T:System.Type"/> that has an equivalent value, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="conversionType">The <see cref="T:System.Type"/> to which the value of this instance is converted.</param>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> instance of type <paramref name="conversionType"/> whose value is equivalent to the value of this instance.
        /// </returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this.ToDouble(), conversionType, provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 16-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(this.ToInt64());
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 32-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(this.ToInt64(), provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// An 64-bit unsigned integer equivalent to the value of this instance.
        /// </returns>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(this.ToInt64(), provider);
        }

        /// <summary>
        /// Initializes the specified numerator and denominator.
        /// </summary>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <exception cref="DivideByZeroException">The denominator must be non-zero.</exception>
        private void Initialize(long numerator, long denominator)
        {
            Check.Current.Requires<DivideByZeroException>(denominator != 0, RationalNumberResource.Initialize_DivideByZeroException_Denominator);

            this.Numerator = numerator;
            this.Denominator = denominator;

            this.Reduce();

            if (this.Denominator < 0)
            {
                this.Numerator *= -1;
                this.Denominator *= -1;
            }
        }

        /// <summary>
        /// Initializes the rational number.
        /// </summary>
        /// <param name="d">The number.</param>
        /// <exception cref="ArgumentException">The number must not be NaN.</exception>
        /// <exception cref="ArgumentException">The number must not be Infinite.</exception>
        private void Initialize(double d)
        {
            if (double.IsNaN(d))
            {
                this.Denominator = NaN.Denominator;
                this.Numerator = NaN.Numerator;
                return;
            }

            if (double.IsNegativeInfinity(d))
            {
                this.Denominator = NegativeInfinity.Denominator;
                this.Numerator = NegativeInfinity.Numerator;
                return;
            }

            if (double.IsPositiveInfinity(d))
            {
                this.Denominator = PositiveInfinity.Denominator;
                this.Numerator = PositiveInfinity.Numerator;
                return;
            }

            if (double.MinValue == d)
            {
                this.Denominator = MinValue.Denominator;
                this.Numerator = MinValue.Numerator;
                return;
            }

            if (double.MaxValue == d)
            {
                this.Denominator = MaxValue.Denominator;
                this.Numerator = MaxValue.Numerator;
                return;
            }

            if (d == 0.0)
            {
                this.Initialize(0, 1);
            }
            else
            {
                // http://fisheye1.atlassian.com/browse/~raw,rationalNumber=1.5/matheclipse/org.matheclipse.basic/src/apache/harmony/math/Rational.java
                var sgn = 1;
                if (d < 0.0)
                {
                    sgn = -1;
                    d = -d;
                }

                var intPart = (long)d;
                var z = d - intPart;
                if (z == 0)
                {
                    this.Initialize(sgn * intPart, 1);
                }
                else
                {
                    z = 1.0 / z;
                    var a = (long)z;
                    z = z - a;
                    var prevNum = 0L;
                    var numerator = 1L;
                    var prevDen = 1L;
                    var denominator = a;
                    var approxAns = (((double)denominator * intPart) + numerator) / denominator;

                    while (Math.Abs((d - approxAns) / d) >= double.Epsilon)
                    {
                        z = 1.0 / z;
                        a = (long)z;
                        z = z - a;

                        // deal with too-big numbers:
                        if (((double)a * numerator) + prevNum > long.MaxValue || ((double)a * denominator) + prevDen > long.MaxValue || ((double)a * numerator) + prevNum < long.MinValue || ((double)a * denominator) + prevDen < long.MinValue)
                        {
                            break;
                        }

                        var tmp = (a * numerator) + prevNum;
                        prevNum = numerator;
                        numerator = tmp;
                        tmp = (a * denominator) + prevDen;
                        prevDen = denominator;
                        denominator = tmp;
                        approxAns = (((double)denominator * intPart) + numerator) / denominator;
                    }

                    if (denominator != PositiveInfinity.Denominator && denominator != PositiveInfinity.Numerator)
                    {
                        this.Initialize(sgn * ((denominator * intPart) + numerator), denominator);
                    }
                    else
                    {
                        if (numerator == PositiveInfinity.Numerator || numerator == NegativeInfinity.Numerator)
                        {
                            this.Numerator = numerator;
                            this.Denominator = denominator;
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("d");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reduces the rational number.
        /// </summary>
        private void Reduce()
        {
            var gcd = MathHelper.GreatCommonDivisor(this.Numerator, this.Denominator);

            this.Numerator /= gcd;
            this.Denominator /= gcd;
        }
    }
}