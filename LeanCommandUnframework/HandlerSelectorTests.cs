using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace LeanCommandUnframework
{
    [TestFixture]
    public class HandlerSelectorTests
    {
        [Test]
        public void It_can_find_handler_by_exact_type()
        {
            var collection = new HandlerSelectorCollection(typeof (TestCommandHandler));

            var handlerType = collection.GetHandlerFor(typeof (TestCommand));

            Assert.AreEqual(typeof(TestCommandHandler), handlerType);
        }

        [Test]
        public void It_can_contain_more_than_one_type()
        {
            var collection = new HandlerSelectorCollection(typeof(TestCommandHandler), typeof (AnotherTestCommandHandler));

            var firstHandler = collection.GetHandlerFor(typeof(TestCommand));
            var secondHandler = collection.GetHandlerFor(typeof(AnotherTestCommand));

            Assert.AreEqual(typeof(TestCommandHandler), firstHandler);
            Assert.AreEqual(typeof(AnotherTestCommandHandler), secondHandler);
        }

        public class TestCommand
        {
        }

        public class AnotherTestCommand
        {
        }

        public class TestCommandHandler : IHandler<TestCommand>
        {
            public object Handle(TestCommand command)
            {
                return new object();
            }
        }

        public class AnotherTestCommandHandler : IHandler<AnotherTestCommand>
        {
            public object Handle(AnotherTestCommand command)
            {
                return new object();
            }
        }
    }
}
// ReSharper restore InconsistentNaming
