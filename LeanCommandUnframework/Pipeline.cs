using System;

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
            var handlerType = _handlerCollection.GetHandlerFor(commandType);
            dynamic handler = _objectFactory.CreateInstance(handlerType);

            dynamic dynamicCommand = command;
            var result = handler.Handle(dynamicCommand);

            return result;
        }
    }
}