using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface IRolePermissionService
    {
        Task<IEnumerable<RolePermissionDto>> GetAllAsync(string? searchQuery, int page, int pageSize);
        Task<RolePermissionDto?> GetByIdAsync(int roleId, int permissionId);
        Task<RolePermissionDto> CreateAsync(RolePermissionRequestDto rolePermission);
        Task<bool> DeleteAsync(int roleId, int permissionId);
    }
}