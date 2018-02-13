using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts
{
    public interface IMicropartServiceCollection
    {
        IServiceCollection ApplicationServiceCollection { get; set; }
        IMicropartServiceCollection AddMicroservice();
    }
}
