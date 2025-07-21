using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentSemesterSummaryService : IStudentSemesterSummaryService
    {
        private readonly AppDbContext _context;

        public StudentSemesterSummaryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Studentsemestersummary>> GetAllAsync()
        {
            return await _context.Studentsemestersummaries
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<Studentsemestersummary> Data, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Studentsemestersummaries
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(s => s.Calculateddate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<Studentsemestersummary?> GetByIdAsync(int id)
        {
            return await _context.Studentsemestersummaries
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Summaryid == id);
        }

        public async Task<Studentsemestersummary> AddAsync(Studentsemestersummary summary)
        {
            // Không gán thủ công các trường trigger tự tính
            summary.Averagescore = null;
            summary.Performancerating = null;
            summary.Conductrating = null;
            summary.Calculateddate = null;

            _context.Studentsemestersummaries.Add(summary);
            await _context.SaveChangesAsync();
            return summary;
        }

        public async Task<Studentsemestersummary?> UpdateAsync(int id, Studentsemestersummary summary)
        {
            var existing = await _context.Studentsemestersummaries.FindAsync(id);
            if (existing == null) return null;

            existing.Studentid = summary.Studentid;
            existing.Semesterid = summary.Semesterid;
            existing.Schoolinfoid = summary.Schoolinfoid;

            // Không gán thủ công các trường trigger tự xử lý
            existing.Averagescore = null;
            existing.Performancerating = null;
            existing.Conductrating = null;
            existing.Calculateddate = null;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Studentsemestersummaries.FindAsync(id);
            if (entity == null) return false;

            _context.Studentsemestersummaries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
