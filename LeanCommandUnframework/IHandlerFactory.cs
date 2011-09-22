using System;

namespace LeanCommandUnframework
{
    public interface IHandlerFactory
    {
        object CreateInstance(Type handlerType);
    }
}