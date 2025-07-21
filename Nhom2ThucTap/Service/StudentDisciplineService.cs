using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentDisciplineService : IStudentDisciplineService
    {
        private readonly AppDbContext _context;

        public StudentDisciplineService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Studentdiscipline>> GetAllAsync()
        {
            return await _context.Studentdisciplines
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .Include(s => s.Disciplinetype)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<Studentdiscipline> Disciplines, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Studentdisciplines
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .Include(s => s.Disciplinetype)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var disciplines = await query
                .OrderByDescending(s => s.Commendationdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (disciplines, totalCount);
        }

        public async Task<Studentdiscipline?> GetByIdAsync(int id)
        {
            return await _context.Studentdisciplines
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .Include(s => s.Disciplinetype)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Disciplineid == id);
        }

        public async Task<Studentdiscipline> AddAsync(Studentdiscipline discipline)
        {
            discipline.Createdat = DateTime.Now;
            _context.Studentdisciplines.Add(discipline);
            await _context.SaveChangesAsync();
            return discipline;
        }

        public async Task<Studentdiscipline?> UpdateAsync(int id, Studentdiscipline discipline)
        {
            var existing = await _context.Studentdisciplines.FindAsync(id);
            if (existing == null) return null;

            existing.Studentid = discipline.Studentid;
            existing.Semesterid = discipline.Semesterid;
            existing.Schoolinfoid = discipline.Schoolinfoid;
            existing.Disciplinetypeid = discipline.Disciplinetypeid;
            existing.Commendationdate = discipline.Commendationdate;
            existing.Content = discipline.Content;
            existing.Attachmenturl = discipline.Attachmenturl;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Studentdisciplines.FindAsync(id);
            if (existing == null) return false;

            _context.Studentdisciplines.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
