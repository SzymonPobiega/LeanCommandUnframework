using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace LeanCommandUnframework
{
    [TestFixture]
    public class PipelineTests
    {
        [Test]
        public void It_executes_the_handler()
        {
            var handlerCollection = new HandlerCollection(typeof(TestCommandHandler));
            var filters = new FilterCollection();
            var pipeline = new Pipeline(handlerCollection, filters, new ObjectFactory());

            var result = pipeline.Process(new TestCommand()) as TestCommandResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.WasHandled);
        }

        [Test]
        public void It_executes_the_filters()
        {
            var handlers = new HandlerCollection(typeof(TestCommandHandler));
            var filters = new FilterCollection(typeof (TestCommandFilter));
            var pipeline = new Pipeline(handlers, filters, new ObjectFactory());

            var result = pipeline.Process(new TestCommand()) as TestCommandResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.WasFilteredBefore);
            Assert.IsTrue(result.WasFilteredAfter);
        }

        public class TestCommand
        {
            public bool WasFilteredBefore;
        }

        public class TestCommandResult
        {
            public bool WasHandled;
            public bool WasFilteredBefore;
            public bool WasFilteredAfter;
        }

        public class TestCommandHandler
        {
            public TestCommandResult Handle(TestCommand command)
            {
                return new TestCommandResult()
                           {
                               WasHandled = true,
                               WasFilteredBefore = command.WasFilteredBefore
                           };
            }
        }

        public class TestCommandFilter
        {
            public void BeforeHandling(TestCommand command)
            {
                command.WasFilteredBefore = true;
            }

            public void AfterHandling(TestCommandResult result)
            {
                result.WasFilteredAfter = true;
            }
        }
    }
}
// ReSharper restore InconsistentNaming
