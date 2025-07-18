// Tạo file này ở một thư mục chung, ví dụ "Helpers" hoặc "Responses"
namespace DuAnThucTap_BE01.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}