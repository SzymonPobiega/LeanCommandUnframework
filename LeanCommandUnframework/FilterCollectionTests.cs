using System;
using System.Collections.Generic;
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

        [Test]
        public void It_calls_OnProcessing_and_OnProcessed_on_same_instances_of_filters()
        {
            var collection = new FilterCollection(GetFilterInstances());
            var command = new TestCommand();
            var result = new object();

            collection.OnHandling(command);
            collection.OnHandled(command, result);
        }

        private static IEnumerable<object> GetFilterInstances()
        {
            yield return new TestFilter();
        }

        public class TestFilter : IFilter<TestCommand>
        {
            private bool _onHandlingCalled;

            public void OnHandling(TestCommand command)
            {
                _onHandlingCalled = true;
            }

            public void OnHandled(TestCommand command, object result)
            {
                Assert.IsTrue(_onHandlingCalled);
            }
        }

        public class TestCommand
        {
        }
    }
}
// ReSharper restore InconsistentNaming
