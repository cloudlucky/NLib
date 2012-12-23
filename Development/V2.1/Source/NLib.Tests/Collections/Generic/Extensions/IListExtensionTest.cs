﻿namespace NLib.Tests.Collections.Generic.Extensions
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Collections.Generic.Extensions;

    [TestClass]
    public class IListExtensionTest
    {
        [TestMethod]
        public void SwapValues1()
        {
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };
            l.SwapValues(2, 3);

            CollectionAssert.AreEquivalent(l, new[] { 1, 2, 4, 3, 5, 6 });
        }
    }
}
