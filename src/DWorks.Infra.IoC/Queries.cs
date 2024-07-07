using Dworks.Application.Interfaces.Queries;
using Dworks.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DWorks.Infra.IoC
{
    public static class Queries
    {
        public static void RegisterQueries(this IServiceCollection services)
        {
            services.AddScoped<IProjectQueries, ProjectQueries>();
            services.AddScoped<ITaskQueries, TaskQueries>();
            services.AddScoped<IUserQueries, UserQueries>();
        }
    }
}
