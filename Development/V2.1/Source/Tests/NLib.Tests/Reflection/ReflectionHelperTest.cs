namespace NLib.Tests.Reflection
{
    using NLib.Reflection.Extensions;

    using Xunit;

    public class ReflectionHelperTest
    {
        [Fact]
        public void PublicFieldTest()
        {
            var t = new FieldClass();

            var m1 = t.Reflection()
                      .Field(x => x.m1)
                      .SetValue("Foo")
                      .Field(x => x.m1)
                      .GetValue();

            var f1 = t.Reflection()
                      .Field(x => x.m1)
                      .FieldInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal("Foo", t.m1);
            Assert.Equal("Foo", m1);
            Assert.Equal(t, t2);
            Assert.Equal("String", f1.Name);
        }

        [Fact]
        public void PublicFieldTest2()
        {
            var t = new SubFieldClass();

            var m1 = t.Reflection()
                      .Field(x => x.m1)
                      .SetValue("Foo")
                      .Field(x => x.m1)
                      .GetValue();

            var f1 = t.Reflection()
                      .Field(x => x.m1)
                      .FieldInfoType;

            var m3 = t.Reflection()
                      .Field(x => x.m3)
                      .SetValue(987654321)
                      .Field(x => x.m3)
                      .GetValue();

            var f3 = t.Reflection()
                      .Field(x => x.m3)
                      .FieldInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal("Foo", t.m1);
            Assert.Equal("Foo", m1);

            Assert.Equal(987654321, t.m3);
            Assert.Equal(987654321, m3);
            Assert.Equal(t, t2);
            Assert.Equal("String", f1.Name);
            Assert.Equal("Int64", f3.Name);
        }

        [Fact]
        public void PrivateFieldTest()
        {
            var t = new FieldClass();

            var m2 = t.Reflection()
                      .Field("m2")
                      .SetValue(230)
                      .Field("m2")
                      .GetValue();

            var f2 = t.Reflection()
                      .Field("m2")
                      .FieldInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal(230, m2);
            Assert.Equal(t, t2);
            Assert.Equal("Int32", f2.Name);
        }

        [Fact]
        public void PrivateFieldTest2()
        {
            var t = new SubFieldClass();

            var m2 = t.Reflection()
                      .Field("m2")
                      .SetValue(230)
                      .Field("m2")
                      .GetValue();

            var f2 = t.Reflection()
                      .Field("m2")
                      .FieldInfoType;

            var m4 = t.Reflection()
                      .Field("m4")
                      .SetValue(456.25)
                      .Field("m4")
                      .GetValue();

            var f4 = t.Reflection()
                      .Field("m4")
                      .FieldInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal(230, m2);
            Assert.Equal(456.25, m4);
            Assert.Equal(t, t2);
            Assert.Equal("Int32", f2.Name);
            Assert.Equal("Double", f4.Name);
        }

        [Fact]
        public void PublicPropertyTest()
        {
            var t = new PropertyClass();

            var p1 = t.Reflection()
                      .Property(x => x.P1)
                      .SetValue("Foo")
                      .Property(x => x.P1)
                      .GetValue();

            var pi1 = t.Reflection()
                      .Property(x => x.P1)
                      .PropertyInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal("Foo", t.P1);
            Assert.Equal("Foo", p1);
            Assert.Equal(t, t2);
            Assert.Equal("String", pi1.Name);
        }

        [Fact]
        public void PublicPropertyTest2()
        {
            var t = new SubPropertyClass();

            var p1 = t.Reflection()
                      .Property(x => x.P1)
                      .SetValue("Foo")
                      .Property(x => x.P1)
                      .GetValue();

            var pi1 = t.Reflection()
                      .Property(x => x.P1)
                      .PropertyInfoType;

            var p3 = t.Reflection()
                      .Property(x => x.P3)
                      .SetValue(987654321)
                      .Property(x => x.P3)
                      .GetValue();

            var pi3 = t.Reflection()
                      .Property(x => x.P3)
                      .PropertyInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal("Foo", t.P1);
            Assert.Equal("Foo", p1);

            Assert.Equal(987654321, t.P3);
            Assert.Equal(987654321, p3);
            Assert.Equal(t, t2);
            Assert.Equal("String", pi1.Name);
            Assert.Equal("Int64", pi3.Name);
        }

        [Fact]
        public void PrivatePropertyTest()
        {
            var t = new PropertyClass();

            var p2 = t.Reflection()
                      .Property("P2")
                      .SetValue(230)
                      .Property("P2")
                      .GetValue();

            var pi2 = t.Reflection()
                      .Property("P2")
                      .PropertyInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal(230, p2);
            Assert.Equal(t, t2);
            Assert.Equal("Int32", pi2.Name);
        }

        [Fact]
        public void PrivatePropertyTest2()
        {
            var t = new SubPropertyClass();

            var p2 = t.Reflection()
                      .Property("P2")
                      .SetValue(230)
                      .Property("P2")
                      .GetValue();

            var pi2 = t.Reflection()
                      .Property("P2")
                      .PropertyInfoType;

            var p4 = t.Reflection()
                      .Property("P4")
                      .SetValue(456.25)
                      .Property("P4")
                      .GetValue();

            var pi4 = t.Reflection()
                      .Property("P4")
                      .PropertyInfoType;

            var t2 = t.Reflection()
                      .Return();

            Assert.Equal(230, p2);
            Assert.Equal(456.25, p4);
            Assert.Equal(t, t2);
            Assert.Equal("Int32", pi2.Name);
            Assert.Equal("Double", pi4.Name);
        }

        public class FieldClass
        {
            public string m1;

            private int m2;
        }

        public class SubFieldClass : FieldClass
        {
            public long m3;

            private double m4;
        }

        public class PropertyClass
        {
            public string P1 { get; set; }

            private int P2 { get; set; }
        }

        public class SubPropertyClass : PropertyClass
        {
            public long P3{get;set;}

            private double P4 { get; set; }
        }
    }
}
