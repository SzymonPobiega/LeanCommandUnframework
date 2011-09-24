using System;
using System.Collections.Generic;

namespace LeanCommandUnframework
{
    public class FilterSelector : TypeSelector
    {
        public FilterSelector(params Type[] filterTypes)
            : base(filterTypes)
        {
        }

        public FilterSelector(IEnumerable<Type> filterTypes)
            : base(filterTypes)
        {
        }

        public IEnumerable<Type> GetFiltersFor(Type commandType)
        {
            return FinaMatching(commandType);
        }

        protected override bool MatchesType(Type genericArgument, Type commandType)
        {
            return genericArgument.IsAssignableFrom(commandType);
        }
    }
}