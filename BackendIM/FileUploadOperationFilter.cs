using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if the action method has parameters of type IFormFile
        foreach (var parameter in context.ApiDescription.ActionDescriptor.Parameters)
        {
            if (parameter.ParameterType == typeof(IFormFile))
            {
                // Define a new OpenApiMediaType for file uploads
                var fileUpload = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary" // Specify the format for file upload
                    }
                };

                // Add the file parameter as part of the request body
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        { "multipart/form-data", fileUpload }
                    }
                };
            }
        }
    }
}
