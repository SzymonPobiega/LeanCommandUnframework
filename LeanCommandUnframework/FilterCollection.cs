using System;
using System.Collections.Generic;

namespace LeanCommandUnframework
{
    public class FilterCollection : TypeCollection
    {
        public FilterCollection(params Type[] filterTypes)
            : base(filterTypes)
        {
        }

        public FilterCollection(IEnumerable<Type> filterTypes)
            : base(filterTypes)
        {
        }

        protected override bool MatchesType(Type genericArgument, Type commandType)
        {
            return genericArgument == commandType;
        }
    }
}