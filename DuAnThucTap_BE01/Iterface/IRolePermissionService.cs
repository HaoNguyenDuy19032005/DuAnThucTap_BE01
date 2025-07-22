using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;

namespace DuAnThucTap_BE01.Iterface
{
    public interface IRolePermissionService
    {
        Task<PagedResponse<RolePermissionDto>> GetAllAssignmentsAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<(bool Succeeded, string? ErrorMessage, RolePermission? Assignment)> AssignPermissionToRoleAsync(RolePermissionRequestDto request);
        Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
