using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly ISCDbContext _context;

        public PermissionService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<PermissionDto>> GetAllPermissionsAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Permissions.AsQueryable();

            // Logic tìm kiếm
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerCaseQuery = searchQuery.ToLower().Trim();
                query = query.Where(p =>
                    (p.Module != null && p.Module.ToLower().Contains(lowerCaseQuery)) ||
                    p.Permissioncode.ToLower().Contains(lowerCaseQuery) ||
                    (p.Description != null && p.Description.ToLower().Contains(lowerCaseQuery))
                );
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PermissionDto
                {
                    Permissionid = p.Permissionid,
                    Module = p.Module,
                    Permissioncode = p.Permissioncode,
                    Description = p.Description
                })
                .ToListAsync();

            return new PagedResponse<PermissionDto>(pagedData, pageNumber, pageSize, totalRecords);
        }

        public async Task<PermissionDto?> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions
                .Where(p => p.Permissionid == id)
                .Select(p => new PermissionDto
                {
                    Permissionid = p.Permissionid,
                    Module = p.Module,
                    Permissioncode = p.Permissioncode,
                    Description = p.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Permission> CreatePermissionAsync(PermissionRequestDto permissionDto)
        {
            var isDuplicateCode = await _context.Permissions.AnyAsync(p => p.Permissioncode == permissionDto.Permissioncode);
            if (isDuplicateCode)
            {
                throw new InvalidOperationException($"Mã quyền '{permissionDto.Permissioncode}' đã tồn tại.");
            }

            var permission = new Permission
            {
                Module = permissionDto.Module,
                Permissioncode = permissionDto.Permissioncode,
                Description = permissionDto.Description
            };

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission?> UpdatePermissionAsync(int id, PermissionRequestDto permissionDto)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null) return null;

            var isDuplicateCode = await _context.Permissions.AnyAsync(p => p.Permissioncode == permissionDto.Permissioncode && p.Permissionid != id);
            if (isDuplicateCode)
            {
                throw new InvalidOperationException($"Mã quyền '{permissionDto.Permissioncode}' đã được sử dụng bởi một quyền khác.");
            }

            permission.Module = permissionDto.Module;
            permission.Permissioncode = permissionDto.Permissioncode;
            permission.Description = permissionDto.Description;

            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null) return false;

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}