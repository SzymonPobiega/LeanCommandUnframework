using System;
using System.Collections.Generic;

namespace LeanCommandUnframework
{
    public class FilterCollection
    {
        private readonly IEnumerable<object> _filters;

        public FilterCollection(IEnumerable<object> filters)
        {
            this._filters = filters;
        }

        public void OnHandling(dynamic command)
        {
            foreach (dynamic filter in _filters)
            {
                filter.OnHandling(command);
            }
        }

        public void OnHandled(dynamic command, object result)
        {
            foreach (dynamic filter in _filters)
            {
                filter.OnHandled(command, result);
            }
        }
    }
}