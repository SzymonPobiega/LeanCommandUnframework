using System;
using System.Collections.Generic;
using System.Linq;

namespace LeanCommandUnframework
{
    public class HandlerSelector : TypeSelector
    {
        public HandlerSelector(params Type[] handlerTypes)
            : base(handlerTypes)
        {
        }

        public HandlerSelector(IEnumerable<Type> handlerTypes)
            : base(handlerTypes)
        {
        }

        public Type GetHandlerFor(Type commandType)
        {
            return FinaMatching(commandType).Single();
        }

        protected override bool MatchesType(Type genericArgument, Type commandType)
        {
            return genericArgument == commandType;
        }
    }
}