using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IRoleService
    {
        Task<(List<Role> Roles, int TotalCount)> GetPagedRolesAsync(int page, int pageSize);
        Task<Role?> GetByIdAsync(int id);
        Task<(bool IsSuccess, string Message)> CreateAsync(Role role);
        Task<(bool IsSuccess, string Message)> UpdateAsync(int id, Role updatedRole);
        Task<(bool IsSuccess, string Message)> DeleteAsync(int id);
    }
}
