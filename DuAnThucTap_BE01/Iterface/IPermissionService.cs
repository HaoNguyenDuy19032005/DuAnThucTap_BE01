using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;

namespace DuAnThucTap_BE01.Iterface
{
    public interface IPermissionService
    {
        Task<PagedResponse<PermissionDto>> GetAllPermissionsAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<PermissionDto?> GetPermissionByIdAsync(int id);
        Task<Permission> CreatePermissionAsync(PermissionRequestDto permissionDto);
        Task<Permission?> UpdatePermissionAsync(int id, PermissionRequestDto permissionDto);
        Task<bool> DeletePermissionAsync(int id);
    }
}
