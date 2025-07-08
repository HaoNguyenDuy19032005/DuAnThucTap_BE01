using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherTrainingHistoryService : ITeacherTrainingHistoryService
    {
        private readonly ISCDbContext _context;

        public TeacherTrainingHistoryService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Teachertraininghistory> CreateAsync(Teachertraininghistory history)
        {
            _context.Teachertraininghistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        // Sửa Guid thành int
        public async Task<bool> DeleteAsync(int id)
        {
            var history = await _context.Teachertraininghistories.FindAsync(id);
            if (history == null) return false;

            _context.Teachertraininghistories.Remove(history);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Teachertraininghistory>> GetAllAsync()
        {
            return await _context.Teachertraininghistories.ToListAsync();
        }

        // Sửa Guid thành int
        public async Task<Teachertraininghistory?> GetByIdAsync(int id)
        {
            return await _context.Teachertraininghistories.FindAsync(id);
        }

        // Sửa Guid thành int
        public async Task<Teachertraininghistory?> UpdateAsync(int id, Teachertraininghistory updatedHistory)
        {
            var existing = await _context.Teachertraininghistories.FindAsync(id);
            if (existing == null) return null;

            existing.Teacherid = updatedHistory.Teacherid;
            existing.Traininginstitutionname = updatedHistory.Traininginstitutionname;
            existing.Majororspecialization = updatedHistory.Majororspecialization;
            existing.Startdate = updatedHistory.Startdate;
            existing.Enddateorgraduationyear = updatedHistory.Enddateorgraduationyear;
            existing.Trainingtype = updatedHistory.Trainingtype;
            existing.Certificatediplomaname = updatedHistory.Certificatediplomaname;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}