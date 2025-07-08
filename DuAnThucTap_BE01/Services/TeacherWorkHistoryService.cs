using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherWorkHistoryService : ITeacherWorkHistoryService
    {
        private readonly ISCDbContext _context;
        public TeacherWorkHistoryService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Teacherworkhistory> CreateAsync(Teacherworkhistory history)
        {
            _context.Teacherworkhistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        // Sửa Guid thành int
        public async Task<bool> DeleteAsync(int id)
        {
            var history = await _context.Teacherworkhistories.FindAsync(id);
            if (history == null) return false;

            _context.Teacherworkhistories.Remove(history);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Teacherworkhistory>> GetAllAsync()
        {
            return await _context.Teacherworkhistories.ToListAsync();
        }

        // Sửa Guid thành int
        public async Task<Teacherworkhistory?> GetByIdAsync(int id)
        {
            return await _context.Teacherworkhistories.FindAsync(id);
        }

        // Sửa Guid thành int
        public async Task<Teacherworkhistory?> UpdateAsync(int id, Teacherworkhistory updatedHistory)
        {
            var existing = await _context.Teacherworkhistories.FindAsync(id);
            if (existing == null) return null;

            // Gán thuộc tính thủ công
            existing.Teacherid = updatedHistory.Teacherid;
            existing.Operationunitid = updatedHistory.Operationunitid;
            existing.Departmentid = updatedHistory.Departmentid;
            existing.Iscurrentschool = updatedHistory.Iscurrentschool;
            existing.Positionheld = updatedHistory.Positionheld;
            existing.Startdate = updatedHistory.Startdate;
            existing.Enddate = updatedHistory.Enddate;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}