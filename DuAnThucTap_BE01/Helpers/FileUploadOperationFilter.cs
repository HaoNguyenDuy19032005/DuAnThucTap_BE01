using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DuAnThucTap_BE01.Helpers
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType.IsAssignableTo(typeof(IFormFile)) || p.ParameterType.IsAssignableTo(typeof(IFormFileCollection)))
                .ToList();

            if (!formFileParams.Any())
            {
                return;
            }

            if (operation.RequestBody == null || !operation.RequestBody.Content.ContainsKey("multipart/form-data"))
            {
                return;
            }

            var mediaType = operation.RequestBody.Content["multipart/form-data"];

            // Với mỗi tham số là file, thêm hoặc ghi đè thuộc tính trong schema
            // để đảm bảo nó có đúng định dạng "binary"
            foreach (var fileParam in formFileParams)
            {
                // Thêm/ghi đè thuộc tính có tên trùng với tên tham số (ví dụ: "file")
                mediaType.Schema.Properties[fileParam.Name] = new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                };
            }
        }
    }
}
