using Microsoft.Extensions.DependencyInjection;

namespace Lily.Microservice.Microparts.Imp
{
    public class MicropartServiceCollection: IMicropartServiceCollection
    {
        public IServiceCollection ApplicationServiceCollection { get; set; }

        public MicropartServiceCollection(IServiceCollection serviceCollection)
        {
            ApplicationServiceCollection = serviceCollection;
        }
        public IMicropartServiceCollection AddMicroservice()
        {
            return this;
        }
    }
}
