using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhom2ThucTap.Services
{
    public class StudentsubjectsummaryService : IStudentsubjectsummaryService
    {
        private readonly AppDbContext _context;

        public StudentsubjectsummaryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Studentsubjectsummary>> GetAllAsync()
        {
            return await _context.Studentsubjectsummaries
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .ToListAsync();
        }

        public async Task<(List<Studentsubjectsummary> Summaries, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Studentsubjectsummaries
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .AsQueryable();

            var totalCount = await query.CountAsync();

            var summaries = await query
                .OrderByDescending(s => s.Subjectsummaryid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (summaries, totalCount);
        }

        public async Task<Studentsubjectsummary?> GetByIdAsync(int id)
        {
            return await _context.Studentsubjectsummaries
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .FirstOrDefaultAsync(s => s.Subjectsummaryid == id);
        }

        public async Task<Studentsubjectsummary> AddAsync(Studentsubjectsummary summary)
        {
            _context.Studentsubjectsummaries.Add(summary);
            await _context.SaveChangesAsync();
            return summary;
        }

        public async Task<Studentsubjectsummary?> UpdateAsync(int id, Studentsubjectsummary summary)
        {
            var existing = await _context.Studentsubjectsummaries.FindAsync(id);
            if (existing == null) return null;

            existing.Studentid = summary.Studentid;
            existing.Subjectid = summary.Subjectid;
            existing.Semesterid = summary.Semesterid;
            existing.Schoolinfoid = summary.Schoolinfoid;
            existing.Averagescore = summary.Averagescore;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Studentsubjectsummaries.FindAsync(id);
            if (entity == null) return false;

            _context.Studentsubjectsummaries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
