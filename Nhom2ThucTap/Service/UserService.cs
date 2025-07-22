using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<User> Users, int TotalCount)> GetPagedUsersAsync(int page, int pageSize)
        {
            var query = _context.Users.Include(u => u.Role).AsQueryable();

            var totalCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Userid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Userid == id);
        }

        public async Task<(bool IsSuccess, string Message)> CreateAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
                return (false, "Email không được để trống");
            if (string.IsNullOrWhiteSpace(user.Passwordhash))
                return (false, "Mật khẩu không được để trống");

            // Kiểm tra trùng email
            bool exists = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (exists)
                return (false, "Email đã tồn tại");

            // Kiểm tra RoleID có tồn tại
            bool roleExists = await _context.Roles.AnyAsync(r => r.Roleid == user.Roleid);
            if (!roleExists)
                return (false, "Vai trò không tồn tại");

            // Nếu chưa có IsActive thì mặc định là true
            if (!user.Isactive.HasValue)
                user.Isactive = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return (true, "Tạo tài khoản thành công");
        }

        public async Task<(bool IsSuccess, string Message)> UpdateAsync(int id, User updatedUser)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return (false, "Không tìm thấy người dùng");

            if (string.IsNullOrWhiteSpace(updatedUser.Email))
                return (false, "Email không được để trống");

            // Kiểm tra email trùng với user khác
            bool exists = await _context.Users.AnyAsync(u => u.Email == updatedUser.Email && u.Userid != id);
            if (exists)
                return (false, "Email đã tồn tại");

            // Kiểm tra RoleID có tồn tại
            bool roleExists = await _context.Roles.AnyAsync(r => r.Roleid == updatedUser.Roleid);
            if (!roleExists)
                return (false, "Vai trò không tồn tại");

            existingUser.Fullname = updatedUser.Fullname;
            existingUser.Email = updatedUser.Email;
            existingUser.Passwordhash = updatedUser.Passwordhash;
            existingUser.Roleid = updatedUser.Roleid;
            existingUser.Isactive = updatedUser.Isactive ?? true;
            existingUser.Teacherid = updatedUser.Teacherid;
            existingUser.Studentid = updatedUser.Studentid;

            await _context.SaveChangesAsync();
            return (true, "Cập nhật người dùng thành công");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return (false, "Không tìm thấy người dùng");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return (true, "Xóa người dùng thành công");
        }
    }
}
