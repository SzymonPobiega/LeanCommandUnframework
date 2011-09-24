using System;
using System.Collections.Generic;
using System.Linq;

namespace LeanCommandUnframework
{
    public class Pipeline
    {
        private readonly HandlerSelectorCollection _handlerSelectorCollection;
        private readonly FilterSelector _filterSelector;
        private readonly IObjectFactory _objectFactory;

        public Pipeline(HandlerSelectorCollection handlerSelectorCollection, FilterSelector filterSelector, IObjectFactory objectFactory)
        {
            _handlerSelectorCollection = handlerSelectorCollection;
            _filterSelector = filterSelector;
            _objectFactory = objectFactory;
        }

        public object Process(object command)
        {
            var commandType = command.GetType();

            var filterTypes = _filterSelector.GetFiltersFor(commandType);
            var filters = filterTypes.Select(x => _objectFactory.GetHandlerInstance(x));
            var filterCollection = new FilterCollection(filters);

            filterCollection.OnHandling(command);

            object result = Handle(command, commandType);

            filterCollection.OnHandled(command, result);

            return result;
        }

        private object Handle(object command, Type commandType)
        {
            var handlerType = _handlerSelectorCollection.GetHandlerFor(commandType);
            dynamic handler = _objectFactory.GetHandlerInstance(handlerType);

            dynamic dynamicCommand = command;
            return handler.Handle(dynamicCommand);
        }
    }
}