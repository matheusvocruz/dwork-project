using Dworks.Application.Pipes;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DWorks.Infra.IoC
{
    public static class MediatR
    {
        public static void RegisterMediatR(this IServiceCollection services)
        {
            var assembly = GetAssembly();

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            RegisterPipelineAndMediatR(services, assembly);
        }

        private static Assembly GetAssembly()
        {
            const string applicationAssemblyName = "Dworks.Application";
            return AppDomain.CurrentDomain.Load(applicationAssemblyName);
        }

        private static void RegisterPipelineAndMediatR(this IServiceCollection services, Assembly assembly)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        }
    }
}
