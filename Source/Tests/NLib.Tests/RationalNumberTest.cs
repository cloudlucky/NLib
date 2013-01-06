namespace NLib.Tests
{
    using System;
    using System.Globalization;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RationalNumberTest
    {
        [TestMethod]
        public void ConstructorLong1()
        {
            var r = new RationalNumber(1);

            Assert.AreEqual(1, r.ToInt64());
            Assert.AreNotEqual(2, r.ToInt64());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        public void ConstructorLong2()
        {
            var r = new RationalNumber(-3);

            Assert.AreEqual(-3, r.ToInt64());
            Assert.AreNotEqual(2, r.ToInt64());

            Assert.AreEqual(-3, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        public void ConstructorLongLong1()
        {
            var r = new RationalNumber(1, 1);

            Assert.AreEqual(1, r.ToInt64());
            Assert.AreNotEqual(2, r.ToInt64());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        public void ConstructorLongLong2()
        {
            var r = new RationalNumber(3, 4);

            Assert.AreEqual(0.75, r.ToDouble());
            Assert.AreNotEqual(2, r.ToDouble());

            Assert.AreEqual(3, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorLongLong3()
        {
            var r = new RationalNumber(5, 4);

            Assert.AreEqual(1.25, r.ToDouble());
            Assert.AreNotEqual(2, r.ToDouble());

            Assert.AreEqual(5, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorLongLong4()
        {
            var r = new RationalNumber(-2, 5);

            Assert.AreEqual(-0.4, r.ToDouble());
            Assert.AreNotEqual(2, r.ToDouble());

            Assert.AreEqual(-2, r.Numerator);
            Assert.AreEqual(5, r.Denominator);
        }

        [TestMethod]
        public void ConstructorLongLong5()
        {
            var r = new RationalNumber(4, 8);

            Assert.AreEqual(0.5, r.ToDouble());
            Assert.AreNotEqual(2, r.ToDouble());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(2, r.Denominator);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void ConstructorLongLong6()
        {
            var r = new RationalNumber(4, 0);
        }

        [TestMethod]
        public void ConstructorLongLong7()
        {
            var r = new RationalNumber(1, -2);

            Assert.AreEqual(-0.5, r.ToDouble());
            Assert.AreNotEqual(2, r.ToDouble());

            Assert.AreEqual(-1, r.Numerator);
            Assert.AreEqual(2, r.Denominator);
        }

        [TestMethod]
        public void ConstructorLongLong8()
        {
            var r = new RationalNumber(-1, -2);

            Assert.AreEqual(0.5, r.ToDouble());
            Assert.AreNotEqual(2, r.ToDouble());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(2, r.Denominator);
        }

        [TestMethod]
        public void ConstructorDouble1()
        {
            var r = new RationalNumber(0.25);

            Assert.AreEqual(0.25, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorDouble2()
        {
            var r = new RationalNumber(double.NaN);

            Assert.AreEqual(r, RationalNumber.NaN);
        }

        [TestMethod]
        public void ConstructorDouble3()
        {
            var r = new RationalNumber(double.PositiveInfinity);

            Assert.AreEqual(r, RationalNumber.PositiveInfinity);
        }

        [TestMethod]
        public void ConstructorDouble4()
        {
            var r = new RationalNumber(double.NegativeInfinity);

            Assert.AreEqual(r, RationalNumber.NegativeInfinity);
        }

        [TestMethod]
        public void ConstructorDouble5()
        {
            var r = new RationalNumber(double.MaxValue);

            Assert.AreEqual(r, RationalNumber.MaxValue);
        }

        [TestMethod]
        public void ConstructorDouble6()
        {
            var r = new RationalNumber(double.MinValue);

            Assert.AreEqual(r, RationalNumber.MinValue);
        }

        [TestMethod]
        public void ConstructorDouble7()
        {
            var r = new RationalNumber(-1.25);

            Assert.AreEqual(-1.25, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(-5, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorDouble8()
        {
            var r = new RationalNumber(0.0);

            Assert.AreEqual(0.0, r.ToDouble());

            Assert.AreEqual(0, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        public void ConstructorDecimal1()
        {
            var r = new RationalNumber(0.25m);

            Assert.AreEqual(0.25m, r.ToDecimal());
            Assert.AreNotEqual(2.25m, r.ToDecimal());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorDecimal2()
        {
            var r = new RationalNumber(decimal.MaxValue);

            Assert.AreEqual(r, RationalNumber.PositiveInfinity);
        }

        [TestMethod]
        public void ConstructorDecimal3()
        {
            var r = new RationalNumber(decimal.MinValue);

            Assert.AreEqual(r, RationalNumber.PositiveInfinity);
        }

        [TestMethod]
        public void ConstructorDecimal4()
        {
            var r = new RationalNumber(-1.25m);

            Assert.AreEqual(-1.25m, r.ToDecimal());
            Assert.AreNotEqual(2.25m, r.ToDecimal());

            Assert.AreEqual(-5, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorDecimal5()
        {
            var r = new RationalNumber(0.0m);

            Assert.AreEqual(0.0m, r.ToDecimal());

            Assert.AreEqual(0, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        public void ConstructorString1()
        {
            var r = new RationalNumber("1 / 4");

            Assert.AreEqual(0.25, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorString2()
        {
            var r = new RationalNumber("1 / 1 / 4");

            Assert.AreEqual(1.25, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(5, r.Numerator);
            Assert.AreEqual(4, r.Denominator);
        }

        [TestMethod]
        public void ConstructorString3()
        {
            var r = new RationalNumber(string.Empty);

            Assert.AreEqual(0.0, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(0, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        public void ConstructorString4()
        {
            var r = new RationalNumber(" ");

            Assert.AreEqual(0.0, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(0, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        public void ConstructorString5()
        {
            var r = new RationalNumber("4");

            Assert.AreEqual(4, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(4, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void ConstructorString6()
        {
            var r = new RationalNumber("7/6/5/4");
        }

        [TestMethod]
        public void ConstructorString7()
        {
            var r = new RationalNumber("-4 /5");

            Assert.AreEqual(-0.8, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(-4, r.Numerator);
            Assert.AreEqual(5, r.Denominator);
        }

        [TestMethod]
        public void ConstructorString8()
        {
            var r = new RationalNumber("1/-2");

            Assert.AreEqual(-0.5, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(-1, r.Numerator);
            Assert.AreEqual(2, r.Denominator);
        }

        [TestMethod]
        public void ConstructorString9()
        {
            var r = new RationalNumber("-1/-2");

            Assert.AreEqual(0.5, r.ToDouble());
            Assert.AreNotEqual(2.25, r.ToDouble());

            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(2, r.Denominator);
        }

        [TestMethod]
        public void Compare1()
        {
            var r = new RationalNumber(1);

            Assert.AreNotEqual(r, RationalNumber.NaN);
            Assert.AreEqual(r, RationalNumber.One);
            Assert.AreNotEqual(r, RationalNumber.Zero);
        }

        [TestMethod]
        public void Compare2()
        {
            var r = new RationalNumber(0);

            Assert.AreNotEqual(r, RationalNumber.NaN);
            Assert.AreNotEqual(r, RationalNumber.One);
            Assert.AreEqual(r, RationalNumber.Zero);
        }

        [TestMethod]
        public void Plus()
        {
            var r1 = new RationalNumber(1.5);
            var r2 = new RationalNumber(1.5);

            var r = RationalNumber.Add(r1, r2);

            Assert.AreEqual(3, r.ToDouble());
        }

        [TestMethod]
        public void Divide()
        {
            var r1 = new RationalNumber(3);
            var r2 = new RationalNumber(1.5);

            var r = RationalNumber.Divide(r1, r2);

            Assert.AreEqual(2, r.ToDouble());
        }

        [TestMethod]
        public void Equals1()
        {
            var r1 = new RationalNumber(1.236589215);
            var r2 = new RationalNumber(1.236589215);

            Assert.IsTrue(r1.Equals(r2));
            Assert.IsFalse(r1.Equals(RationalNumber.NaN));
        }

        [TestMethod]
        public void Equals2()
        {
            var r1 = new RationalNumber(1.23658921);
            var r2 = new RationalNumber(1.236589215);

            Assert.IsFalse(r1.Equals(r2));
            Assert.IsFalse(r1.Equals(RationalNumber.NaN));
        }

        [TestMethod]
        public void Equals3()
        {
            var r1 = new RationalNumber(1.236589215);
            var r2 = 1.236589215;

            Assert.IsTrue(r1.Equals(r2));
        }

        [TestMethod]
        public void Equals4()
        {
            var r1 = new RationalNumber(1.236589215);

            Assert.IsFalse(r1.Equals(null));
        }

        [TestMethod]
        public void Equals5()
        {
            var r1 = new RationalNumber(1.236589215);

            Assert.IsFalse(r1.Equals(DateTime.Now));
        }

        [TestMethod]
        public void Equals6()
        {
            var r1 = new RationalNumber(1.236589215);

            Assert.IsFalse(r1.Equals(null as object));
        }

        [TestMethod]
        public void Multiply()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            var r = RationalNumber.Multiply(r1, r2);

            Assert.AreEqual(8.75, r.ToDouble());
        }

        [TestMethod]
        public void Negate1()
        {
            var r1 = new RationalNumber(2.5);

            var r = RationalNumber.Negate(r1);

            Assert.AreEqual(-2.5, r.ToDouble());
        }

        [TestMethod]
        public void Negate2()
        {
            var r1 = new RationalNumber(2.5);

            var r = r1.Negate();

            Assert.AreEqual(-2.5, r.ToDouble());
        }

        [TestMethod]
        public void Reverse1()
        {
            var r1 = new RationalNumber(0.5);

            var r = RationalNumber.Reverse(r1);

            Assert.AreEqual(2, r.ToDouble());
        }

        [TestMethod]
        public void Reverse2()
        {
            var r1 = new RationalNumber(0.5);

            var r = r1.Reverse();

            Assert.AreEqual(2, r.ToDouble());
        }

        [TestMethod]
        public void Substract()
        {
            var r1 = new RationalNumber(0.75);
            var r2 = new RationalNumber(0.25);

            var r = RationalNumber.Subtract(r1, r2);

            Assert.AreEqual(0.5, r.ToDouble());
        }

        [TestMethod]
        public void ImplicitOperatorDecimal()
        {
            var r1 = new RationalNumber(4.3m);
            RationalNumber r2 = 4.3m;

            Assert.AreEqual(r1, r2);
        }

        [TestMethod]
        public void ImplicitOperatorLong()
        {
            var r1 = new RationalNumber(4);
            RationalNumber r2 = 4;

            Assert.AreEqual(r1, r2);
        }

        [TestMethod]
        public void ImplicitOperatorString()
        {
            var r1 = new RationalNumber(0.5);
            RationalNumber r2 = "1/2";

            Assert.AreEqual(r1, r2);
        }

        [TestMethod]
        public void OperatorSubtract()
        {
            var r1 = new RationalNumber(0.75);
            var r2 = new RationalNumber(0.25);

            var r = r1 - r2;

            Assert.AreEqual(0.5, r.ToDouble());
        }

        [TestMethod]
        public void OperatorNegate()
        {
            var r1 = new RationalNumber(2.5);

            var r = -r1;

            Assert.AreEqual(-2.5, r.ToDouble());
        }

        [TestMethod]
        public void OperatorDecrement()
        {
            var r1 = new RationalNumber(2.5);

            var r = --r1;

            Assert.AreEqual(1.5, r.ToDouble());
        }

        [TestMethod]
        public void OperatorNotEqual()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            Assert.IsTrue(r1 != r2);
        }

        [TestMethod]
        public void OperatorModulo()
        {
            var r1 = new RationalNumber(1);
            var r2 = new RationalNumber(2);

            var r = r1 % r2;

            Assert.AreEqual(1, r.ToDouble());
        }

        [TestMethod]
        public void OperatorMultiply()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            var r = r1 * r2;

            Assert.AreEqual(8.75, r.ToDouble());
        }

        [TestMethod]
        public void OperatorDivide()
        {
            var r1 = new RationalNumber(3);
            var r2 = new RationalNumber(1.5);

            var r = r1 / r2;

            Assert.AreEqual(2, r.ToDouble());
        }

        [TestMethod]
        public void OperatorPlus()
        {
            var r1 = new RationalNumber(1.5);
            var r2 = new RationalNumber(1.5);

            var r = r1 + r2;

            Assert.AreEqual(3, r.ToDouble());
        }

        [TestMethod]
        public void OperatorPlusAlone()
        {
            var r1 = new RationalNumber(2.5);

            var r = +r1;

            Assert.AreEqual(2.5, r.ToDouble());
        }

        [TestMethod]
        public void OperatorIncrement()
        {
            var r1 = new RationalNumber(2.5);

            var r = ++r1;

            Assert.AreEqual(3.5, r.ToDouble());
        }

        [TestMethod]
        public void OperatorLessThan()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            Assert.IsTrue(r1 < r2);
            Assert.IsFalse(r2 < r1);
            Assert.IsFalse(r1 < r1);
        }

        [TestMethod]
        public void OperatorLessThanOrEqual()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(3.5);

            Assert.IsTrue(r1 <= r2);
            Assert.IsFalse(r2 <= r1);
            Assert.IsTrue(r1 <= r1);
        }

        [TestMethod]
        public void OperatorEqual()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(2.5);

            Assert.IsTrue(r1 == r2);
        }

        [TestMethod]
        public void OperatorGreaterThan()
        {
            var r1 = new RationalNumber(3.5);
            var r2 = new RationalNumber(2.5);

            Assert.IsTrue(r1 > r2);
            Assert.IsFalse(r2 > r1);
            Assert.IsFalse(r1 > r1);
        }

        [TestMethod]
        public void OperatorGreaterThanOrEqual()
        {
            var r1 = new RationalNumber(3.5);
            var r2 = new RationalNumber(2.5);

            Assert.IsTrue(r1 >= r2);
            Assert.IsFalse(r2 >= r1);
            Assert.IsTrue(r1 >= r1);
        }

        [TestMethod]
        public void CompareToObject1()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = 2.5;

            Assert.IsTrue(r1.CompareTo(r2) == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException), "The obj must be a RationalNumber or can be implicit converted to RationalNumber")]
        public void CompareToObject2()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = 2.5m as object;

            Assert.IsTrue(r1.CompareTo(r2) == 0);
        }

        [TestMethod]
        public void CompareToRationalNumber()
        {
            var r1 = new RationalNumber(2.5);
            var r2 = new RationalNumber(2.5);

            Assert.IsTrue(r1.CompareTo(r2) == 0);
        }

        [TestMethod]
        public void RationalNumberGetHashCode()
        {
            var r1 = new RationalNumber(2.5);

            Assert.AreEqual(r1.Numerator.GetHashCode() ^ r1.Denominator.GetHashCode(), r1.GetHashCode());
        }

        [TestMethod]
        public void ToDecimal()
        {
            var r1 = new RationalNumber(2.5);

            Assert.AreEqual(2.5m, r1.ToDecimal());
        }

        [TestMethod]
        public void ToSingle()
        {
            var r1 = new RationalNumber(2.5);

            Assert.AreEqual(2.5f, r1.ToSingle());
        }

        [TestMethod]
        public void ToString1()
        {
            var r1 = new RationalNumber(0.5);

            Assert.AreEqual("1 / 2", r1.ToString());
        }

        [TestMethod]
        public void ToString2()
        {
            var r1 = new RationalNumber(1.5);

            Assert.AreEqual("3 / 2", r1.ToString());
        }

        [TestMethod]
        public void ToStringBool1()
        {
            var r1 = new RationalNumber(1.5);

            Assert.AreEqual("3 / 2", r1.ToString(false));
        }

        [TestMethod]
        public void ToStringBool2()
        {
            var r1 = new RationalNumber(1.5);

            Assert.AreEqual("1 / 1 / 2", r1.ToString(true));
        }

        [TestMethod]
        public void ToStringBool3()
        {
            var r1 = new RationalNumber(0.5);

            Assert.AreEqual("1 / 2", r1.ToString(true));
        }

        [TestMethod]
        public void ToStringBool4()
        {
            var r1 = new RationalNumber(-40);

            Assert.AreEqual("-40 / 0 / 1", r1.ToString(true));
        }

        [TestMethod]
        public void ToStringBool5()
        {
            var r1 = new RationalNumber(-40.5);

            Assert.AreEqual("-40 / 1 / 2", r1.ToString(true));
        }

        [TestMethod]
        public void ConvertibleGetTypeCode()
        {
            var r1 = new RationalNumber(0.5) as IConvertible;

            Assert.IsTrue(r1.GetTypeCode() == TypeCode.Double);
        }

        [TestMethod]
        public void ConvertibleToBoolean1()
        {
            var r1 = new RationalNumber(1.5) as IConvertible;

            Assert.IsTrue(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertibleToBoolean2()
        {
            var r1 = new RationalNumber(0.5) as IConvertible;

            Assert.IsFalse(r1.ToBoolean(CultureInfo.CurrentCulture));
        }

        [TestMethod]
        public void ConvertibleToByte()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToByte(CultureInfo.CurrentCulture), 1);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertibleToChar()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            r1.ToChar(CultureInfo.CurrentCulture);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertibleToDateTime()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            r1.ToDateTime(CultureInfo.CurrentCulture);
        }

        [TestMethod]
        public void ConvertibleToDecimal()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToDecimal(CultureInfo.CurrentCulture), 1.3m);
        }

        [TestMethod]
        public void ConvertibleToDouble()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToDouble(CultureInfo.CurrentCulture), 1.3);
        }

        [TestMethod]
        public void ConvertibleToInt16()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToInt16(CultureInfo.CurrentCulture), 1);
        }

        [TestMethod]
        public void ConvertibleToInt32()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToInt32(CultureInfo.CurrentCulture), 1);
        }

        [TestMethod]
        public void ConvertibleToInt64()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToInt64(CultureInfo.CurrentCulture), 1);
        }

        [TestMethod]
        public void ConvertibleToSByte()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToSByte(CultureInfo.CurrentCulture), 1);
        }

        [TestMethod]
        public void ConvertibleToSingle()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToSingle(CultureInfo.CurrentCulture), 1.3f);
        }

        [TestMethod]
        public void ConvertibleToString()
        {
            var r1 = new RationalNumber(0.5) as IConvertible;

            Assert.AreEqual(r1.ToString(new CultureInfo("en-CA")), "1 / 2");
        }

        [TestMethod]
        public void ConvertibleToUInt16()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToUInt16(CultureInfo.CurrentCulture), 1);
        }

        [TestMethod]
        public void ConvertibleToUInt32()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToUInt32(CultureInfo.CurrentCulture), (uint)1);
        }

        [TestMethod]
        public void ConvertibleToUInt64()
        {
            var r1 = new RationalNumber(1.3) as IConvertible;

            Assert.AreEqual(r1.ToUInt64(CultureInfo.CurrentCulture), (ulong)1);
        }

        [TestMethod]
        public void IsInfinity()
        {
            Assert.IsTrue(RationalNumber.IsInfinity(RationalNumber.PositiveInfinity));
            Assert.IsTrue(RationalNumber.IsInfinity(RationalNumber.NegativeInfinity));
            Assert.IsTrue(RationalNumber.IsInfinity(double.PositiveInfinity));
            Assert.IsTrue(RationalNumber.IsInfinity(double.NegativeInfinity));
            Assert.IsFalse(RationalNumber.IsInfinity(2.3));
        }

        [TestMethod]
        public void IsPositiveInfinity()
        {
            Assert.IsTrue(RationalNumber.IsPositiveInfinity(RationalNumber.PositiveInfinity));
            Assert.IsFalse(RationalNumber.IsPositiveInfinity(RationalNumber.NegativeInfinity));
            Assert.IsTrue(RationalNumber.IsPositiveInfinity(double.PositiveInfinity));
            Assert.IsFalse(RationalNumber.IsPositiveInfinity(double.NegativeInfinity));
            Assert.IsFalse(RationalNumber.IsPositiveInfinity(2.3));
        }

        [TestMethod]
        public void IsNegativeInfinity()
        {
            Assert.IsFalse(RationalNumber.IsNegativeInfinity(RationalNumber.PositiveInfinity));
            Assert.IsTrue(RationalNumber.IsNegativeInfinity(RationalNumber.NegativeInfinity));
            Assert.IsFalse(RationalNumber.IsNegativeInfinity(double.PositiveInfinity));
            Assert.IsTrue(RationalNumber.IsNegativeInfinity(double.NegativeInfinity));
            Assert.IsFalse(RationalNumber.IsNegativeInfinity(2.3));
        }

        [TestMethod]
        public void IsNan()
        {
            Assert.IsFalse(RationalNumber.IsNaN(RationalNumber.PositiveInfinity));
            Assert.IsFalse(RationalNumber.IsNaN(RationalNumber.NegativeInfinity));
            Assert.IsFalse(RationalNumber.IsNaN(double.PositiveInfinity));
            Assert.IsFalse(RationalNumber.IsNaN(double.NegativeInfinity));
            Assert.IsFalse(RationalNumber.IsNaN(2.3));
            Assert.IsTrue(RationalNumber.IsNaN(double.NaN));
            Assert.IsTrue(RationalNumber.IsNaN(RationalNumber.NaN));
        }

        [TestMethod]
        public void IConvertibleToType()
        {
            var r1 = new RationalNumber(2) as IConvertible;

            Assert.AreEqual(r1.ToType(typeof(int), CultureInfo.CurrentCulture), 2);
        }
    }
}
