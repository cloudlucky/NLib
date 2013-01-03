namespace NLib.Tests.Patterns
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Patterns;

    [TestClass]
    public class SimpleCommandTest
    {
        [TestMethod]
        public void Test1()
        {
            var i = 1;
            var sc = new SimpleCommand(() => i++, () => i--);

            sc.Execute();
            Assert.AreEqual(2, i);
            Assert.IsTrue(sc.CanUndo);
            Assert.IsFalse(sc.CanRedo);

            sc.Execute();
            Assert.AreEqual(3, i);
            Assert.IsTrue(sc.CanUndo);
            Assert.IsFalse(sc.CanRedo);

            sc.Undo();
            Assert.AreEqual(2, i);
            Assert.IsTrue(sc.CanUndo);
            Assert.IsTrue(sc.CanRedo);

            sc.Undo();
            Assert.AreEqual(1, i);
            Assert.IsFalse(sc.CanUndo);
            Assert.IsTrue(sc.CanRedo);

            sc.Redo();
            Assert.AreEqual(2, i);
            Assert.IsTrue(sc.CanUndo);
            Assert.IsTrue(sc.CanRedo);

            sc.Redo();
            Assert.AreEqual(3, i);
            Assert.IsTrue(sc.CanUndo);
            Assert.IsFalse(sc.CanRedo);
        }

        [TestMethod]
        public void Test2()
        {
            var i = 1;
            var sc = new SimpleCommand(() => i++, () => i--, () => i*=3);

            sc.Execute();
            Assert.AreEqual(2, i);
            Assert.IsTrue(sc.CanUndo);
            Assert.IsFalse(sc.CanRedo);

            sc.Execute();
            Assert.AreEqual(3, i);
            Assert.IsTrue(sc.CanUndo);
            Assert.IsFalse(sc.CanRedo);

            sc.Undo();
            sc.Undo();
            Assert.AreEqual(1, i);
            Assert.IsFalse(sc.CanUndo);
            Assert.IsTrue(sc.CanRedo);
            
            sc.Redo();
            sc.Redo();
            Assert.IsFalse(sc.CanRedo);
            Assert.IsTrue(sc.CanUndo);
        }

        [TestMethod]
        public void Test3()
        {
            var i = 1;
            var sc = new SimpleCommand(() => i++, () => i--);

            sc.Execute();
            sc.Execute();
            sc.Execute();

            Assert.IsTrue(sc.CanUndo);
            Assert.IsFalse(sc.CanRedo);

            sc.Undo();
            sc.Clear();

            Assert.IsFalse(sc.CanUndo);
            Assert.IsFalse(sc.CanRedo);
        }
    }
}
