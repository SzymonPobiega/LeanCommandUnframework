using System;

namespace LeanCommandUnframework
{
    public class ObjectFactory : IObjectFactory
    {
        public object CreateInstance(Type handlerType)
        {
            return Activator.CreateInstance(handlerType);
        }
    }
}