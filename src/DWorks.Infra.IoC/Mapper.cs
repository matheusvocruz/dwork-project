using Dworks.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace DWorks.Infra.IoC
{
    public static class Mapper
    {
        public static void RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToApplicationMappingProfile));
        }
    }
}
