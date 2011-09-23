using System;
using System.Collections.Generic;
using System.Linq;

namespace LeanCommandUnframework
{
    public abstract class TypeCollection
    {
        private readonly IEnumerable<Type> _types;

        protected TypeCollection(params Type[] types)
            : this((IEnumerable<Type>)types)
        {
        }

        protected TypeCollection(IEnumerable<Type> types)
        {
            _types = types;
        }

        public Type GetFor(Type commandType)
        {
            var handlerType = _types.FirstOrDefault(x => ImplementsProperInterface(x, commandType));
            if (handlerType == null)
            {
                throw new InvalidOperationException("Could not find handler for command "+commandType.FullName);
            }
            return handlerType;
        }

        private bool ImplementsProperInterface(Type candidateHandler, Type commandType)
        {
            var interfaces = candidateHandler.GetInterfaces();
            return interfaces.Any(x => IsProperInterface(x, commandType));
        }

        private bool IsProperInterface(Type candidateInterface, Type commandType)
        {
            return candidateInterface.IsGenericType &&
                   candidateInterface.GetGenericArguments().Length == 1 &&
                   MatchesType(candidateInterface.GetGenericArguments()[0], commandType);
        }

        protected abstract bool MatchesType(Type genericArgument, Type commandType);
    }
}