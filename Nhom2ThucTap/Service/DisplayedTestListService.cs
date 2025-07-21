using Nhom2ThucTap.Data;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Models;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Service
{
    public class DisplayedTestListService : IDisplayedTestListService
    {
        private readonly AppDbContext _context;
        public DisplayedTestListService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DisplayedTestList>> GetAllAsync(string? keyword, int page, int pageSize)
        {
            var query = _context.DisplayedTestLists.Include(d => d.Subject).Include(d => d.Teacher).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(d => d.Title.Contains(keyword));

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<DisplayedTestList>> GetUpcomingAsync(int page, int pageSize)
        {
            return await _context.DisplayedTestLists
                .Where(t => t.StartTime > DateTime.UtcNow)
                .OrderBy(t => t.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<DisplayedTestList>> GetFinishedAsync(int page, int pageSize)
        {
            return await _context.DisplayedTestLists
                .Where(t => t.StartTime.AddMinutes(t.DurationInMinutes) < DateTime.UtcNow)
                .OrderByDescending(t => t.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<DisplayedTestList?> GetByIdAsync(int id) =>
            await _context.DisplayedTestLists.Include(d => d.Subject).Include(d => d.Teacher).FirstOrDefaultAsync(d => d.DisplayItemID == id);

        public async Task<DisplayedTestList> CreateAsync(DisplayedTestList item)
        {
            _context.DisplayedTestLists.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> UpdateAsync(int id, DisplayedTestList updated)
        {
            var existing = await _context.DisplayedTestLists.FindAsync(id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.DisplayedTestLists.FindAsync(id);
            if (item == null) return false;

            _context.DisplayedTestLists.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SubjectExistsAsync(int subjectId) =>
            await _context.Subjects.AnyAsync(s => s.Subjectid == subjectId);

        public async Task<bool> TeacherExistsAsync(int teacherId) =>
            await _context.Teachers.AnyAsync(t => t.Teacherid == teacherId);
    }
}
