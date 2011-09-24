using System;

namespace LeanCommandUnframework
{
    public class DefaultObjectFactory : IObjectFactory
    {
        public object GetHandlerInstance(Type handlerType)
        {
            return Activator.CreateInstance(handlerType);
        }

        public object GetFilterInstance(Type filterType)
        {
            return Activator.CreateInstance(filterType);
        }
    }
}