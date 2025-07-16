using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


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

            return examGrader;
        }

        public async Task<bool> DeleteAsync(int examGraderId)
        {
            var examGrader = await _context.Examgraders.FindAsync(examGraderId);
            if (examGrader == null) return false;

            _context.Examgraders.Remove(examGrader);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Examgrader>> GetAllAsync()
        {
            return await _context.Examgraders
                .Include(eg => eg.Examschedule)
                .Include(eg => eg.Teacher)
                .ToListAsync();
        }

        public async Task<Examgrader?> GetByIdAsync(int examGraderId)
        {
            return await _context.Examgraders
                .Include(eg => eg.Examschedule)
                .Include(eg => eg.Teacher)
                .FirstOrDefaultAsync(eg => eg.Examgraderid == examGraderId);
        }

        public async Task<Examgrader?> UpdateAsync(int examGraderId, Examgrader updatedExamGrader)
        {
            var existingExamGrader = await _context.Examgraders.FindAsync(examGraderId);

            if (existingExamGrader == null) return null;

            existingExamGrader.Examscheduleid = updatedExamGrader.Examscheduleid;
            existingExamGrader.Teacherid = updatedExamGrader.Teacherid;

            await _context.SaveChangesAsync();

            return existingExamGrader;
        }
    }
}