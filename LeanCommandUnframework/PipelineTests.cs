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
            var pipeline = new Pipeline(new HandlerCollection(typeof(TestCommandHandler)), new HandlerFactory());

            var result = pipeline.Process(new TestCommand());

            Assert.IsInstanceOf<TestCommandResult>(result);
        }

        public class TestCommand
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
    }
}
// ReSharper restore InconsistentNaming
