namespace LeanCommandUnframework
{
    public class Pipeline
    {
        private readonly FilterCollection _filterCollection;
        private readonly object _handler;
        private readonly object _command;

        public Pipeline(object command, FilterCollection filterCollection, object handler)
        {
            _filterCollection = filterCollection;
            _command = command;
            _handler = handler;
        }

        public object Process()
        {
            _filterCollection.OnHandling(_command);
            var result = Handle();
            _filterCollection.OnHandled(_command, result);
            return result;
        }

        private object Handle()
        {
            dynamic handler = _handler;
            dynamic dynamicCommand = _command;
            return handler.Handle(dynamicCommand);
        }
    }
}