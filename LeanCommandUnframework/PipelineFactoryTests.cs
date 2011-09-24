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
        public void It_can_process_a_command()
        {
            var handlerCollection = new HandlerSelector(typeof(TestCommandHandler));
            var filters = new FilterSelector();
            var pipeline = new PipelineFactory(handlerCollection, filters, new DefaultObjectFactory());

            var result = pipeline.Process(new TestCommand()) as TestCommandResult;

            Assert.IsNotNull(result);
        }

        public class TestCommand
        {
        }

        public class TestCommandResult
        {
        }

        public class TestCommandHandler : IHandler<TestCommand>
        {
            public object Handle(TestCommand command)
            {
                return new TestCommandResult();
            }
        }
    }
}
// ReSharper restore InconsistentNaming
