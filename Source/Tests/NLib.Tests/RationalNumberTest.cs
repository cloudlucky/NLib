namespace NLib.Tests
{
    using System;
    using System.Globalization;

    using Xunit;

    public class RationalNumberTest
    {
        [Fact]
        public void ConstructorLong1()
        {
            var r = new RationalNumber(1L);

            Assert.Equal(1, r.ToInt64());
            Assert.NotEqual(2, r.ToInt64());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorLong2()
        {
            var r = new RationalNumber(-3L);

            Assert.Equal(-3, r.ToInt64());
            Assert.NotEqual(2, r.ToInt64());

            Assert.Equal(-3, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorLongLong1()
        {
            var r = new RationalNumber(1L, 1L);

            Assert.Equal(1, r.ToInt64());
            Assert.NotEqual(2, r.ToInt64());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorLongLong2()
        {
            var r = new RationalNumber(3L, 4L);

            Assert.Equal(0.75, r.ToDouble());
            Assert.NotEqual(2, r.ToDouble());

            Assert.Equal(3, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorLongLong3()
        {
            var r = new RationalNumber(5L, 4L);

            Assert.Equal(1.25, r.ToDouble());
            Assert.NotEqual(2, r.ToDouble());

            Assert.Equal(5, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorLongLong4()
        {
            var r = new RationalNumber(-2L, 5L);

            Assert.Equal(-0.4, r.ToDouble());
            Assert.NotEqual(2, r.ToDouble());

            Assert.Equal(-2, r.Numerator);
            Assert.Equal(5, r.Denominator);
        }

        [Fact]
        public void ConstructorLongLong5()
        {
            var r = new RationalNumber(4L, 8L);

            Assert.Equal(0.5, r.ToDouble());
            Assert.NotEqual(2, r.ToDouble());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(2, r.Denominator);
        }

        [Fact]
        public void ConstructorLongLong6()
        {
            Assert.Throws(typeof(DivideByZeroException), () => new RationalNumber(4L, 0L));
        }

        [Fact]
        public void ConstructorLongLong7()
        {
            var r = new RationalNumber(1L, -2L);

            Assert.Equal(-0.5, r.ToDouble());
            Assert.NotEqual(2, r.ToDouble());

            Assert.Equal(-1, r.Numerator);
            Assert.Equal(2, r.Denominator);
        }

        [Fact]
        public void ConstructorLongLong8()
        {
            var r = new RationalNumber(-1L, -2L);

            Assert.Equal(0.5, r.ToDouble());
            Assert.NotEqual(2, r.ToDouble());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(2, r.Denominator);
        }

        [Fact]
        public void ConstructorDouble1()
        {
            var r = new RationalNumber(0.25);

            Assert.Equal(0.25, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorDouble2()
        {
            var r = new RationalNumber(double.NaN);

            Assert.Equal(r, RationalNumber.NaN);
        }

        [Fact]
        public void ConstructorDouble3()
        {
            var r = new RationalNumber(double.PositiveInfinity);

            Assert.Equal(r, RationalNumber.PositiveInfinity);
        }

        [Fact]
        public void ConstructorDouble4()
        {
            var r = new RationalNumber(double.NegativeInfinity);

            Assert.Equal(r, RationalNumber.NegativeInfinity);
        }

        [Fact]
        public void ConstructorDouble5()
        {
            var r = new RationalNumber(double.MaxValue);

            Assert.Equal(r, RationalNumber.MaxValue);
        }

        [Fact]
        public void ConstructorDouble6()
        {
            var r = new RationalNumber(double.MinValue);

            Assert.Equal(r, RationalNumber.MinValue);
        }

        [Fact]
        public void ConstructorDouble7()
        {
            var r = new RationalNumber(-1.25);

            Assert.Equal(-1.25, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(-5, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorDouble8()
        {
            var r = new RationalNumber(0.0);

            Assert.Equal(0.0, r.ToDouble());

            Assert.Equal(0, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorDecimal1()
        {
            var r = new RationalNumber(0.25m);

            Assert.Equal(0.25m, r.ToDecimal());
            Assert.NotEqual(2.25m, r.ToDecimal());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorDecimal2()
        {
            var r = new RationalNumber(decimal.MaxValue);

            Assert.Equal(r, RationalNumber.PositiveInfinity);
        }

        [Fact]
        public void ConstructorDecimal3()
        {
            var r = new RationalNumber(decimal.MinValue);

            Assert.Equal(r, RationalNumber.PositiveInfinity);
        }

        [Fact]
        public void ConstructorDecimal4()
        {
            var r = new RationalNumber(-1.25m);

            Assert.Equal(-1.25m, r.ToDecimal());
            Assert.NotEqual(2.25m, r.ToDecimal());

            Assert.Equal(-5, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorDecimal5()
        {
            var r = new RationalNumber(0.0m);

            Assert.Equal(0.0m, r.ToDecimal());

            Assert.Equal(0, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorString1()
        {
            var r = new RationalNumber("1 / 4");

            Assert.Equal(0.25, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorString2()
        {
            var r = new RationalNumber("1 / 1 / 4");

            Assert.Equal(1.25, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(5, r.Numerator);
            Assert.Equal(4, r.Denominator);
        }

        [Fact]
        public void ConstructorString3()
        {
            var r = new RationalNumber(string.Empty);

            Assert.Equal(0.0, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(0, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorString4()
        {
            var r = new RationalNumber(" ");

            Assert.Equal(0.0, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(0, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorString5()
        {
            var r = new RationalNumber("4");

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(4, r.Numerator);
            Assert.Equal(1, r.Denominator);
        }

        [Fact]
        public void ConstructorString6()
        {
            Assert.Throws(typeof(ArgumentException), () => new RationalNumber("7/6/5/4"));
        }

        [Fact]
        public void ConstructorString7()
        {
            var r = new RationalNumber("-4 /5");

            Assert.Equal(-0.8, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(-4, r.Numerator);
            Assert.Equal(5, r.Denominator);
        }

        [Fact]
        public void ConstructorString8()
        {
            var r = new RationalNumber("1/-2");

            Assert.Equal(-0.5, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(-1, r.Numerator);
            Assert.Equal(2, r.Denominator);
        }

        [Fact]
        public void ConstructorString9()
        {
            var r = new RationalNumber("-1/-2");

            Assert.Equal(0.5, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());

            Assert.Equal(1, r.Numerator);
            Assert.Equal(2, r.Denominator);
        }

        [Fact]
        public void Parse1()
        {
            var r = RationalNumber.Parse("4");

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void TryParse1()
        {
            RationalNumber r;
            Assert.True(RationalNumber.TryParse("4", out r));

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void TryParse2()
        {
            RationalNumber r;
            Assert.False(RationalNumber.TryParse("Foo", out r));
        }

        [Fact]
        public void ImplicitLong1()
        {
            var r = (RationalNumber)4L;

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void Compare1()
        {
            var r = new RationalNumber(1);

            Assert.NotEqual(r, RationalNumber.NaN);
            Assert.Equal(r, RationalNumber.One);
            Assert.NotEqual(r, RationalNumber.Zero);
        }

        [Fact]
        public void Compare2()
        {
            var r = new RationalNumber(0);

            Assert.NotEqual(r, RationalNumber.NaN);
            Assert.NotEqual(r, RationalNumber.One);
            Assert.Equal(r, RationalNumber.Zero);
        }

        [Fact]
        public void Plus()
        {
            var r1 = new RationalNumber(1.5);
            var r2 = new RationalNumber(1.5);

            var r = RationalNumber.Add(r1, r2);

            Assert.Equal(3, r.ToDouble());
        }

        [Fact]
        public void Divide()
        {
            var r1 = new RationalNumber(3);
            var r2 = new RationalNumber(1.5);

            var r = RationalNumber.Divide(r1, r2);

            Assert.Equal(2, r.ToDouble());
        }

        [Fact]
        public void Equals1()
        {
            var r1 = new RationalNumber(1.236589215);
            var r2 = new RationalNumber(1.236589215);

            Assert.True(r1.Equals(r2));
            Assert.False(r1.Equals(RationalNumber.NaN));
        }

        [Fact]
        public void Equals2()
        {
            var r1 = new RationalNumber(1.23658921);
            var r2 = new RationalNumber(1.236589215);

            Assert.False(r1.Equals(r2));
            Assert.False(r1.Equals(RationalNumber.NaN));
        }

        [Fact]
        public void Equals3()
        {
            var r1 = new RationalNumber(1.236589215);
            var r2 = 1.236589215;

            Assert.True(r1.Equals(r2));
        }

        [Fact]
        public void Equals4()
        {
            var r1 = new RationalNumber(1.236589215);

            Assert.False(r1.Equals(null));
        }

        [Fact]
        public void Equals5()
        {
            var r1 = new RationalNumber(1.236589215);

            Assert.False(r1.Equals(DateTime.Now));
        }

        [Fact]
        public void Equals6()
        {
            var r1 = new RationalNumber(1.236589215);

            Assert.False(r1.Equals(null as object));
        }

        [Fact]
        public void Multiply()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            var r = RationalNumber.Multiply(r1, r2);

            Assert.Equal(8.75, r.ToDouble());
        }

        [Fact]
        public void Negate1()
        {
            var r1 = new RationalNumber(2.5);

            var r = RationalNumber.Negate(r1);

            Assert.Equal(-2.5, r.ToDouble());
        }

        [Fact]
        public void Negate2()
        {
            var r1 = new RationalNumber(2.5);

            var r = r1.Negate();

            Assert.Equal(-2.5, r.ToDouble());
        }

        [Fact]
        public void Reverse1()
        {
            var r1 = new RationalNumber(0.5);

            var r = RationalNumber.Reverse(r1);

            Assert.Equal(2, r.ToDouble());
        }

        [Fact]
        public void Reverse2()
        {
            var r1 = new RationalNumber(0.5);

            var r = r1.Reverse();

            Assert.Equal(2, r.ToDouble());
        }

        [Fact]
        public void Substract()
        {
            var r1 = new RationalNumber(0.75);
            var r2 = new RationalNumber(0.25);

            var r = RationalNumber.Subtract(r1, r2);

            Assert.Equal(0.5, r.ToDouble());
        }

        [Fact]
        public void ImplicitOperatorDecimal()
        {
            var r1 = new RationalNumber(4.3m);
            RationalNumber r2 = 4.3m;

            Assert.Equal(r1, r2);
        }

        [Fact]
        public void ImplicitOperatorLong()
        {
            var r1 = new RationalNumber(4);
            RationalNumber r2 = 4;

            Assert.Equal(r1, r2);
        }

        [Fact]
        public void ImplicitOperatorString()
        {
            var r1 = new RationalNumber(0.5);
            RationalNumber r2 = "1/2";

            Assert.Equal(r1, r2);
        }

        [Fact]
        public void OperatorSubtract()
        {
            var r1 = new RationalNumber(0.75);
            var r2 = new RationalNumber(0.25);

            var r = r1 - r2;

            Assert.Equal(0.5, r.ToDouble());
        }

        [Fact]
        public void OperatorNegate()
        {
            var r1 = new RationalNumber(2.5);

            var r = -r1;

            Assert.Equal(-2.5, r.ToDouble());
        }

        [Fact]
        public void OperatorDecrement()
        {
            var r1 = new RationalNumber(2.5);

            var r = --r1;

            Assert.Equal(1.5, r.ToDouble());
        }

        [Fact]
        public void OperatorNotEqual()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            Assert.True(r1 != r2);
        }

        [Fact]
        public void OperatorModulo()
        {
            var r1 = new RationalNumber(1);
            var r2 = new RationalNumber(2);

            var r = r1 % r2;

            Assert.Equal(1, r.ToDouble());
        }

        [Fact]
        public void OperatorMultiply()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            var r = r1 * r2;

            Assert.Equal(8.75, r.ToDouble());
        }

        [Fact]
        public void OperatorDivide()
        {
            var r1 = new RationalNumber(3);
            var r2 = new RationalNumber(1.5);

            var r = r1 / r2;

            Assert.Equal(2, r.ToDouble());
        }

        [Fact]
        public void OperatorPlus()
        {
            var r1 = new RationalNumber(1.5);
            var r2 = new RationalNumber(1.5);

            var r = r1 + r2;

            Assert.Equal(3, r.ToDouble());
        }

        [Fact]
        public void OperatorPlusAlone()
        {
            var r1 = new RationalNumber(2.5);

            var r = +r1;

            Assert.Equal(2.5, r.ToDouble());
        }

        [Fact]
        public void OperatorIncrement()
        {
            var r1 = new RationalNumber(2.5);

            var r = ++r1;

            Assert.Equal(3.5, r.ToDouble());
        }

        [Fact]
        public void OperatorLessThan()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);
            var r3 = r1;

            Assert.True(r1 < r2);
            Assert.False(r2 < r1);
            Assert.False(r1 < r3);
        }

        [Fact]
        public void OperatorLessThanOrEqual()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);
            var r3 = r1;

            Assert.True(r1 <= r2);
            Assert.False(r2 <= r1);
            Assert.True(r1 <= r3);
        }

        [Fact]
        public void OperatorEqual()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(2.5);

            Assert.True(r1 == r2);
        }

        [Fact]
        public void OperatorGreaterThan()
        {
            var r1 = new RationalNumber(3.5);
            var r2 = new RationalNumber(2.5);
            var r3 = r1;

            Assert.True(r1 > r2);
            Assert.False(r2 > r1);
            Assert.False(r1 > r3);
        }

        [Fact]
        public void OperatorGreaterThanOrEqual()
        {
            var r1 = new RationalNumber(3.5);
            var r2 = new RationalNumber(2.5);
            var r3 = r1;

            Assert.True(r1 >= r2);
            Assert.False(r2 >= r1);
            Assert.True(r1 >= r3);
        }

        [Fact]
        public void CompareToObject1()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = 2.5;

            Assert.True(r1.CompareTo(r2) == 0);
        }

        [Fact]
        public void CompareToObject2()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = 2.5m as object;

            var ex = Assert.Throws(typeof(InvalidCastException), () => r1.CompareTo(r2) == 0);
            Assert.Equal("The obj must be a RationalNumber or can be implicit converted to RationalNumber", ex.Message);
        }

        [Fact]
        public void CompareToRationalNumber()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(2.5);

            Assert.True(r1.CompareTo(r2) == 0);
        }

        [Fact]
        public void RationalNumberGetHashCode()
        {
            var r1 = new RationalNumber(2.5);

            Assert.Equal(r1.Numerator.GetHashCode() ^ r1.Denominator.GetHashCode(), r1.GetHashCode());
        }

        [Fact]
        public void ToDecimal()
        {
            var r1 = new RationalNumber(2.5);

            Assert.Equal(2.5m, r1.ToDecimal());
        }

        [Fact]
        public void ToSingle()
        {
            var r1 = new RationalNumber(2.5);

            Assert.Equal(2.5f, r1.ToSingle());
        }

        [Fact]
        public void ToString1()
        {
            var r1 = new RationalNumber(0.5);

            Assert.Equal("1 / 2", r1.ToString());
        }

        [Fact]
        public void ToString2()
        {
            var r1 = new RationalNumber(1.5);

            Assert.Equal("3 / 2", r1.ToString());
        }

        [Fact]
        public void ToStringBool1()
        {
            var r1 = new RationalNumber(1.5);

            Assert.Equal("3 / 2", r1.ToString(false));
        }

        [Fact]
        public void ToStringBool2()
        {
            var r1 = new RationalNumber(1.5);

            Assert.Equal("1 / 1 / 2", r1.ToString(true));
        }

        [Fact]
        public void ToStringBool3()
        {
            var r1 = new RationalNumber(0.5);

            Assert.Equal("1 / 2", r1.ToString(true));
        }

        [Fact]
        public void ToStringBool4()
        {
            var r1 = new RationalNumber(-40);

            Assert.Equal("-40 / 0 / 1", r1.ToString(true));
        }

        [Fact]
        public void ToStringBool5()
        {
            var r1 = new RationalNumber(-40.5);

            Assert.Equal("-40 / 1 / 2", r1.ToString(true));
        }

        [Fact]
        public void ConvertibleGetTypeCode()
        {
            var r1 = new RationalNumber(0.5) as IConvertible;

            Assert.True(r1.GetTypeCode() == TypeCode.Double);
        }

        [Fact]
        public void ConvertibleToBoolean1()
        {
            var r1 = new RationalNumber(1.5) as IConvertible;

            Assert.True(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToBoolean2()
        {
            var r1 = new RationalNumber(0.5) as IConvertible;

            Assert.False(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToByte()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToByte(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToChar()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Throws(typeof(NotSupportedException), () => r1.ToChar(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToDateTime()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Throws(typeof(NotSupportedException), () => r1.ToDateTime(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToDecimal()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToDecimal(CultureInfo.CurrentCulture), 1.3m);
        }

        [Fact]
        public void ConvertibleToDouble()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToDouble(CultureInfo.CurrentCulture), 1.3);
        }

        [Fact]
        public void ConvertibleToInt16()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToInt16(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToInt32()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToInt32(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToInt64()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToInt64(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToSByte()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToSByte(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToSingle()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToSingle(CultureInfo.CurrentCulture), 1.3f);
        }

        [Fact]
        public void ConvertibleToString()
        {
            var r1 = new RationalNumber(0.5) as IConvertible;

            Assert.Equal(r1.ToString(new CultureInfo("en-CA")), "1 / 2");
        }

        [Fact]
        public void ConvertibleToUInt16()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToUInt16(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToUInt32()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToUInt32(CultureInfo.CurrentCulture), (uint)1);
        }

        [Fact]
        public void ConvertibleToUInt64()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.Equal(r1.ToUInt64(CultureInfo.CurrentCulture), (ulong)1);
        }

        [Fact]
        public void IsInfinity()
        {
            Assert.True(RationalNumber.IsInfinity(RationalNumber.PositiveInfinity));
            Assert.True(RationalNumber.IsInfinity(RationalNumber.NegativeInfinity));
            Assert.True(RationalNumber.IsInfinity(double.PositiveInfinity));
            Assert.True(RationalNumber.IsInfinity(double.NegativeInfinity));
            Assert.False(RationalNumber.IsInfinity(2.3));
        }

        [Fact]
        public void IsPositiveInfinity()
        {
            Assert.True(RationalNumber.IsPositiveInfinity(RationalNumber.PositiveInfinity));
            Assert.False(RationalNumber.IsPositiveInfinity(RationalNumber.NegativeInfinity));
            Assert.True(RationalNumber.IsPositiveInfinity(double.PositiveInfinity));
            Assert.False(RationalNumber.IsPositiveInfinity(double.NegativeInfinity));
            Assert.False(RationalNumber.IsPositiveInfinity(2.3));
        }

        [Fact]
        public void IsNegativeInfinity()
        {
            Assert.False(RationalNumber.IsNegativeInfinity(RationalNumber.PositiveInfinity));
            Assert.True(RationalNumber.IsNegativeInfinity(RationalNumber.NegativeInfinity));
            Assert.False(RationalNumber.IsNegativeInfinity(double.PositiveInfinity));
            Assert.True(RationalNumber.IsNegativeInfinity(double.NegativeInfinity));
            Assert.False(RationalNumber.IsNegativeInfinity(2.3));
        }

        [Fact]
        public void IsNan()
        {
            Assert.False(RationalNumber.IsNaN(RationalNumber.PositiveInfinity));
            Assert.False(RationalNumber.IsNaN(RationalNumber.NegativeInfinity));
            Assert.False(RationalNumber.IsNaN(double.PositiveInfinity));
            Assert.False(RationalNumber.IsNaN(double.NegativeInfinity));
            Assert.False(RationalNumber.IsNaN(2.3));
            Assert.True(RationalNumber.IsNaN(double.NaN));
            Assert.True(RationalNumber.IsNaN(RationalNumber.NaN));
        }

        [Fact]
        public void IConvertibleToType()
        {
            var r1 = new RationalNumber(2) as IConvertible;

            Assert.Equal(r1.ToType(typeof(int), CultureInfo.CurrentCulture), 2);
        }
    }
}
