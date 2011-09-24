using System;
using System.Collections.Generic;
using System.Linq;

namespace LeanCommandUnframework
{
    public class Pipeline
    {
        private readonly HandlerCollection _handlerCollection;
        private readonly FilterCollection _filterCollection;
        private readonly IObjectFactory _objectFactory;

        public Pipeline(HandlerCollection handlerCollection, FilterCollection filterCollection, IObjectFactory objectFactory)
        {
            _handlerCollection = handlerCollection;
            _filterCollection = filterCollection;
            _objectFactory = objectFactory;
        }

        public object Process(object command)
        {
            var commandType = command.GetType();

            var filterTypes = _filterCollection.GetFiltersFor(commandType);
            var filters = filterTypes.Select(x => _objectFactory.GetHandlerInstance(x));

            ExecuteFiltersBeforeHandling(filters, command);

            object result = Handle(command, commandType);

            ExecuteFiltersAfterHandling(filters, command, result);

            return result;
        }

        private static void ExecuteFiltersAfterHandling(IEnumerable<object> filters, dynamic command, object result)
        {
            foreach (dynamic filter in filters)
            {
                filter.OnHandled(command, result);
            }
        }

        private static void ExecuteFiltersBeforeHandling(IEnumerable<object> filters, dynamic command)
        {
            foreach (dynamic filter in filters)
            {
                filter.OnHandling(command);
            }
        }

        private object Handle(object command, Type commandType)
        {
            var handlerType = _handlerCollection.GetHandlerFor(commandType);
            dynamic handler = _objectFactory.GetHandlerInstance(handlerType);

            dynamic dynamicCommand = command;
            return handler.Handle(dynamicCommand);
        }
    }
}