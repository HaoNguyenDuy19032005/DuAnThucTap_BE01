using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> GetAllAsync(string? searchQuery, int page, int pageSize);
        Task<PermissionDto?> GetByIdAsync(int id);
        Task<PermissionDto> CreateAsync(PermissionRequestDto permission);
        Task<PermissionDto?> UpdateAsync(int id, PermissionRequestDto permission);
        Task<bool> DeleteAsync(int id);
    }
}