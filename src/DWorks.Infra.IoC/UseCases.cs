using Dworks.Application.Interfaces.UseCases;
using Dworks.Application.UseCases.Comment;
using Dworks.Application.UseCases.Log;
using Dworks.Application.UseCases.Project;
using Dworks.Application.UseCases.Task;
using Microsoft.Extensions.DependencyInjection;

namespace DWorks.Infra.IoC
{
    public static class UseCases
    {
        public static void RegisterUseCases(this IServiceCollection services)
        {
            services.AddScoped<ICommentCreateUseCase, CommentCreateUseCase>();
            services.AddScoped<ILogCreateUseCase, LogCreateUseCase>();
            services.AddScoped<IProjectCreateUseCase, ProjectCreateUseCase>();
            services.AddScoped<IProjectDeleteUseCase, ProjectDeleteUseCase>();
            services.AddScoped<ITaskCreateUseCase, TaskCreateUseCase>();
            services.AddScoped<ITaskUpdateUseCase, TaskUpdateUseCase>();
            services.AddScoped<ITaskDeleteUseCase, TaskDeleteUseCase>();
        }
    }
}
