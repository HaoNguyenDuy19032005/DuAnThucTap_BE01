using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class UserThreadReadStatusService : IUserThreadReadStatusService
    {
        private readonly AppDbContext _context;

        public UserThreadReadStatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Userthreadreadstatus>> GetAllAsync()
        {
            return await _context.Userthreadreadstatuses
                .Include(s => s.User)
                .Include(s => s.Thread)
                .ToListAsync();
        }

        public async Task<Userthreadreadstatus?> GetByIdAsync(int userId, int threadId)
        {
            return await _context.Userthreadreadstatuses
                .Include(s => s.User)
                .Include(s => s.Thread)
                .FirstOrDefaultAsync(s => s.Userid == userId && s.Threadid == threadId);
        }

        public async Task<(bool IsSuccess, string Message)> CreateAsync(Userthreadreadstatus entity)
        {
            bool exists = await _context.Userthreadreadstatuses
                .AnyAsync(s => s.Userid == entity.Userid && s.Threadid == entity.Threadid);

            if (exists)
                return (false, "Bản ghi đã tồn tại");

            entity.Lastreadtimestamp ??= DateTime.UtcNow;

            _context.Userthreadreadstatuses.Add(entity);
            await _context.SaveChangesAsync();

            return (true, "Tạo bản ghi thành công");
        }

        public async Task<(bool IsSuccess, string Message)> UpdateAsync(int userId, int threadId, DateTime? lastRead)
        {
            var existing = await _context.Userthreadreadstatuses
                .FirstOrDefaultAsync(s => s.Userid == userId && s.Threadid == threadId);

            if (existing == null)
                return (false, "Không tìm thấy bản ghi");

            existing.Lastreadtimestamp = lastRead ?? DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return (true, "Cập nhật thành công");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteAsync(int userId, int threadId)
        {
            var existing = await _context.Userthreadreadstatuses
                .FirstOrDefaultAsync(s => s.Userid == userId && s.Threadid == threadId);

            if (existing == null)
                return (false, "Không tìm thấy bản ghi");

            _context.Userthreadreadstatuses.Remove(existing);
            await _context.SaveChangesAsync();

            return (true, "Xóa bản ghi thành công");
        }
    }
}
