using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace LeanCommandUnframework
{
    [TestFixture]
    public class PipelineFactoryTests
    {
        [Test]
        public void It_executes_the_handler()
        {
            var handlerCollection = new HandlerSelectorCollection(typeof(TestCommandHandler));
            var filters = new FilterSelector();
            var pipeline = new PipelineFactory(handlerCollection, filters, new ObjectFactory());

            var result = pipeline.Process(new TestCommand()) as TestCommandResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.WasHandled);
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

        public class TestCommandHandler : IHandler<TestCommand>
        {
            public object Handle(TestCommand command)
            {
                return new TestCommandResult()
                           {
                               WasHandled = true,
                               WasFilteredBefore = command.WasFilteredBefore
                           };
            }
        }
    }
}
// ReSharper restore InconsistentNaming
