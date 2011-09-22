using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace LeanCommandUnframework
{
    [TestFixture]
    public class HandlerCollectionTests
    {
        [Test]
        public void It_can_find_handler_by_exact_type()
        {
            var collection = new HandlerCollection(typeof (TestCommandHandler));

            var handlerType = collection.GetHandlerFor(typeof (TestCommand));

            Assert.AreEqual(typeof(TestCommandHandler), handlerType);
        }

        [Test]
        public void It_can_contain_more_than_one_type()
        {
            var collection = new HandlerCollection(typeof(TestCommandHandler), typeof (AnotherTestCommandHandler));

            var firstHandler = collection.GetHandlerFor(typeof (TestCommand));
            var secondHandler = collection.GetHandlerFor(typeof (AnotherTestCommand));

            Assert.AreEqual(typeof(TestCommandHandler), firstHandler);
            Assert.AreEqual(typeof(AnotherTestCommandHandler), secondHandler);
        }

        public class TestCommand
        {
        }

        public class AnotherTestCommand
        {
        }

        public class TestCommandResult
        {
        }

        public class TestCommandHandler
        {
            public TestCommandResult Handle(TestCommand command)
            {
                return new TestCommandResult();
            }
        }

        public class AnotherTestCommandHandler
        {
            public TestCommandResult Handle(AnotherTestCommand command)
            {
                return new TestCommandResult();
            }
        }
    }
}
// ReSharper restore InconsistentNaming
