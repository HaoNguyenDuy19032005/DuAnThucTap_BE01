using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentTransferReceiptService : IStudentTransferReceiptService
    {
        private readonly AppDbContext _context;

        public StudentTransferReceiptService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentTransferReceipt>> GetAllAsync()
        {
            return await _context.StudentTransferReceipts
                .Include(r => r.Semester)
                .ToListAsync();
        }

        public async Task<(List<StudentTransferReceipt> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.StudentTransferReceipts.Include(r => r.Semester);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }

        public async Task<StudentTransferReceipt?> GetByIdAsync(int id)
        {
            return await _context.StudentTransferReceipts
                .Include(r => r.Semester)
                .FirstOrDefaultAsync(r => r.ReceiptId == id);
        }

        public async Task<StudentTransferReceipt> CreateAsync(StudentTransferReceipt receipt)
        {
            _context.StudentTransferReceipts.Add(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<bool> UpdateAsync(int id, StudentTransferReceipt receipt)
        {
            var existing = await _context.StudentTransferReceipts.FindAsync(id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(receipt);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var receipt = await _context.StudentTransferReceipts.FindAsync(id);
            if (receipt == null) return false;

            _context.StudentTransferReceipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Thêm kiểm tra semester có tồn tại
        public async Task<bool> ExistsSemesterAsync(int semesterId)
        {
            return await _context.Semesters.AnyAsync(s => s.Semesterid == semesterId);
        }

    }
}
