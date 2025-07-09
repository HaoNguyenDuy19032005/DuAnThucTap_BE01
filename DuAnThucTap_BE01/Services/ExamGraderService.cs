using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class ExamGraderService : IExamGraderService
    {
        private readonly ISCDbContext _context;

        public ExamGraderService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Examgrader> CreateAsync(Examgrader examGrader)
        {
            _context.Examgraders.Add(examGrader);
            await _context.SaveChangesAsync();
            return await _context.Examgraders
                .AsNoTracking()
                .FirstOrDefaultAsync(eg => eg.Examgraderid == examGrader.Examgraderid) ?? examGrader;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var examGrader = await _context.Examgraders.FindAsync(id);
            if (examGrader == null) return false;
            _context.Examgraders.Remove(examGrader);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Examgrader>> GetAllAsync()
        {
            return await _context.Examgraders
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Examgrader?> GetByIdAsync(int id)
        {
            return await _context.Examgraders
                .AsNoTracking()
                .FirstOrDefaultAsync(eg => eg.Examgraderid == id);
        }

        public async Task<Examgrader?> UpdateAsync(int id, Examgrader updatedExamGrader)
        {
            var existingExamGrader = await _context.Examgraders.FindAsync(id);
            if (existingExamGrader == null) return null;

            existingExamGrader.Examscheduleid = updatedExamGrader.Examscheduleid;
            existingExamGrader.Teacherid = updatedExamGrader.Teacherid;

            await _context.SaveChangesAsync();
            return await _context.Examgraders
                .AsNoTracking()
                .FirstOrDefaultAsync(eg => eg.Examgraderid == id);
        }
    }
}