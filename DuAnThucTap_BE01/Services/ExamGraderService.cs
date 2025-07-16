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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Examgrader> CreateAsync(Examgrader examGrader)
        {
            // Kiểm tra xem Examscheduleid có hợp lệ không
            var examschedule = await _context.Examschedules
                .AsNoTracking()
                .FirstOrDefaultAsync(es => es.Examscheduleid == examGrader.Examscheduleid);
            if (examschedule == null)
            {
                throw new InvalidOperationException("Examschedule not found.");
            }

            // Kiểm tra xem Teacherid có hợp lệ không (giả định bảng Teachers tồn tại)
            var teacher = await _context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Teacherid == examGrader.Teacherid);
            if (teacher == null)
            {
                throw new InvalidOperationException("Teacher not found.");
            }

            _context.Examgraders.Add(examGrader);
            await _context.SaveChangesAsync();

            return await _context.Examgraders
                .AsNoTracking()
                .Include(eg => eg.Examschedule)
                .Include(eg => eg.Teacher)
                .FirstOrDefaultAsync(eg => eg.Examgraderid == examGrader.Examgraderid) ?? examGrader;
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
                .AsNoTracking()
                .Include(eg => eg.Examschedule)
                .Include(eg => eg.Teacher)
                .ToListAsync();
        }

        public async Task<Examgrader?> GetByIdAsync(int examGraderId)
        {
            return await _context.Examgraders
                .AsNoTracking()
                .Include(eg => eg.Examschedule)
                .Include(eg => eg.Teacher)
                .FirstOrDefaultAsync(eg => eg.Examgraderid == examGraderId);
        }

        public async Task<Examgrader?> UpdateAsync(int examGraderId, Examgrader updatedExamGrader)
        {
            var existingExamGrader = await _context.Examgraders.FindAsync(examGraderId);
            if (existingExamGrader == null) return null;

            // Kiểm tra xem Examscheduleid mới có hợp lệ không
            var examschedule = await _context.Examschedules
                .AsNoTracking()
                .FirstOrDefaultAsync(es => es.Examscheduleid == updatedExamGrader.Examscheduleid);
            if (examschedule == null)
            {
                throw new InvalidOperationException("Examschedule not found.");
            }

            // Kiểm tra xem Teacherid mới có hợp lệ không
            var teacher = await _context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Teacherid == updatedExamGrader.Teacherid);
            if (teacher == null)
            {
                throw new InvalidOperationException("Teacher not found.");
            }

            existingExamGrader.Examscheduleid = updatedExamGrader.Examscheduleid;
            existingExamGrader.Teacherid = updatedExamGrader.Teacherid;

            await _context.SaveChangesAsync();

            return await _context.Examgraders
                .AsNoTracking()
                .Include(eg => eg.Examschedule)
                .Include(eg => eg.Teacher)
                .FirstOrDefaultAsync(eg => eg.Examgraderid == examGraderId);
        }
    }
}