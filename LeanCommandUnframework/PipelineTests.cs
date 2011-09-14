using System;
using System.Collections.Generic;
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
            var pipeline = new Pipeline(new HandlerCollection(typeof(TestCommandHandler)));

            var result = pipeline.Process(new TestCommand());

            Assert.IsInstanceOf<TestCommandResult>(result);
        }
    }
}
// ReSharper restore InconsistentNaming
