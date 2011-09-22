using System;

namespace LeanCommandUnframework
{
    public interface IObjectFactory
    {
        object CreateInstance(Type handlerType);
    }
}