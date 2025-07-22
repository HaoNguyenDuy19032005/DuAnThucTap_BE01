using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly ISCDbContext _context;

        public RolePermissionService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolePermissionDto>> GetAllAsync(string? searchQuery, int page, int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                throw new ArgumentException("Trang hoặc kích thước trang không hợp lệ.");
            }

            var query = _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .Select(rp => new RolePermissionDto
                {
                    RoleId = rp.RoleId,
                    RoleName = rp.Role != null ? rp.Role.RoleName : null,
                    PermissionId = rp.PermissionId,
                    PermissionCode = rp.Permission != null ? rp.Permission.PermissionCode : null
                });

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(rp => (rp.RoleName != null && rp.RoleName.ToLower().Contains(searchQuery)) ||
                                         (rp.PermissionCode != null && rp.PermissionCode.ToLower().Contains(searchQuery)));
            }

            query = query.OrderBy(rp => rp.RoleId).ThenBy(rp => rp.PermissionId).Skip((page - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<RolePermissionDto?> GetByIdAsync(int roleId, int permissionId)
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .Where(rp => rp.RoleId == roleId && rp.PermissionId == permissionId)
                .Select(rp => new RolePermissionDto
                {
                    RoleId = rp.RoleId,
                    RoleName = rp.Role != null ? rp.Role.RoleName : null,
                    PermissionId = rp.PermissionId,
                    PermissionCode = rp.Permission != null ? rp.Permission.PermissionCode : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<RolePermissionDto> CreateAsync(RolePermissionRequestDto rolePermission)
        {
            var role = await _context.Roles.FindAsync(rolePermission.RoleId);
            if (role == null)
            {
                throw new ArgumentException("Vai trò không tồn tại.");
            }

            var permission = await _context.Permissions.FindAsync(rolePermission.PermissionId);
            if (permission == null)
            {
                throw new ArgumentException("Quyền không tồn tại.");
            }

            var existing = await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == rolePermission.RoleId && rp.PermissionId == rolePermission.PermissionId);
            if (existing)
            {
                throw new ArgumentException("Quan hệ giữa vai trò và quyền đã tồn tại.");
            }

            var newRolePermission = new RolePermission
            {
                RoleId = rolePermission.RoleId,
                PermissionId = rolePermission.PermissionId
            };

            _context.RolePermissions.Add(newRolePermission);
            await _context.SaveChangesAsync();

            return new RolePermissionDto
            {
                RoleId = newRolePermission.RoleId,
                RoleName = role.RoleName,
                PermissionId = newRolePermission.PermissionId,
                PermissionCode = permission.PermissionCode
            };
        }

        public async Task<bool> DeleteAsync(int roleId, int permissionId)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            if (rolePermission == null)
            {
                return false;
            }

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}