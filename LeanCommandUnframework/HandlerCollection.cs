using System;
using System.Collections.Generic;
using System.Reflection;

namespace LeanCommandUnframework
{
    public class HandlerCollection : TypeCollection
    {
        public HandlerCollection(params Type[] handlerTypes)
            : base(handlerTypes)
        {
        }

        public HandlerCollection(IEnumerable<Type> handlerTypes)
            : base(handlerTypes)
        {
        }

        protected override bool IsProperMethod(MethodInfo candidateMethod, Type commandType)
        {
            return HasProperName(candidateMethod) && HasOneParametrOfType(candidateMethod, commandType);
        }

        private static bool HasProperName(MethodInfo candidateMethod)
        {
            return candidateMethod.Name == "Handle";
        }

        private static bool HasOneParametrOfType(MethodInfo candidateMethod, Type commandType)
        {
            var parameters = candidateMethod.GetParameters();

            return parameters.Length == 1 &&
                   parameters[0].ParameterType == commandType;
        }
    }
}