using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IUserThreadReadStatusService
    {
        Task<List<Userthreadreadstatus>> GetAllAsync();
        Task<Userthreadreadstatus?> GetByIdAsync(int userId, int threadId);
        Task<(bool IsSuccess, string Message)> CreateAsync(Userthreadreadstatus entity);
        Task<(bool IsSuccess, string Message)> UpdateAsync(int userId, int threadId, DateTime? lastRead);
        Task<(bool IsSuccess, string Message)> DeleteAsync(int userId, int threadId);
    }
}
