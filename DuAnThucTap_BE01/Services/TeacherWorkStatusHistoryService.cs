// Services/TeacherWorkStatusHistoryService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherWorkStatusHistoryService : ITeacherWorkStatusHistoryService
    {
        private readonly ISCDbContext _context;
        public TeacherWorkStatusHistoryService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Teacherworkstatushistory> CreateAsync(Teacherworkstatushistory history)
        {
            history.Createdat = DateTime.UtcNow;
            _context.Teacherworkstatushistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var history = await _context.Teacherworkstatushistories.FindAsync(id);
            if (history == null) return false;

            _context.Teacherworkstatushistories.Remove(history);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Teacherworkstatushistory>> GetAllAsync()
        {
            return await _context.Teacherworkstatushistories.ToListAsync();
        }

        public async Task<Teacherworkstatushistory?> GetByIdAsync(Guid id)
        {
            return await _context.Teacherworkstatushistories.FindAsync(id);
        }

        public async Task<Teacherworkstatushistory?> UpdateAsync(Guid id, Teacherworkstatushistory updatedHistory)
        {
            var existing = await _context.Teacherworkstatushistories.FindAsync(id);
            if (existing == null) return null;

            // Gán thuộc tính thủ công
            existing.Teacherid = updatedHistory.Teacherid;
            existing.Statustype = updatedHistory.Statustype;
            existing.Startdate = updatedHistory.Startdate;
            existing.Enddate = updatedHistory.Enddate;
            existing.Note = updatedHistory.Note;
            existing.Decisionfileurl = updatedHistory.Decisionfileurl;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}