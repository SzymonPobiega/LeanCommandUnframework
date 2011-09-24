using Moq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace LeanCommandUnframework
{
    [TestFixture]
    public class FilterCollectionTests
    {
        [Test]
        public void It_executes_all_filters_before_handling()
        {
            var filterOne = new Mock<IFilter<TestCommand>>();
            var filterTwo = new Mock<IFilter<TestCommand>>();
            var collection = new FilterCollection(new object[] {filterOne.Object, filterTwo.Object});
            var command = new TestCommand();

            collection.OnHandling(command);

            filterOne.Verify(x => x.OnHandling(command));
            filterTwo.Verify(x => x.OnHandling(command));
        }

        [Test]
        public void It_executes_all_filters_after_handling()
        {
            var filterOne = new Mock<IFilter<TestCommand>>();
            var filterTwo = new Mock<IFilter<TestCommand>>();
            var collection = new FilterCollection(new object[] { filterOne.Object, filterTwo.Object });
            var command = new TestCommand();
            var result = new object();

            collection.OnHandled(command, result);

            filterOne.Verify(x => x.OnHandled(command, result));
            filterTwo.Verify(x => x.OnHandled(command, result));
        }


        public class TestCommand
        {
        }
    }
}
// ReSharper restore InconsistentNaming
