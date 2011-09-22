using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LeanCommandUnframework
{
    public class HandlerCollection
    {
        private readonly IEnumerable<Type> _handlerTypes;

        public HandlerCollection(params Type[] handlerTypes)
            : this((IEnumerable<Type>)handlerTypes)
        {
        }

        public HandlerCollection(IEnumerable<Type> handlerTypes)
        {
            _handlerTypes = handlerTypes;
        }

        public Type GetHandlerFor(Type commandType)
        {
            var handlerType = _handlerTypes.FirstOrDefault(x => HasMethodForHandling(x, commandType));
            if (handlerType == null)
            {
                throw new InvalidOperationException("Could not find handler for command "+commandType.FullName);
            }
            return handlerType;
        }

        private static bool HasMethodForHandling(Type candidateHandler, Type commandType)
        {
            var candidateMethods = candidateHandler.GetMethods();
            return candidateMethods.Any(x => HasProperName(x) && HasOneParametrOfType(x, commandType));
        }

        private static bool HasProperName(MethodInfo x)
        {
            return x.Name == "Handle";
        }

        private static bool HasOneParametrOfType(MethodInfo candidateMethod, Type commandType)
        {
            var parameters = candidateMethod.GetParameters();

            return parameters.Length == 1 && 
                parameters[0].ParameterType == commandType;
        }
    }
}