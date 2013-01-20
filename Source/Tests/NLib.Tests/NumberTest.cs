namespace NLib.Tests
{
    using System;
    using System.Globalization;
    using System.Threading;

    using Xunit;

    public class NumberTest
    {
        [Fact]
        public void ConstructorLong1()
        {
            var r = new Number(1L);

            Assert.Equal(1, r.ToInt64());
            Assert.NotEqual(2, r.ToInt64());
        }

        [Fact]
        public void ConstructorLong2()
        {
            var r = new Number(-3L);

            Assert.Equal(-3, r.ToInt64());
            Assert.NotEqual(2, r.ToInt64());
        }

        [Fact]
        public void ConstructorUnsignedLong1()
        {
            var r = new Number(1UL);

            Assert.Equal(1, r.ToInt64());
            Assert.NotEqual(2, r.ToInt64());
        }

        [Fact]
        public void ConstructorDouble1()
        {
            var r = new Number(0.25);

            Assert.Equal(0.25, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void ConstructorDouble2()
        {
            var r = new Number(double.NaN);

            Assert.Equal(r, Number.NaN);
        }

        [Fact]
        public void ConstructorDouble3()
        {
            var r = new Number(double.PositiveInfinity);

            Assert.Equal(r, Number.PositiveInfinity);
        }

        [Fact]
        public void ConstructorDouble4()
        {
            var r = new Number(double.NegativeInfinity);

            Assert.Equal(r, Number.NegativeInfinity);
        }

        [Fact]
        public void ConstructorDouble5()
        {
            var r = new Number(double.MaxValue);

            Assert.Equal(r, Number.MaxValue);
        }

        [Fact]
        public void ConstructorDouble6()
        {
            var r = new Number(double.MinValue);

            Assert.Equal(r, Number.MinValue);
        }

        [Fact]
        public void ConstructorDouble7()
        {
            var r = new Number(-1.25);

            Assert.Equal(-1.25, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void ConstructorDouble8()
        {
            var r = new Number(0.0);

            Assert.Equal(0.0, r.ToDouble());
        }

        [Fact]
        public void ConstructorDecimal1()
        {
            var r = new Number(0.25m);

            Assert.Equal(0.25m, r.ToDecimal());
            Assert.NotEqual(2.25m, r.ToDecimal());
        }

        [Fact]
        public void ConstructorDecimal4()
        {
            var r = new Number(-1.25m);

            Assert.Equal(-1.25m, r.ToDecimal());
            Assert.NotEqual(2.25m, r.ToDecimal());
        }

        [Fact]
        public void ConstructorDecimal5()
        {
            var r = new Number(0.0m);

            Assert.Equal(0.0m, r.ToDecimal());
        }

        [Fact]
        public void ConstructorString1()
        {
            var r = new Number("1");

            Assert.Equal(1, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void ConstructorString3()
        {
            var r = new Number("4");

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void ConstructorString4()
        {
            var r = new Number("-4");

            Assert.Equal(-4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void Parse1()
        {
            var r = Number.Parse("4");

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void TryParse1()
        {
            Number r;
            Assert.True(Number.TryParse("4", out r));

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void TryParse2()
        {
            Number r;
            Assert.False(Number.TryParse("Foo", out r));
        }

        [Fact]
        public void ImplicitLong1()
        {
            var r = (Number)4L;

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void ImplicitUnsignedLong1()
        {
            var r = (Number)4UL;

            Assert.Equal(4, r.ToDouble());
            Assert.NotEqual(2.25, r.ToDouble());
        }

        [Fact]
        public void Compare1()
        {
            var r = new Number(1);

            Assert.NotEqual(r, Number.NaN);
            Assert.Equal(r, Number.One);
            Assert.NotEqual(r, Number.Zero);
        }

        [Fact]
        public void Compare2()
        {
            var r = new Number(0);

            Assert.NotEqual(r, Number.NaN);
            Assert.NotEqual(r, Number.One);
            Assert.Equal(r, Number.Zero);
        }

        [Fact]
        public void Plus()
        {
            var r1 = new Number(1.5);
            var r2 = new Number(1.5);

            var r = Number.Add(r1, r2);

            Assert.Equal(3, r.ToDouble());
        }

        [Fact]
        public void Divide()
        {
            var r1 = new Number(3);
            var r2 = new Number(1.5);

            var r = Number.Divide(r1, r2);

            Assert.Equal(2, r.ToDouble());
        }

        [Fact]
        public void Equals1()
        {
            var r1 = new Number(1.236589215);
            var r2 = new Number(1.236589215);

            Assert.True(r1.Equals(r2));
            Assert.False(r1.Equals(Number.NaN));
        }

        [Fact]
        public void Equals2()
        {
            var r1 = new Number(1.23658921);
            var r2 = new Number(1.236589215);

            Assert.False(r1.Equals(r2));
            Assert.False(r1.Equals(Number.NaN));
        }

        [Fact]
        public void Equals3()
        {
            var r1 = new Number(1.236589215);
            var r2 = 1.236589215;

            Assert.True(r1.Equals(r2));
        }

        [Fact]
        public void Equals5()
        {
            var r1 = new Number(1.236589215);

            Assert.False(r1.Equals(DateTime.Now));
        }

        [Fact]
        public void Equals6()
        {
            var r1 = new Number(1.236589215);

            Assert.False(r1.Equals(null as object));
        }

        [Fact]
        public void Multiply()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            var r = Number.Multiply(r1, r2);

            Assert.Equal(8.75, r.ToDouble());
        }

        [Fact]
        public void Negate1()
        {
            var r1 = new Number(2.5);

            var r = Number.Negate(r1);

            Assert.Equal(-2.5, r.ToDouble());
        }

        [Fact]
        public void Negate2()
        {
            var r1 = new Number(2.5);

            var r = r1.Negate();

            Assert.Equal(-2.5, r.ToDouble());
        }

        [Fact]
        public void Substract()
        {
            var r1 = new Number(0.75);
            var r2 = new Number(0.25);

            var r = Number.Subtract(r1, r2);

            Assert.Equal(0.5, r.ToDouble());
        }

        [Fact]
        public void ImplicitOperatorDecimal()
        {
            var r1 = new Number(4.3m);
            Number r2 = 4.3m;

            Assert.Equal(r1, r2);
        }

        [Fact]
        public void ImplicitOperatorLong()
        {
            var r1 = new Number(4);
            Number r2 = 4;

            Assert.Equal(r1, r2);
        }

        [Fact]
        public void ImplicitOperatorString()
        {
            var r1 = new Number(1);
            Number r2 = "1";

            Assert.Equal(r1, r2);
        }

        [Fact]
        public void OperatorSubtract()
        {
            var r1 = new Number(0.75);
            var r2 = new Number(0.25);

            var r = r1 - r2;

            Assert.Equal(0.5, r.ToDouble());
        }

        [Fact]
        public void OperatorNegate()
        {
            var r1 = new Number(2.5);

            var r = -r1;

            Assert.Equal(-2.5, r.ToDouble());
        }

        [Fact]
        public void OperatorDecrement()
        {
            var r1 = new Number(2.5);

            var r = --r1;

            Assert.Equal(1.5, r.ToDouble());
        }

        [Fact]
        public void OperatorNotEqual()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            Assert.True(r1 != r2);
        }

        [Fact]
        public void OperatorModulo()
        {
            var r1 = new Number(1);
            var r2 = new Number(2);

            var r = r1 % r2;

            Assert.Equal(1, r.ToDouble());
        }

        [Fact]
        public void OperatorMultiply()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            var r = r1 * r2;

            Assert.Equal(8.75, r.ToDouble());
        }

        [Fact]
        public void OperatorDivide()
        {
            var r1 = new Number(3);
            var r2 = new Number(1.5);

            var r = r1 / r2;

            Assert.Equal(2, r.ToDouble());
        }

        [Fact]
        public void OperatorPlus()
        {
            var r1 = new Number(1.5);
            var r2 = new Number(1.5);

            var r = r1 + r2;

            Assert.Equal(3, r.ToDouble());
        }

        [Fact]
        public void OperatorPlusAlone()
        {
            var r1 = new Number(2.5);

            var r = +r1;

            Assert.Equal(2.5, r.ToDouble());
        }

        [Fact]
        public void OperatorIncrement()
        {
            var r1 = new Number(2.5);

            var r = ++r1;

            Assert.Equal(3.5, r.ToDouble());
        }

        [Fact]
        public void OperatorLessThan()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            Assert.True(r1 < r2);
            Assert.False(r2 < r1);
        }

        [Fact]
        public void OperatorLessThanOrEqual()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            Assert.True(r1 <= r2);
            Assert.False(r2 <= r1);
        }

        [Fact]
        public void OperatorEqual()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(2.5);

            Assert.True(r1 == r2);
        }

        [Fact]
        public void OperatorGreaterThan()
        {
            var r1 = new Number(3.5);
            var r2 = new Number(2.5);

            Assert.True(r1 > r2);
            Assert.False(r2 > r1);
        }

        [Fact]
        public void OperatorGreaterThanOrEqual()
        {
            var r1 = new Number(3.5);
            var r2 = new Number(2.5);

            Assert.True(r1 >= r2);
            Assert.False(r2 >= r1);
        }

        [Fact]
        public void CompareToObject1()
        {
            var r1 = new Number(2.5);
            var r2 = 2.5;

            Assert.True(r1.CompareTo(r2) == 0);
        }

        [Fact]
        public void CompareToObject2()
        {
            var r1 = new Number(2.5);
            var r2 = 2.5m as object;

            var ex = Assert.Throws<InvalidCastException>(() => r1.CompareTo(r2) == 0);
            Assert.Equal("The obj must be a Number or can be implicit converted to Number", ex.Message);
        }

        [Fact]
        public void CompareToNumber()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(2.5);

            Assert.True(r1.CompareTo(r2) == 0);
        }

        [Fact]
        public void NumberGetHashCode()
        {
            var r1 = new Number(2.5);

            Assert.Equal((2.5).GetHashCode(), r1.GetHashCode());
        }

        [Fact]
        public void ToDecimal()
        {
            var r1 = new Number(2.5);

            Assert.Equal(2.5m, r1.ToDecimal());
        }

        [Fact]
        public void ToSingle()
        {
            var r1 = new Number(2.5);

            Assert.Equal(2.5f, r1.ToSingle());
        }

        [Fact]
        public void ToString1()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");

            var r1 = new Number(0.5);

            Assert.Equal("0.5", r1.ToString());
        }

        [Fact]
        public void ToString2()
        {
            var r1 = new Number(1.5);

            Assert.Equal("1.5", r1.ToString(new CultureInfo("en-CA")));
        }

        [Fact]
        public void ToString3()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-CA");

            var r1 = new Number(1.5);

            Assert.Equal("1.5", r1.ToString("#.#"));
        }

        [Fact]
        public void ToString4()
        {
            var r1 = new Number(1.5);

            Assert.Equal("1.5", r1.ToString("#.#", new CultureInfo("en-CA")));
        }

        [Fact]
        public void ConvertibleGetTypeCode()
        {
            var r1 = new Number(0.5) as IConvertible;

            Assert.True(r1.GetTypeCode() == TypeCode.Double);
        }

        [Fact]
        public void ConvertibleToBoolean1()
        {
            var r1 = new Number(1.5) as IConvertible;

            Assert.True(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToBoolean2()
        {
            var r1 = new Number(0.5) as IConvertible;

            Assert.True(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToByte()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToByte(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToChar()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Throws<InvalidCastException>(() => r1.ToChar(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToDateTime()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Throws<InvalidCastException>(() => r1.ToDateTime(CultureInfo.CurrentCulture));
        }

        [Fact]
        public void ConvertibleToDecimal()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToDecimal(CultureInfo.CurrentCulture), 1.3m);
        }

        [Fact]
        public void ConvertibleToDouble()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToDouble(CultureInfo.CurrentCulture), 1.3);
        }

        [Fact]
        public void ConvertibleToInt16()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToInt16(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToInt32()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToInt32(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToInt64()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToInt64(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToSByte()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToSByte(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToSingle()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToSingle(CultureInfo.CurrentCulture), 1.3f);
        }

        [Fact]
        public void ConvertibleToString()
        {
            var r1 = new Number(0.5) as IConvertible;

            Assert.Equal(r1.ToString(new CultureInfo("en-CA")), "0.5");
        }

        [Fact]
        public void ConvertibleToUInt16()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToUInt16(CultureInfo.CurrentCulture), 1);
        }

        [Fact]
        public void ConvertibleToUInt32()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToUInt32(CultureInfo.CurrentCulture), (uint)1);
        }

        [Fact]
        public void ConvertibleToUInt64()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.Equal(r1.ToUInt64(CultureInfo.CurrentCulture), (ulong)1);
        }

        [Fact]
        public void IsInfinity()
        {
            Assert.True(Number.IsInfinity(Number.PositiveInfinity));
            Assert.True(Number.IsInfinity(Number.NegativeInfinity));
            Assert.True(Number.IsInfinity(double.PositiveInfinity));
            Assert.True(Number.IsInfinity(double.NegativeInfinity));
            Assert.False(Number.IsInfinity(2.3));
        }

        [Fact]
        public void IsPositiveInfinity()
        {
            Assert.True(Number.IsPositiveInfinity(Number.PositiveInfinity));
            Assert.False(Number.IsPositiveInfinity(Number.NegativeInfinity));
            Assert.True(Number.IsPositiveInfinity(double.PositiveInfinity));
            Assert.False(Number.IsPositiveInfinity(double.NegativeInfinity));
            Assert.False(Number.IsPositiveInfinity(2.3));
        }

        [Fact]
        public void IsNegativeInfinity()
        {
            Assert.False(Number.IsNegativeInfinity(Number.PositiveInfinity));
            Assert.True(Number.IsNegativeInfinity(Number.NegativeInfinity));
            Assert.False(Number.IsNegativeInfinity(double.PositiveInfinity));
            Assert.True(Number.IsNegativeInfinity(double.NegativeInfinity));
            Assert.False(Number.IsNegativeInfinity(2.3));
        }

        [Fact]
        public void IsNan()
        {
            Assert.False(Number.IsNaN(Number.PositiveInfinity));
            Assert.False(Number.IsNaN(Number.NegativeInfinity));
            Assert.False(Number.IsNaN(double.PositiveInfinity));
            Assert.False(Number.IsNaN(double.NegativeInfinity));
            Assert.False(Number.IsNaN(2.3));
            Assert.True(Number.IsNaN(double.NaN));
            Assert.True(Number.IsNaN(Number.NaN));
        }

        [Fact]
        public void IConvertibleToType()
        {
            var r1 = new Number(2) as IConvertible;

            Assert.Equal(r1.ToType(typeof(int), CultureInfo.CurrentCulture), 2);
        }
    }
}
