using System;

namespace LeanCommandUnframework
{
    public interface IObjectFactory
    {
        object GetHandlerInstance(Type handlerType);
        object GetFilterInstance(Type filterType);
    }
}