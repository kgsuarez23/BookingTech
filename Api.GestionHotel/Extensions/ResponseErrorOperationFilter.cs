using Api.Manager.Application.Wrappers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.GestionHotel.Extensions
{
    public class ResponseErrorOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Verifica si ya existe una respuesta 400
            if (!operation.Responses.ContainsKey("500"))
            {
                operation.Responses.Add("500", new OpenApiResponse
                {
                    Description = "Internal Server Error",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(typeof(ResponseError<object>), context.SchemaRepository)
                        }
                    }
                });
            }
        }
    }
}
