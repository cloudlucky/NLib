namespace NLib.Tests.Extensions
{
    using System;

    using NLib;
    using NLib.Extensions;

    using Xunit;

    public class EventHandlerExtensionsTest
    {
        public event EventHandler Event1;
        public event EventHandler<EventArgs<int>> Event2;

        public EventHandlerExtensionsTest()
        {
            this.Event1 = null;
            this.Event2 = null;
        }

        [Fact]
        public void RaiseEventTest1()
        {
            this.Event1.RaiseEvent(this);

            Assert.True(true);
        }

        [Fact]
        public void RaiseEventTest2()
        {
            var i = 1;
            this.Event1 += (sender, e) => i++;

            this.Event1.RaiseEvent(this);

            Assert.Equal(2, i);
        }

        [Fact]
        public void RaiseEventTest3()
        {
            var i = 1;
            this.Event1 += (sender, e) => i++;

            this.Event1.RaiseEvent(this, EventArgs.Empty);

            Assert.Equal(2, i);
        }

        [Fact]
        public void RaiseEventTest4()
        {
            var i = 1;
            this.Event2 += (sender, e) => i += e.Value;

            this.Event2.RaiseEvent(this, new EventArgs<int>(2));

            Assert.Equal(3, i);
        }

        [Fact]
        public void RaiseEventTest5()
        {
            var i = 1;
            this.Event1 += (sender, e) => i--;
            this.Event2 += (sender, e) => i += e.Value;
            this.Event1.RaiseEvent(this);
            this.Event2.RaiseEvent(this, new EventArgs<int>(2));

            Assert.Equal(2,i);
        }
    }
}
