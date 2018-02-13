using Lily.Microservice.AppTrace.Features;

namespace Lily.Microservice.AppTrace
{
    public interface IMicroserviceFilterManager<in TFilter,in TFilterType>
        where TFilter : IMicroserviceFilter<TFilterType>
    {
        void AddFilter(TFilter microserviceFilter);
        bool ShouldBeFiltered(TFilterType filterType);
    }
}
