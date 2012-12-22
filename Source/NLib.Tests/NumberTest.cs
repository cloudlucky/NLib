namespace NLib.Tests
{
    using System;
    using System.Globalization;

    using NUnit.Framework;

    [TestFixture]
    public class NumberTest
    {
        [Test]
        public void ConstructorLong1()
        {
            var r = new Number(1);

            Assert.AreEqual(1, r.ToInt64());
            Assert.AreNotEqual(2, r.ToInt64());
        }

        [Test]
        public void ConstructorLong2()
        {
            var r = new Number(-3);

            Assert.AreEqual(-3, r.ToInt64());
            Assert.AreNotEqual(2, r.ToInt64());
        }

        [Test]
        public void ConstructorDouble1()
        {
            var r = new Number(0.25);

            Assert.AreEqual(0.25, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());
        }

        [Test]
        public void ConstructorDouble2()
        {
            var r = new Number(double.NaN);

            Assert.AreEqual(r, Number.NaN);
        }

        [Test]
        public void ConstructorDouble3()
        {
            var r = new Number(double.PositiveInfinity);

            Assert.AreEqual(r, Number.PositiveInfinity);
        }

        [Test]
        public void ConstructorDouble4()
        {
            var r = new Number(double.NegativeInfinity);

            Assert.AreEqual(r, Number.NegativeInfinity);
        }

        [Test]
        public void ConstructorDouble5()
        {
            var r = new Number(double.MaxValue);

            Assert.AreEqual(r, Number.MaxValue);
        }

        [Test]
        public void ConstructorDouble6()
        {
            var r = new Number(double.MinValue);

            Assert.AreEqual(r, Number.MinValue);
        }

        [Test]
        public void ConstructorDouble7()
        {
            var r = new Number(-1.25);

            Assert.AreEqual(-1.25, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());
        }

        [Test]
        public void ConstructorDouble8()
        {
            var r = new Number(0.0);

            Assert.AreEqual(0.0, r.ToDouble());
        }

        [Test]
        public void ConstructorDecimal1()
        {
            var r = new Number(0.25m);

            Assert.AreEqual(0.25, r.ToDecimal());
            Assert.AreNotEqual(2.25, r.ToDecimal());
        }

        [Test]
        public void ConstructorDecimal4()
        {
            var r = new Number(-1.25m);

            Assert.AreEqual(-1.25, r.ToDecimal());
            Assert.AreNotEqual(2.25, r.ToDecimal());
        }

        [Test]
        public void ConstructorDecimal5()
        {
            var r = new Number(0.0m);

            Assert.AreEqual(0.0, r.ToDecimal());
        }

        [Test]
        public void ConstructorString1()
        {
            var r = new Number("1");

            Assert.AreEqual(1, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());
        }

        [Test]
        public void ConstructorString3()
        {
            var r = new Number("4");

            Assert.AreEqual(4, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());
        }

        [Test]
        public void ConstructorString4()
        {
            var r = new Number("-4");

            Assert.AreEqual(-4, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());
        }

        [Test]
        public void Compare1()
        {
            var r = new Number(1);

            Assert.AreNotEqual(r, Number.NaN);
            Assert.AreEqual(r, Number.One);
            Assert.AreNotEqual(r, Number.Zero);
        }

        [Test]
        public void Compare2()
        {
            var r = new Number(0);

            Assert.AreNotEqual(r, Number.NaN);
            Assert.AreNotEqual(r, Number.One);
            Assert.AreEqual(r, Number.Zero);
        }

        [Test]
        public void Plus()
        {
            var r1 = new Number(1.5);
            var r2 = new Number(1.5);

            var r = Number.Add(r1, r2);

            Assert.AreEqual(3, r.ToDouble());
        }

        [Test]
        public void Divide()
        {
            var r1 = new Number(3);
            var r2 = new Number(1.5);

            var r = Number.Divide(r1, r2);

            Assert.AreEqual(2, r.ToDouble());
        }

        [Test]
        public void Equals1()
        {
            var r1 = new Number(1.236589215);
            var r2 = new Number(1.236589215);

            Assert.IsTrue(r1.Equals(r2));
            Assert.IsFalse(r1.Equals(Number.NaN));
        }

        [Test]
        public void Equals2()
        {
            var r1 = new Number(1.23658921);
            var r2 = new Number(1.236589215);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsFalse(r1.Equals(Number.NaN));
        }

        [Test]
        public void Equals3()
        {
            var r1 = new Number(1.236589215);
            var r2 = 1.236589215;

            Assert.IsTrue(r1.Equals(r2));
        }

        [Test]
        public void Equals5()
        {
            var r1 = new Number(1.236589215);

            Assert.IsFalse(r1.Equals(DateTime.Now));
        }

        [Test]
        public void Equals6()
        {
            var r1 = new Number(1.236589215);

            Assert.IsFalse(r1.Equals(null as object));
        }

        [Test]
        public void Multiply()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            var r = Number.Multiply(r1, r2);

            Assert.AreEqual(8.75, r.ToDouble());
        }

        [Test]
        public void Negate1()
        {
            var r1 = new Number(2.5);

            var r = Number.Negate(r1);

            Assert.AreEqual(-2.5, r.ToDouble());
        }

        [Test]
        public void Negate2()
        {
            var r1 = new Number(2.5);

            var r = r1.Negate();

            Assert.AreEqual(-2.5, r.ToDouble());
        }

        [Test]
        public void Substract()
        {
            var r1 = new Number(0.75);
            var r2 = new Number(0.25);

            var r = Number.Subtract(r1, r2);

            Assert.AreEqual(0.5, r.ToDouble());
        }

        [Test]
        public void ImplicitOperatorDecimal()
        {
            var r1 = new Number(4.3m);
            Number r2 = 4.3m;

            Assert.AreEqual(r1, r2);
        }

        [Test]
        public void ImplicitOperatorLong()
        {
            var r1 = new Number(4);
            Number r2 = 4;

            Assert.AreEqual(r1, r2);
        }

        [Test]
        public void ImplicitOperatorString()
        {
            var r1 = new Number(1);
            Number r2 = "1";

            Assert.AreEqual(r1, r2);
        }

        [Test]
        public void OperatorSubtract()
        {
            var r1 = new Number(0.75);
            var r2 = new Number(0.25);

            var r = r1 - r2;

            Assert.AreEqual(0.5, r.ToDouble());
        }

        [Test]
        public void OperatorNegate()
        {
            var r1 = new Number(2.5);

            var r = -r1;

            Assert.AreEqual(-2.5, r.ToDouble());
        }

        [Test]
        public void OperatorDecrement()
        {
            var r1 = new Number(2.5);

            var r = --r1;

            Assert.AreEqual(1.5, r.ToDouble());
        }

        [Test]
        public void OperatorNotEqual()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            Assert.IsTrue(r1 != r2);
        }

        [Test]
        public void OperatorModulo()
        {
            var r1 = new Number(1);
            var r2 = new Number(2);

            var r = r1 % r2;

            Assert.AreEqual(1, r.ToDouble());
        }

        [Test]
        public void OperatorMultiply()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            var r = r1 * r2;

            Assert.AreEqual(8.75, r.ToDouble());
        }

        [Test]
        public void OperatorDivide()
        {
            var r1 = new Number(3);
            var r2 = new Number(1.5);

            var r = r1 / r2;

            Assert.AreEqual(2, r.ToDouble());
        }

        [Test]
        public void OperatorPlus()
        {
            var r1 = new Number(1.5);
            var r2 = new Number(1.5);

            var r = r1 + r2;

            Assert.AreEqual(3, r.ToDouble());
        }

        [Test]
        public void OperatorPlusAlone()
        {
            var r1 = new Number(2.5);

            var r = +r1;

            Assert.AreEqual(2.5, r.ToDouble());
        }

        [Test]
        public void OperatorIncrement()
        {
            var r1 = new Number(2.5);

            var r = ++r1;

            Assert.AreEqual(3.5, r.ToDouble());
        }

        [Test]
        public void OperatorLessThan()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            Assert.IsTrue(r1 < r2);
            Assert.IsFalse(r2 < r1);
            Assert.IsFalse(r1 < r1);
        }

        [Test]
        public void OperatorLessThanOrEqual()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(3.5);

            Assert.IsTrue(r1 <= r2);
            Assert.IsFalse(r2 <= r1);
            Assert.IsTrue(r1 <= r1);
        }

        [Test]
        public void OperatorEqual()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(2.5);

            Assert.IsTrue(r1 == r2);
        }

        [Test]
        public void OperatorGreaterThan()
        {
            var r1 = new Number(3.5);
            var r2 = new Number(2.5);

            Assert.IsTrue(r1 > r2);
            Assert.IsFalse(r2 > r1);
            Assert.IsFalse(r1 > r1);
        }

        [Test]
        public void OperatorGreaterThanOrEqual()
        {
            var r1 = new Number(3.5);
            var r2 = new Number(2.5);

            Assert.IsTrue(r1 >= r2);
            Assert.IsFalse(r2 >= r1);
            Assert.IsTrue(r1 >= r1);
        }

        [Test]
        public void CompareToObject1()
        {
            var r1 = new Number(2.5);
            var r2 = 2.5;

            Assert.IsTrue(r1.CompareTo(r2) == 0);
        }

        [Test]
        [ExpectedException(typeof(InvalidCastException), ExpectedMessage = "The obj must be a Number or can be implicit converted to Number")]
        public void CompareToObject2()
        {
            var r1 = new Number(2.5);
            var r2 = 2.5m as object;

            Assert.IsTrue(r1.CompareTo(r2) == 0);
        }

        [Test]
        public void CompareToNumber()
        {
            var r1 = new Number(2.5);
            var r2 = new Number(2.5);

            Assert.IsTrue(r1.CompareTo(r2) == 0);
        }

        [Test]
        public void NumberGetHashCode()
        {
            var r1 = new Number(2.5);

            Assert.AreEqual((2.5).GetHashCode(), r1.GetHashCode());
        }

        [Test]
        public void ToDecimal()
        {
            var r1 = new Number(2.5);

            Assert.AreEqual(2.5m, r1.ToDecimal());
        }

        [Test]
        public void ToSingle()
        {
            var r1 = new Number(2.5);

            Assert.AreEqual(2.5f, r1.ToSingle());
        }

        [Test]
        public void ToString1()
        {
            var r1 = new Number(0.5);

            Assert.AreEqual("0.5", r1.ToString(new CultureInfo("en-CA")));
        }

        [Test]
        public void ToString2()
        {
            var r1 = new Number(1.5);

            Assert.AreEqual("1.5", r1.ToString(new CultureInfo("en-CA")));
        }

        [Test]
        public void ConvertibleGetTypeCode()
        {
            var r1 = new Number(0.5) as IConvertible;

            Assert.IsTrue(r1.GetTypeCode() == TypeCode.Double);
        }

        [Test]
        public void ConvertibleToBoolean1()
        {
            var r1 = new Number(1.5) as IConvertible;

            Assert.IsTrue(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [Test]
        public void ConvertibleToBoolean2()
        {
            var r1 = new Number(0.5) as IConvertible;

            Assert.IsTrue(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [Test]
        public void ConvertibleToByte()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToByte(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(InvalidCastException))]
        public void ConvertibleToChar()
        {
            var r1 = new Number(1.3) as IConvertible;

            r1.ToChar(CultureInfo.CurrentCulture);
        }

        [Test]
        [ExpectedException(ExpectedException = typeof(InvalidCastException))]
        public void ConvertibleToDateTime()
        {
            var r1 = new Number(1.3) as IConvertible;

            r1.ToDateTime(CultureInfo.CurrentCulture);
        }

        [Test]
        public void ConvertibleToDecimal()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToDecimal(CultureInfo.CurrentCulture), 1.3m);
        }

        [Test]
        public void ConvertibleToDouble()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToDouble(CultureInfo.CurrentCulture), 1.3m);
        }

        [Test]
        public void ConvertibleToInt16()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToInt16(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        public void ConvertibleToInt32()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToInt32(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        public void ConvertibleToInt64()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToInt64(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        public void ConvertibleToSByte()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToSByte(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        public void ConvertibleToSingle()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToSingle(CultureInfo.CurrentCulture), 1.3f);
        }

        [Test]
        public void ConvertibleToString()
        {
            var r1 = new Number(0.5) as IConvertible;

            Assert.AreEqual(r1.ToString(new CultureInfo("en-CA")), "0.5");
        }

        [Test]
        public void ConvertibleToUInt16()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToUInt16(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        public void ConvertibleToUInt32()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToUInt32(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        public void ConvertibleToUInt64()
        {
            var r1 = new Number(1.3) as IConvertible;

            Assert.AreEqual(r1.ToUInt64(CultureInfo.CurrentCulture), 1);
        }

        [Test]
        public void IsInfinity()
        {
            Assert.IsTrue(Number.IsInfinity(Number.PositiveInfinity));
            Assert.IsTrue(Number.IsInfinity(Number.NegativeInfinity));
            Assert.IsTrue(Number.IsInfinity(double.PositiveInfinity));
            Assert.IsTrue(Number.IsInfinity(double.NegativeInfinity));
            Assert.IsFalse(Number.IsInfinity(2.3));
        }

        [Test]
        public void IsPositiveInfinity()
        {
            Assert.IsTrue(Number.IsPositiveInfinity(Number.PositiveInfinity));
            Assert.IsFalse(Number.IsPositiveInfinity(Number.NegativeInfinity));
            Assert.IsTrue(Number.IsPositiveInfinity(double.PositiveInfinity));
            Assert.IsFalse(Number.IsPositiveInfinity(double.NegativeInfinity));
            Assert.IsFalse(Number.IsPositiveInfinity(2.3));
        }

        [Test]
        public void IsNegativeInfinity()
        {
            Assert.IsFalse(Number.IsNegativeInfinity(Number.PositiveInfinity));
            Assert.IsTrue(Number.IsNegativeInfinity(Number.NegativeInfinity));
            Assert.IsFalse(Number.IsNegativeInfinity(double.PositiveInfinity));
            Assert.IsTrue(Number.IsNegativeInfinity(double.NegativeInfinity));
            Assert.IsFalse(Number.IsNegativeInfinity(2.3));
        }

        [Test]
        public void IsNan()
        {
            Assert.IsFalse(Number.IsNaN(Number.PositiveInfinity));
            Assert.IsFalse(Number.IsNaN(Number.NegativeInfinity));
            Assert.IsFalse(Number.IsNaN(double.PositiveInfinity));
            Assert.IsFalse(Number.IsNaN(double.NegativeInfinity));
            Assert.IsFalse(Number.IsNaN(2.3));
            Assert.IsTrue(Number.IsNaN(double.NaN));
            Assert.IsTrue(Number.IsNaN(Number.NaN));
        }

        [Test]
        public void IConvertibleToType()
        {
            var r1 = new Number(2) as IConvertible;

            Assert.AreEqual(r1.ToType(typeof(int), CultureInfo.CurrentCulture), 2);
        }
    }
}
