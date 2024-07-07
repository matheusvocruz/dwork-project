using DWorks.Infra.IoC.Filters;
using Microsoft.OpenApi.Models;

namespace DWorks.Api.Configuration
{
    public static class Swagger
    {
        public static void RegisterSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DWorks - Project",
                    Description = "DWorks - Project documentation",
                    Contact = new OpenApiContact() { Name = "Contact", Email = "contact@mail.com" }
                });

                c.SchemaFilter<EnumSchemaFilter>();
                c.OperationFilter<AddRequiredHeaderParameter>();
                c.CustomSchemaIds(x => x.FullName);

                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
            });
        }

        public static void UseSwaggerConfig(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "v1");
                    c.RoutePrefix = "";
                });
            }
        }
    }
}
