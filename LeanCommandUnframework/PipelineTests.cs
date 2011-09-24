using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace LeanCommandUnframework
{
    [TestFixture]
    public class PipelineTests
    {
        [Test]
        public void It_executes_filters_and_handler()
        {
            var command = new TestCommand();
            var commandResult = new object();
            var sequence = new MockSequence();
            
            var handlerMock = new Mock<IHandler<TestCommand>>(MockBehavior.Strict);
            var filterMock = new Mock<IFilter<TestCommand>>(MockBehavior.Strict);

            filterMock.InSequence(sequence).Setup(x => x.OnHandling(command));
            handlerMock.InSequence(sequence).Setup(x => x.Handle(command)).Returns(commandResult);
            filterMock.InSequence(sequence).Setup(x => x.OnHandled(command, commandResult));

            var pipeline = new Pipeline(command, new FilterCollection(new object[] {filterMock.Object}),
                                        handlerMock.Object);

            pipeline.Process();

            filterMock.VerifyAll();
            handlerMock.VerifyAll();
        }

        public class TestCommand
        {
        }
    }
}
// ReSharper restore InconsistentNaming
