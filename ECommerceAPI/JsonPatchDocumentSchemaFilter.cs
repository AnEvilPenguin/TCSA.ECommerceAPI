using Microsoft.AspNetCore.JsonPatch;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECommerceAPI;

public class JsonPatchDocumentSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsGenericType &&
            context.Type.GetGenericTypeDefinition() == typeof(JsonPatchDocument<>))
        {
            var innerType = context.Type.GetGenericArguments()[0];
            var innerSchema = context.SchemaGenerator.GenerateSchema(innerType, context.SchemaRepository);

            var innerTypeName = innerType.Name;
            
            // Replace the schema with the inner object’s schema
            schema.Type = "object";
            schema.Title = $"JsonPatchDocument<{innerTypeName}>";
            schema.Properties = innerSchema.Properties;
            schema.Required = innerSchema.Required;
            schema.Reference = innerSchema.Reference;
        }
    }
}