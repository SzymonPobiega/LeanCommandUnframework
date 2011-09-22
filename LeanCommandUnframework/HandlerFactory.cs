using System;

namespace LeanCommandUnframework
{
    public class HandlerFactory : IHandlerFactory
    {
        public object CreateInstance(Type handlerType)
        {
            return Activator.CreateInstance(handlerType);
        }
    }
}