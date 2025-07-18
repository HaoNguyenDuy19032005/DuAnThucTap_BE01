using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class ExamScheduleService : IExamScheduleService
    {
        private readonly ISCDbContext _context;

        public ExamScheduleService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Examschedule> CreateAsync(Examschedule examSchedule)
        {
            _context.Examschedules.Add(examSchedule);
            await _context.SaveChangesAsync();
            return examSchedule;
        }

        public async Task<bool> DeleteAsync(int examScheduleId)
        {
            var examSchedule = await _context.Examschedules.FindAsync(examScheduleId);
            if (examSchedule == null) return false;

            _context.Examschedules.Remove(examSchedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Examschedule>> GetAllAsync()
        {
            return await _context.Examschedules
                .Include(es => es.Exam)
                .Include(es => es.Class)
                .ToListAsync();
        }

        public async Task<Examschedule?> GetByIdAsync(int examScheduleId)
        {
            return await _context.Examschedules
                .Include(es => es.Exam)
                .Include(es => es.Class)
                .FirstOrDefaultAsync(es => es.Examscheduleid == examScheduleId);
        }

        public async Task<Examschedule?> UpdateAsync(int examScheduleId, Examschedule updatedExamSchedule)
        {
            var existingExamSchedule = await _context.Examschedules.FindAsync(examScheduleId);
            if (existingExamSchedule == null) return null;

            existingExamSchedule.Examid = updatedExamSchedule.Examid;
            existingExamSchedule.Classid = updatedExamSchedule.Classid;

            await _context.SaveChangesAsync();
            return existingExamSchedule;
        }
    }
}