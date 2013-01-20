namespace NLib.ComponentModel.DataAnnotations.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Xunit;

    public class EqualsToAttributeTest
    {
        [Fact]
        public void PropertyAreEqualValue()
        {
            var model = new ModelProperty { P1 = "Foo", P2 = "Foo" };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.True(r);
            Assert.Equal(0, vr.Count);
        }

        [Fact]
        public void PropertyAreNotEqualValue()
        {
            var model = new ModelProperty { P1 = "Foo", P2 = "Bar" };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.False(r);
            Assert.Equal(1, vr.Count);
            Assert.Equal("'P2' and 'P1' do not match.", vr[0].ErrorMessage);
        }

        [Fact]
        public void PropertyDifferentTypeAreEqualValue()
        {
            var model = new ModelPropertyDifferentType { P1 = "2", P2 = 2 };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.True(r);
            Assert.Equal(0, vr.Count);
        }

        [Fact]
        public void PropertyDifferentTypeAreNotEqualValue()
        {
            var model = new ModelPropertyDifferentType { P1 = "22", P2 = 2 };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.False(r);
            Assert.Equal(1, vr.Count);
            Assert.Equal("'P2' and 'P1' do not match.", vr[0].ErrorMessage);
        }

        [Fact]
        public void PropertyCannotBeCompared()
        {
            var model = new ModelPropertyDifferentType { P1 = "Foo", P2 = 2 };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();

            var ex = Assert.Throws<ValidationException>(() => Validator.TryValidateObject(model, vc1, vr, true));
            Assert.Equal("'P2' and 'P1' cannot be compared.", ex.Message);
        }

        [Fact]
        public void PropertyTypeMatch()
        {
            var model = new ModelPropertyTypeMatch { P1 = "Foo", P2 = "Foo" };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.True(r);
            Assert.Equal(0, vr.Count);
        }

        [Fact]
        public void PropertyTypeMissMatch()
        {
            var model = new ModelPropertyTypeMissMatch { P1 = "Foo", P2 = 2 };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var ex = Assert.Throws<ValidationException>(() => Validator.TryValidateObject(model, vc1, vr, true));
            Assert.Equal("'P1' type (String) and 'P2' type (Int32) must be the same.", ex.Message);
        }

        [Fact]
        public void PropertyAreNull()
        {
            var model = new ModelProperty { P1 = null, P2 = null };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.True(r);
            Assert.Equal(0, vr.Count);
        }

        [Fact]
        public void PropertyIsMissing()
        {
            var model = new ModelPropertyMissing { P1 = "Foo" };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var ex = Assert.Throws<ValidationException>(() => Validator.TryValidateObject(model, vc1, vr, true));
            Assert.Equal("Could not find a property named P2.", ex.Message);
        }

        [Fact]
        public void CustomAttributeValue()
        {
            var model = new ModelProperty2 { P1 = "Foo", P2 = "Foo" };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.True(r);
            Assert.Equal(0, vr.Count);
        }

        [Fact]
        public void CustomAttributeValue2()
        {
            var model = new ModelProperty2 { P1 = "Foo", P2 = "Bar" };

            var vc1 = new ValidationContext(model, null, null);
            var vr = new List<ValidationResult>();
            var r = Validator.TryValidateObject(model, vc1, vr, true);
            Assert.False(r);
            Assert.Equal(1, vr.Count);
            Assert.Equal("Test", vr[0].ErrorMessage);
        }

        public class ModelProperty
        {
            public string P1 { get; set; }
            [EqualsTo("P1")]
            public string P2 { get; set; }
        }

        public class ModelProperty2
        {
            public string P1 { get; set; }
            [CustomProperty("P1")]
            public string P2 { get; set; }
        }

        public class ModelPropertyDifferentType
        {
            public string P1 { get; set; }
            [EqualsTo("P1")]
            public int P2 { get; set; }
        }

        public class ModelPropertyTypeMatch
        {
            [EqualsTo("P2", MustBeSameType = true)]
            public string P1 { get; set; }
            public string P2 { get; set; }
        }

        public class ModelPropertyTypeMissMatch
        {
            [EqualsTo("P2", MustBeSameType = true)]
            public string P1 { get; set; }
            public int P2 { get; set; }
        }

        public class ModelPropertyMissing
        {
            [EqualsTo("P2")]
            public string P1 { get; set; }
        }

        public class CustomPropertyAttribute : CompareBaseAttribute
        {
            public CustomPropertyAttribute(string otherPropertyName)
                : base(otherPropertyName)
            {
                this.ErrorMessage = "Test";
            }

            public CustomPropertyAttribute(string otherPropertyName, string errorMessage)
                : base(otherPropertyName, errorMessage)
            {
            }

            public CustomPropertyAttribute(string otherPropertyName, Func<string> errorMessageAccessor)
                : base(otherPropertyName, errorMessageAccessor)
            {
            }

            protected override bool IsValid(IComparable currentValue, object otherValue)
            {
                return currentValue.CompareTo(otherValue) == 0;
            }
        }
    }
}
