using System;
using System.Collections.Generic;

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

        protected override bool MatchesType(Type genericArgument, Type commandType)
        {
            return genericArgument == commandType;
        }
    }
}