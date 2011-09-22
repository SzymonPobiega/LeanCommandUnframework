using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LeanCommandUnframework
{
    public abstract class TypeCollection
    {
        private readonly IEnumerable<Type> _handlerTypes;

        protected TypeCollection(params Type[] handlerTypes)
            : this((IEnumerable<Type>)handlerTypes)
        {
        }

        protected TypeCollection(IEnumerable<Type> handlerTypes)
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

        private bool HasMethodForHandling(Type candidateHandler, Type commandType)
        {
            var candidateMethods = candidateHandler.GetMethods();
            return candidateMethods.Any(x => IsProperMethod(x, commandType));
        }

        protected abstract bool IsProperMethod(MethodInfo x, Type commandType);
    }
}