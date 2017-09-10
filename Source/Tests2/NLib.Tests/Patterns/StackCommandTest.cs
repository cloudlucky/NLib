namespace NLib.Tests.Patterns
{
    using NLib.Patterns;

    using Xunit;
    public class StackCommandTest
    {
        [Fact]
        public void Test1()
        {
            var i = 1;
            var sc = new StackCommand(() => i++, () => i--);

            sc.Execute();
            Assert.Equal(2, i);
            Assert.True(sc.CanUndo);
            Assert.False(sc.CanRedo);

            sc.Execute();
            Assert.Equal(3, i);
            Assert.True(sc.CanUndo);
            Assert.False(sc.CanRedo);

            sc.Undo();
            Assert.Equal(2, i);
            Assert.True(sc.CanUndo);
            Assert.True(sc.CanRedo);

            sc.Undo();
            Assert.Equal(1, i);
            Assert.False(sc.CanUndo);
            Assert.True(sc.CanRedo);

            sc.Redo();
            Assert.Equal(2, i);
            Assert.True(sc.CanUndo);
            Assert.True(sc.CanRedo);

            sc.Redo();
            Assert.Equal(3, i);
            Assert.True(sc.CanUndo);
            Assert.False(sc.CanRedo);
        }

        [Fact]
        public void Test2()
        {
            var i = 1;
            var sc = new StackCommand(() => i++, () => i--, () => i*=3);

            sc.Execute();
            Assert.Equal(2, i);
            Assert.True(sc.CanUndo);
            Assert.False(sc.CanRedo);

            sc.Execute();
            Assert.Equal(3, i);
            Assert.True(sc.CanUndo);
            Assert.False(sc.CanRedo);

            sc.Undo();
            sc.Undo();
            Assert.Equal(1, i);
            Assert.False(sc.CanUndo);
            Assert.True(sc.CanRedo);
            
            sc.Redo();
            sc.Redo();
            Assert.False(sc.CanRedo);
            Assert.True(sc.CanUndo);
        }

        [Fact]
        public void Test3()
        {
            var i = 1;
            var sc = new StackCommand(() => i++, () => i--);

            sc.Execute();
            sc.Execute();
            sc.Execute();

            Assert.True(sc.CanUndo);
            Assert.False(sc.CanRedo);

            sc.Undo();
            sc.Clear();

            Assert.False(sc.CanUndo);
            Assert.False(sc.CanRedo);
        }
    }
}
