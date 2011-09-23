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

            var handlerType = collection.GetFor(typeof (TestCommand));

            Assert.AreEqual(typeof(TestCommandHandler), handlerType);
        }

        [Test]
        public void It_can_contain_more_than_one_type()
        {
            var collection = new HandlerCollection(typeof(TestCommandHandler), typeof (AnotherTestCommandHandler));

            var firstHandler = collection.GetFor(typeof (TestCommand));
            var secondHandler = collection.GetFor(typeof (AnotherTestCommand));

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
