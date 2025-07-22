using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly ISCDbContext _context;

        public RolePermissionService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<RolePermissionDto>> GetAllAssignmentsAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            // Sửa: Rolepermission -> Rolepermissions
            var query = _context.Rolepermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerCaseQuery = searchQuery.ToLower().Trim();
                query = query.Where(rp =>
                    (rp.Role.Rolename != null && rp.Role.Rolename.ToLower().Contains(lowerCaseQuery)) ||
                    (rp.Permission.Permissioncode != null && rp.Permission.Permissioncode.ToLower().Contains(lowerCaseQuery))
                );
            }

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(rp => new RolePermissionDto
                {
                    RoleId = rp.Roleid,
                    RoleName = rp.Role.Rolename,
                    PermissionId = rp.Permissionid,
                    PermissionCode = rp.Permission.Permissioncode,
                    PermissionModule = rp.Permission.Module
                })
                .ToListAsync();

            return new PagedResponse<RolePermissionDto>(data, pageNumber, pageSize, totalRecords);
        }

        public async Task<(bool Succeeded, string? ErrorMessage, RolePermission? Assignment)> AssignPermissionToRoleAsync(RolePermissionRequestDto request)
        {
            // Sửa: Rolepermission -> Rolepermissions
            var assignmentExists = await _context.Rolepermissions.AnyAsync(rp =>
                rp.Roleid == request.RoleId && rp.Permissionid == request.PermissionId);

            if (assignmentExists)
            {
                return (false, "Quyền này đã được gán cho vai trò.", null);
            }

            var assignment = new RolePermission
            {
                Roleid = request.RoleId,
                Permissionid = request.PermissionId
            };

            // Sửa: Rolepermission -> Rolepermissions
            await _context.Rolepermissions.AddAsync(assignment);
            await _context.SaveChangesAsync();

            return (true, null, assignment);
        }

        public async Task<bool> RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            // Sửa: Rolepermission -> Rolepermissions
            var assignment = await _context.Rolepermissions
                .FirstOrDefaultAsync(rp => rp.Roleid == roleId && rp.Permissionid == permissionId);

            if (assignment == null)
            {
                return false;
            }

            // Sửa: Rolepermission -> Rolepermissions
            _context.Rolepermissions.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}