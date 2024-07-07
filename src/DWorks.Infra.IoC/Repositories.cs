using DWorks.Domain.Interfaces.Repositories;
using DWorks.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DWorks.Infra.IoC
{
    public static class Repositories
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
