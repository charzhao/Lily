namespace Lily.Microservice.AppTrace.Features
{
    public interface IMicroserviceFilter<in T>
    {
        bool ShouldBeFiltered(T request);
    }
}
