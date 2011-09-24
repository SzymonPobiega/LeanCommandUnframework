using System;
using System.Linq;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace LeanCommandUnframework
{
    [TestFixture]
    public class FilterCollectionTests
    {
        [Test]
        public void It_can_find_handler_by_exact_type()
        {
            var collection = new FilterCollection(typeof(TestCommandFilter));

            var filterTypes = collection.GetFiltersFor(typeof (TestCommand));

            Assert.Contains(typeof(TestCommandFilter), filterTypes.ToList());
        }

        [Test]
        public void It_can_find_handler_by_subtype()
        {
            var collection = new FilterCollection(typeof(TestCommandFilter));

            var filterTypes = collection.GetFiltersFor(typeof(DerivedTestCommand));

            Assert.Contains(typeof(TestCommandFilter), filterTypes.ToList());
        }

        public class TestCommand
        {
        }

        public class DerivedTestCommand : TestCommand
        {
        }

        public class TestCommandFilter : IFilter<TestCommand>
        {
            public void OnHandling(TestCommand command)
            {
            }

            public void OnHandled(TestCommand command, object result)
            {
            }
        }

        public class DerivedTestCommandHandler : IFilter<DerivedTestCommand>
        {
            public void OnHandling(DerivedTestCommand command)
            {
            }

            public void OnHandled(DerivedTestCommand command, object result)
            {
            }
        }
    }
}
// ReSharper restore InconsistentNaming
