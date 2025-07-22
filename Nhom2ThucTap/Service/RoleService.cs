using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;

        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Role> Roles, int TotalCount)> GetPagedRolesAsync(int page, int pageSize)
        {
            var query = _context.Roles.AsQueryable();
            var totalCount = await query.CountAsync();
            var roles = await query
                .OrderBy(r => r.Roleid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (roles, totalCount);
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Roleid == id);
        }

        public async Task<(bool IsSuccess, string Message)> CreateAsync(Role role)
        {
            if (string.IsNullOrWhiteSpace(role.Rolename))
                return (false, "Tên vai trò không được để trống");

            // Kiểm tra trùng tên vai trò
            bool exists = await _context.Roles.AnyAsync(r => r.Rolename == role.Rolename);
            if (exists)
                return (false, "Tên vai trò đã tồn tại");

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return (true, "Tạo vai trò thành công");
        }

        public async Task<(bool IsSuccess, string Message)> UpdateAsync(int id, Role updatedRole)
        {
            var existingRole = await _context.Roles.FindAsync(id);
            if (existingRole == null)
                return (false, "Không tìm thấy vai trò");

            if (string.IsNullOrWhiteSpace(updatedRole.Rolename))
                return (false, "Tên vai trò không được để trống");

            // Kiểm tra trùng tên với role khác
            bool exists = await _context.Roles.AnyAsync(r => r.Rolename == updatedRole.Rolename && r.Roleid != id);
            if (exists)
                return (false, "Tên vai trò đã tồn tại");

            existingRole.Rolename = updatedRole.Rolename;
            existingRole.Description = updatedRole.Description;

            await _context.SaveChangesAsync();
            return (true, "Cập nhật vai trò thành công");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return (false, "Không tìm thấy vai trò");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return (true, "Xóa vai trò thành công");
        }
    }
}
