using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace DWorks.Infra.IoC.Filters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();

                StringBuilder enumValues = new StringBuilder();
                foreach (var item in Enum.GetValues(context.Type))
                {
                    var value = (int)item;
                    var name = Enum.GetName(context.Type, item);

                    enumValues.Append($"{value} - {name} ");
                }

                schema.Enum.Add(new OpenApiString(enumValues.ToString()));
            }
        }
    }
}
