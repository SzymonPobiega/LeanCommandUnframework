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

        protected IEnumerable<Type> FinaMatching(Type commandType)
        {
            return _types.Where(x => ImplementsProperInterface(x, commandType));
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