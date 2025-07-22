using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IUserNotificationService
    {
        Task<(List<Usernotification> Notifications, int TotalCount)> GetPagedAsync(int page, int pageSize, int? userId = null);
        Task<Usernotification?> GetByIdAsync(int id);
        Task<(bool IsSuccess, string Message)> CreateAsync(Usernotification notification);
        Task<(bool IsSuccess, string Message)> UpdateAsync(int id, bool isRead);
        Task<(bool IsSuccess, string Message)> DeleteAsync(int id);
    }
}
