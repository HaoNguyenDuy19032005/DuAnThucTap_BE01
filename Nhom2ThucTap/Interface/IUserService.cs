using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IUserService
    {
        Task<(List<User> Users, int TotalCount)> GetPagedUsersAsync(int page, int pageSize);
        Task<User?> GetByIdAsync(int id);
        Task<(bool IsSuccess, string Message)> CreateAsync(User user);
        Task<(bool IsSuccess, string Message)> UpdateAsync(int id, User updatedUser);
        Task<(bool IsSuccess, string Message)> DeleteAsync(int id);
    }
}
