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
    public class PermissionService : IPermissionService
    {
        private readonly ISCDbContext _context;

        public PermissionService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllAsync(string? searchQuery, int page, int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                throw new ArgumentException("Trang hoặc kích thước trang không hợp lệ.");
            }

            var query = _context.Permissions
                .Select(p => new PermissionDto
                {
                    PermissionId = p.PermissionId,
                    Module = p.Module,
                    PermissionCode = p.PermissionCode,
                    Description = p.Description
                });

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(p => (p.Module != null && p.Module.ToLower().Contains(searchQuery)) ||
                                         p.PermissionCode.ToLower().Contains(searchQuery));
            }

            query = query.OrderBy(p => p.PermissionId).Skip((page - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<PermissionDto?> GetByIdAsync(int id)
        {
            return await _context.Permissions
                .Where(p => p.PermissionId == id)
                .Select(p => new PermissionDto
                {
                    PermissionId = p.PermissionId,
                    Module = p.Module,
                    PermissionCode = p.PermissionCode,
                    Description = p.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PermissionDto> CreateAsync(PermissionRequestDto permission)
        {
            if (await _context.Permissions.AnyAsync(p => p.PermissionCode == permission.PermissionCode))
            {
                throw new ArgumentException("Mã quyền đã tồn tại.");
            }

            var newPermission = new Permission
            {
                Module = permission.Module,
                PermissionCode = permission.PermissionCode,
                Description = permission.Description
            };

            _context.Permissions.Add(newPermission);
            await _context.SaveChangesAsync();

            return new PermissionDto
            {
                PermissionId = newPermission.PermissionId,
                Module = newPermission.Module,
                PermissionCode = newPermission.PermissionCode,
                Description = newPermission.Description
            };
        }

        public async Task<PermissionDto?> UpdateAsync(int id, PermissionRequestDto permission)
        {
            var existing = await _context.Permissions.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            if (await _context.Permissions.AnyAsync(p => p.PermissionCode == permission.PermissionCode && p.PermissionId != id))
            {
                throw new ArgumentException("Mã quyền đã tồn tại.");
            }

            existing.Module = permission.Module;
            existing.PermissionCode = permission.PermissionCode;
            existing.Description = permission.Description;

            await _context.SaveChangesAsync();

            return new PermissionDto
            {
                PermissionId = existing.PermissionId,
                Module = existing.Module,
                PermissionCode = existing.PermissionCode,
                Description = existing.Description
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                return false;
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}