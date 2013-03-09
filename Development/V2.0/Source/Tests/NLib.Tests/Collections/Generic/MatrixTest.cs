namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NLib.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    class MatrixTest
    {
        [TestMethod, Timeout(2000)]
        public void matrixTest()
        {
            Assert.IsInstanceOfType(new Matrix2D(0, 0), typeof(Matrix2D));
        }

        [TestMethod, Timeout(2000)]
        public static void SumTest()
        {
            Assert.AreEqual(new Matrix2D(2, 2) + new Matrix2D(2, 2), new Matrix2D(2, 2));   
        }


    }
}
