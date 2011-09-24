using System;
using System.Linq;

namespace LeanCommandUnframework
{
    public class PipelineFactory
    {
        private readonly HandlerSelector _handlerSelector;
        private readonly FilterSelector _filterSelector;
        private readonly IObjectFactory _objectFactory;

        public PipelineFactory(HandlerSelector handlerSelector, FilterSelector filterSelector, IObjectFactory objectFactory)
        {
            _handlerSelector = handlerSelector;
            _filterSelector = filterSelector;
            _objectFactory = objectFactory;
        }

        public object Process(object command)
        {
            var commandType = command.GetType();
            var pipeline = CreatePipeline(command, commandType);
            return pipeline.Process();
        }

        private Pipeline CreatePipeline(object command, Type commandType)
        {
            var handler = CreateHandler(commandType);
            var filterCollection = CreateFilterCollection(commandType);
            return new Pipeline(command, filterCollection, handler);
        }

        private object CreateHandler(Type commandType)
        {
            var handlerType = _handlerSelector.GetHandlerFor(commandType);
            return _objectFactory.GetHandlerInstance(handlerType);
        }

        private FilterCollection CreateFilterCollection(Type commandType)
        {
            var filterTypes = _filterSelector.GetFiltersFor(commandType);
            var filters = filterTypes.Select(x => _objectFactory.GetHandlerInstance(x));
            return new FilterCollection(filters);
        }
    }
}