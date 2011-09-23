using System;
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

            var handlerType = collection.GetFor(typeof (TestCommand));

            Assert.AreEqual(typeof(TestCommandFilter), handlerType);
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
