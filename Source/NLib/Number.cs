namespace NLib
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using NLib.Resources;

    /// <summary>
    /// Represents any number like. It can be a float, integer and rational number.
    /// </summary>
    [Serializable]
    [CLSCompliant(false)]
    public struct Number : IConvertible, IComparable<Number>, IEquatable<Number>, IComparable
    {
        /// <summary>
        /// Represents the number one.
        /// </summary>
        public static readonly Number One = 1;

        /// <summary>
        /// Represents the number zero.
        /// </summary>
        public static readonly Number Zero = 0;

        /// <summary>
        /// Represents a value that is not a number (NaN).
        /// </summary>
        public static readonly Number NaN = double.NaN;

        /// <summary>
        /// Represents the positive infinite.
        /// </summary>
        public static readonly Number PositiveInfinity = double.PositiveInfinity;

        /// <summary>
        /// Represents the negative infinite.
        /// </summary>
        public static readonly Number NegativeInfinity = double.NegativeInfinity;

        /// <summary>
        /// Represents the smallest possible value of a <see cref="Number"/>. This field is constant.
        /// </summary>
        public static readonly Number MinValue = double.MinValue;

        /// <summary>
        /// Represents the biggest possible value of a <see cref="Number"/>. This field is constant.
        /// </summary>
        public static readonly Number MaxValue = double.MaxValue;

        /// <summary>
        /// Uses as the representation of the number.
        /// </summary>
        private readonly double model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Number" /> struct.
        /// </summary>
        /// <param name="l">A long value.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public Number(long l)
        {
            this.model = l;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number" /> struct.
        /// </summary>
        /// <param name="i">An integer value.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public Number(int i)
        {
            this.model = i;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number" /> struct.
        /// </summary>
        /// <param name="l">An unsigned value.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public Number(ulong l)
        {
            this.model = l;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="s">The <see cref="string"/> to convert.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public Number(string s)
            : this(s, CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="s">The <see cref="string"/> to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public Number(string s, IFormatProvider provider)
        {
            this.model = double.Parse(s, provider);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="d">The <see cref="decimal"/> to convert.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public Number(decimal d)
        {
            this.model = Convert.ToDouble(d);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="d">The <see cref="double"/> to convert.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public Number(double d)
        {
            this.model = d;
        }

        /// <summary>
        /// Adds two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>A <see cref="Number"/> value that is the sum of <paramref name="r1"/> and <paramref name="r2"/>.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Add(Number r1, Number r2)
        {
            return r1.model + r2.model;
        }

        /// <summary>
        /// Decrements the <see cref="Number"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="Number"/> operand.</param>
        /// <returns>The value of d decremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="Number"/> is less than <see cref="long.MinValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Decrement(Number r)
        {
            return r - 1;
        }

        /// <summary>
        /// Divides two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>A <see cref="Number"/> value that is the result of dividing r1 by r2.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Divide(Number r1, Number r2)
        {
            return r1.model / r2.model;
        }

        /// <summary>
        /// Returns a value indicating whether two specified instances of <see cref="Number"/> represent the same value.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>true if r1 and r2 are equal; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool Equals(Number r1, Number r2)
        {
            return r1.model.Equals(r2.model);
        }

        /// <summary>
        /// Increments the <see cref="Number"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="Number"/> operand.</param>
        /// <returns>The value of d incremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="Number"/> is greater than <see cref="long.MaxValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Increment(Number r)
        {
            return r + 1;
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative or positive infinity
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>true if <paramref name="r"/> evaluates to <see cref="Number.PositiveInfinity"/> or <see cref="Number.NegativeInfinity"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsInfinity(Number r)
        {
            return IsNegativeInfinity(r) || IsPositiveInfinity(r);
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to negative infinity.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>
        /// true if <paramref name="r"/> evaluates to <see cref="Number.NegativeInfinity"/>; otherwise, false.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsNegativeInfinity(Number r)
        {
            return r == NegativeInfinity;
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to positive infinity.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>
        /// true if <paramref name="r"/> evaluates to <see cref="Number.PositiveInfinity"/>; otherwise, false.
        /// </returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsPositiveInfinity(Number r)
        {
            return r == PositiveInfinity;
        }

        /// <summary>
        /// Returns a value indicating whether the specified number evaluates to a value that is not a number (<see cref="Number.NaN"/>).
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>true if <paramref name="r"/> evaluates to <see cref="Number.NaN"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool IsNaN(Number r)
        {
            return r == NaN;
        }

        /// <summary>
        /// Converts the string representation of a number to its number equivalent.
        /// </summary>
        /// <param name="s">A string containing a number to convert. </param>
        /// <returns>A number equivalent to the number contained in <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Parse(string s)
        {
            return Parse(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the string representation of a number to its number equivalent.
        /// </summary>
        /// <param name="s">A <see cref="string"/> containing a number to convert. </param>
        /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        /// <returns>A number equivalent to the number contained in <paramref name="s"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Parse(string s, IFormatProvider provider)
        {
            Check.Current.ArgumentNullException(s, "s");

            return new Number(s, provider);
        }

        /// <summary>
        /// Converts the string representation of a number to its number equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A <see cref="string"/> containing a number to convert. </param>
        /// <param name="result">When this method returns, contains the number value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or <see cref="RationalNumber.Zero"/> if the conversion failed. The conversion fails if the <paramref name="s"/> parameter is null, is not of the correct format, or represents a number less than <see cref="Number.MinValue"/> or greater than <see cref="Number.MaxValue"/>. This parameter is passed uninitialized.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool TryParse(string s, out Number result)
        {
            return TryParse(s, CultureInfo.CurrentCulture, out result);
        }

        /// <summary>
        /// Converts the string representation of a number to its number equivalent. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="s">A <see cref="string"/> containing a number to convert. </param>
        /// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s"/>.</param>
        /// <param name="result">When this method returns, contains the number value equivalent to the number contained in <paramref name="s"/>, if the conversion succeeded, or <see cref="RationalNumber.Zero"/> if the conversion failed. The conversion fails if the <paramref name="s"/> parameter is null, is not of the correct format, or represents a number less than <see cref="Number.MinValue"/> or greater than <see cref="Number.MaxValue"/>. This parameter is passed uninitialized.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool TryParse(string s, IFormatProvider provider, out Number result)
        {
            result = Zero;

            if (s != null)
            {
                try
                {
                    result = new Number(s, provider);
                }
                catch (FormatException)
                {
                    return false;
                }
                catch (OverflowException)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the value of the <see cref="Number"/> operand (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="r">The <see cref="Number"/> operand.</param>
        /// <returns>The value of the operand, <paramref name="r"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Plus(Number r)
        {
            return r;
        }

        /// <summary>
        /// Multiplies two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/> (the multiplicand).</param>
        /// <param name="r2">A <see cref="Number"/> (the multiplier).</param>
        /// <returns>A <see cref="Number"/> that is the result of multiplying d1 and d2.</returns>
        /// <exception cref="OverflowException">The numerator/denominator is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/> ."</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Multiply(Number r1, Number r2)
        {
            return r1.model * r2.model;
        }

        /// <summary>
        /// Returns the remainder resulting from dividing two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/> (the dividend).</param>
        /// <param name="r2">A <see cref="Number"/> (the divisor).</param>
        /// <returns>The <see cref="Number"/> remainder resulting from dividing <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="DivideByZeroException">d2 is zero</exception>
        /// <exception cref="OverflowException">The return value is less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Mod(Number r1, Number r2)
        {
            return r1.model % r2.model;
        }

        /// <summary>
        /// Returns the result of multiplying the specified <see cref="Number"/> value by negative one.
        /// </summary>
        /// <param name="r">A <see cref="Number"/>.</param>
        /// <returns>A <see cref="Number"/> with the value of d, but the opposite sign.  -or- Zero, if <paramref name="r"/> is zero.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Negate(Number r)
        {
            return -r.model;
        }

        /// <summary>
        /// Subtracts one specified <see cref="Number"/> value from another.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/> (the minuend).</param>
        /// <param name="r2">A <see cref="Number"/> (the subtrahend).</param>
        /// <returns>The <see cref="Number"/> result of subtracting d2 from d1.</returns>
        /// <exception cref="OverflowException">The numerator/denominator is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/> ."</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number Subtract(Number r1, Number r2)
        {
            return r1.model - r2.model;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="Number"/>.
        /// </summary>
        /// <param name="value">The <see cref="double"/>.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Number(decimal value)
        {
            return new Number(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="double"/> to <see cref="Number"/>.
        /// </summary>
        /// <param name="value">The <see cref="double"/>.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Number(double value)
        {
            return new Number(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="long"/> to <see cref="Number"/>.
        /// </summary>
        /// <param name="value">The <see cref="int"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Number(long value)
        {
            return new Number(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="int"/> to <see cref="Number"/>.
        /// </summary>
        /// <param name="value">The <see cref="int"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Number(int value)
        {
            return new Number(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="ulong"/> to <see cref="Number"/>.
        /// </summary>
        /// <param name="value">The <see cref="ulong"/> to convert.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Number(ulong value)
        {
            return new Number(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="string"/> to <see cref="Number"/>.
        /// </summary>
        /// <param name="value">The <see cref="string"/>.</param>
        /// <returns>The result of the conversion.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "The constructor will handle the culture")]
        public static implicit operator Number(string value)
        {
            return new Number(value);
        }

        /// <summary>
        /// Subtracts two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>The result of the operator.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator -(Number r1, Number r2)
        {
            return Subtract(r1, r2);
        }

        /// <summary>
        /// Negates the value of the specified <see cref="Number"/> operand.
        /// </summary>
        /// <param name="r">The <see cref="Number"/> operand.</param>
        /// <returns>The result of the operator.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator -(Number r)
        {
            return Negate(r);
        }

        /// <summary>
        /// Decrements the <see cref="Number"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="Number"/> operand.</param>
        /// <returns>The value of d decremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="Number"/> is less than <see cref="long.MinValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator --(Number r)
        {
            return Decrement(r);
        }

        /// <summary>
        /// Returns a value indicating whether two instances of <see cref="Number"/> are not equal.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>true if <paramref name="r1"/> and <paramref name="r2"/> are not equal; otherwise, false..</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator !=(Number r1, Number r2)
        {
            return !Equals(r1, r2);
        }

        /// <summary>
        /// Returns the remainder resulting from dividing two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/> (the dividend).</param>
        /// <param name="r2">A <see cref="Number"/> (the divisor).</param>
        /// <returns>The <see cref="Number"/> remainder resulting from dividing <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="DivideByZeroException">d2 is zero</exception>
        /// <exception cref="OverflowException">The return value is less than <see cref="double.MinValue"/> or greater than <see cref="double.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "r", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator %(Number r1, Number r2)
        {
            return Mod(r1, r2);
        }

        /// <summary>
        /// Multiplies two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>The <see cref="Number"/> result of multiplying <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="OverflowException">The return value is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator *(Number r1, Number r2)
        {
            return Multiply(r1, r2);
        }

        /// <summary>
        /// Divides two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/> (the dividend).</param>
        /// <param name="r2">A <see cref="Number"/> (the divisor).</param>
        /// <returns>The <see cref="Number"/> result of <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="DivideByZeroException">d2 is zero</exception>
        /// <exception cref="OverflowException">The return value is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator /(Number r1, Number r2)
        {
            return Divide(r1, r2);
        }

        /// <summary>
        /// Adds two specified <see cref="Number"/> values.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>The <see cref="Number"/> result of <paramref name="r1"/> by <paramref name="r2"/>.</returns>
        /// <exception cref="OverflowException">The return value is less than <see cref="long.MinValue"/> or greater than <see cref="long.MaxValue"/>.</exception>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator +(Number r1, Number r2)
        {
            return Add(r1, r2);
        }

        /// <summary>
        /// Returns the value of the <see cref="Number"/> operand (the sign of the operand is unchanged).
        /// </summary>
        /// <param name="r">The <see cref="Number"/> operand.</param>
        /// <returns>The value of the operand, <paramref name="r"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator +(Number r)
        {
            return Plus(r);
        }

        /// <summary>
        /// Increments the <see cref="Number"/> operand by one.
        /// </summary>
        /// <param name="r">The <see cref="Number"/> operand.</param>
        /// <returns>The value of d incremented by 1.</returns>
        /// <exception cref="OverflowException">The <see cref="Number"/> is greater than <see cref="long.MaxValue"/></exception>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static Number operator ++(Number r)
        {
            return Increment(r);
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Number"/> is less than another specified <see cref="Number"/>.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>true if <paramref name="r1"/> is less than <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator <(Number r1, Number r2)
        {
            return r1.model < r2.model;
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Number"/> is less than or equal to another specified <see cref="Number"/>.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>true if <paramref name="r1"/> is less than or to equal <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator <=(Number r1, Number r2)
        {
            return r1.model <= r2.model;
        }

        /// <summary>
        /// Returns a value indicating whether two instances of <see cref="Number"/> are equal.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>true if <paramref name="r1"/> is less than or to equal <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator ==(Number r1, Number r2)
        {
            return Equals(r1, r2);
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Number"/> is greater than another specified <see cref="Number"/>.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>true if <paramref name="r1"/> is greater than <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator >(Number r1, Number r2)
        {
            return r1.model > r2.model;
        }

        /// <summary>
        /// Returns a value indicating whether a specified <see cref="Number"/> is greater than or equal to another specified <see cref="Number"/>.
        /// </summary>
        /// <param name="r1">A <see cref="Number"/>.</param>
        /// <param name="r2">A <see cref="Number"/>.</param>
        /// <returns>true if <paramref name="r1"/> is greater than or to equal <paramref name="r2"/>; otherwise, false.</returns>
        [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1625:ElementDocumentationMustNotBeCopiedAndPasted", Justification = "Reviewed. Suppression is OK here. It's like the System.Decimal documentation.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Parameter name is enough meaningful in the current context")]
        public static bool operator >=(Number r1, Number r2)
        {
            return r1.model >= r2.model;
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
                return this.CompareTo((Number)obj);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException(NumberResource.CompareTo_InvalidCastException_Obj, ex);
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="other"/>. Zero This instance is equal to <paramref name="other"/>. Greater than zero This instance is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(Number other)
        {
            return this.model.CompareTo(other.model);
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
                return this.Equals((Number)obj);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="Number"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Number"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="Number"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(Number other)
        {
            return this.model.Equals(other.model);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.model.GetHashCode();
        }

        /// <summary>
        /// Negates this instance.
        /// </summary>
        /// <returns>A new negative <see cref="Number"/></returns>
        public Number Negate()
        {
            return Negate(this);
        }

        /// <summary>
        /// Convert to <see cref="decimal"/>.
        /// </summary>
        /// <returns>The <see cref="decimal"/> value of the current instance.</returns>
        public decimal ToDecimal()
        {
            return Convert.ToDecimal(this.model);
        }

        /// <summary>
        /// Convert to <see cref="double"/>.
        /// </summary>
        /// <returns>The <see cref="double"/> value of the current instance.</returns>
        public double ToDouble()
        {
            return this.model;
        }

        /// <summary>
        /// Convert to <see cref="long"/>.
        /// </summary>
        /// <returns>The <see cref="long"/> value of the current instance.</returns>
        public long ToInt64()
        {
            return Convert.ToInt64(this.model);
        }

        /// <summary>
        /// Convert to <see cref="float"/>.
        /// </summary>
        /// <returns>The <see cref="float"/> value of the current instance.</returns>
        public float ToSingle()
        {
            return Convert.ToSingle(this.model);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent <see cref="string"/> representation, using the specified format.
        /// </summary>
        /// <returns>The <see cref="string"/> representation of the value of this instance as specified by format.</returns>
        public override string ToString()
        {
            return this.ToString(NumberFormatInfo.CurrentInfo);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent <see cref="string"/> representation, using the specified format.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The <see cref="string"/> representation of the value of this instance as specified by format.</returns>
        public string ToString(IFormatProvider provider)
        {
            return this.model.ToString(provider);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent <see cref="string"/> representation, using the specified format.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <returns>The <see cref="string"/> representation of the value of this instance as specified by format.</returns>
        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent <see cref="string"/> representation, using the specified format.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The <see cref="string"/> representation of the value of this instance as specified by format.</returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return this.model.ToString(format, provider);
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
            return ((IConvertible)this.model).ToBoolean(provider);
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
            return ((IConvertible)this.model).ToByte(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A Unicode character equivalent to the value of this instance.
        /// </returns>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return ((IConvertible)this.model).ToChar(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="T:System.DateTime"/> using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> instance equivalent to the value of this instance.
        /// </returns>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)this.model).ToDateTime(provider);
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
            return ((IConvertible)this.model).ToDecimal(provider);
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
            return ((IConvertible)this.model).ToDouble(provider);
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
            return ((IConvertible)this.model).ToInt16(provider);
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
            return ((IConvertible)this.model).ToInt32(provider);
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
            return ((IConvertible)this.model).ToInt64(provider);
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
            return ((IConvertible)this.model).ToSByte(provider);
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
            return ((IConvertible)this.model).ToSingle(provider);
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
            return ((IConvertible)this.model).ToString(provider);
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
            return ((IConvertible)this.model).ToType(conversionType, provider);
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
            return ((IConvertible)this.model).ToUInt16(provider);
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
            return ((IConvertible)this.model).ToUInt32(provider);
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
            return ((IConvertible)this.model).ToUInt64(provider);
        }
    }
}
