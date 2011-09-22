namespace LeanCommandUnframework
{
    public class Pipeline
    {
        private readonly HandlerCollection _handlerCollection;
        private readonly IHandlerFactory _handlerFactory;

        public Pipeline(HandlerCollection handlerCollection, IHandlerFactory handlerFactory)
        {
            _handlerCollection = handlerCollection;
            _handlerFactory = handlerFactory;
        }

        public object Process(object command)
        {
            var commandType = command.GetType();
            var handlerType = _handlerCollection.GetHandlerFor(commandType);
            dynamic handler = _handlerFactory.CreateInstance(handlerType);

            dynamic dynamicCommand = command;
            var result = handler.Handle(dynamicCommand);

            return result;
        }
    }
}