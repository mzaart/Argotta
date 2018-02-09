using System.Linq;
using Multilang.Models.Jwt;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Multilang.Docs
{
    public class HideParamsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null || !operation.Parameters.Any())
            {
                return;
            }

            context.ApiDescription.ParameterDescriptions
                .Where(desc => desc.Type == typeof(JwtBody))
                .ToList()
                .ForEach(param =>
                {
                    var toRemove = operation.Parameters
                        .SingleOrDefault(p => p.Name == param.Name);

                    if (toRemove != null)
                    {
                        operation.Parameters.Remove(toRemove);
                    }
                });
        }
    }
}