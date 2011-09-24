using System;
using System.Collections.Generic;
using System.Linq;

namespace LeanCommandUnframework
{
    public abstract class TypeSelector
    {
        private readonly IEnumerable<Type> _types;

        protected TypeSelector(params Type[] types)
            : this((IEnumerable<Type>)types)
        {
        }

        protected TypeSelector(IEnumerable<Type> types)
        {
            _types = types;
        }

        protected IEnumerable<Type> FindMatching(Type openGenericInterfaceType, Type commandType)
        {
            return _types.Where(x => ImplementsProperInterface(x, openGenericInterfaceType, commandType));
        }

        private bool ImplementsProperInterface(Type candidateHandler, Type openGenericInterfaceType, Type commandType)
        {
            var interfaces = candidateHandler.GetInterfaces();
            return interfaces.Any(x => IsProperInterface(x, openGenericInterfaceType, commandType));
        }

        private bool IsProperInterface(Type candidateInterface, Type openGenericInterfaceType, Type commandType)
        {
            return candidateInterface.IsGenericType &&
                   candidateInterface.GetGenericTypeDefinition() == openGenericInterfaceType &&
                   candidateInterface.GetGenericArguments().Length == 1 &&
                   MatchesType(candidateInterface.GetGenericArguments()[0], commandType);
        }

        protected abstract bool MatchesType(Type genericArgument, Type commandType);
    }
}