using System.Collections.Generic;
using Lily.Microservice.AppTrace.Features;

namespace Lily.Microservice.AppTrace.Imp
{
    public class MicroserviceFilterManager<TFilter,TFilterType> : IMicroserviceFilterManager<TFilter, TFilterType>
        where TFilter : IMicroserviceFilter<TFilterType>
    {
        private readonly List<TFilter> _filters=new List<TFilter>();

        public void AddFilter(TFilter microserviceFilter)
        {
            if (!_filters.Contains(microserviceFilter))
            {
                _filters.Add(microserviceFilter);
            }
        }

        public bool ShouldBeFiltered(TFilterType filterType)
        {
            return _filters.Exists(item => item.ShouldBeFiltered(filterType));
        }
    }
}
