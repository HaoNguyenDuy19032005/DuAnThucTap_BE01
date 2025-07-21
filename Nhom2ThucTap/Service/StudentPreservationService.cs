using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentPreservationService : IStudentPreservationService
    {
        private readonly AppDbContext _context;

        public StudentPreservationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Studentpreservation>> GetAllAsync()
        {
            return await _context.Studentpreservations
                .Include(p => p.Student)
                .Include(p => p.Class)
                .Include(p => p.Semester)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<Studentpreservation> Preservations, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Studentpreservations
                .Include(p => p.Student)
                .Include(p => p.Class)
                .Include(p => p.Semester)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(p => p.Preservationdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<Studentpreservation?> GetByIdAsync(int id)
        {
            return await _context.Studentpreservations
                .Include(p => p.Student)
                .Include(p => p.Class)
                .Include(p => p.Semester)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Preservationid == id);
        }

        public async Task<Studentpreservation> AddAsync(Studentpreservation preservation)
        {
            preservation.Createdat = DateTime.UtcNow;
            preservation.Updatedat = DateTime.UtcNow;

            _context.Studentpreservations.Add(preservation);
            await _context.SaveChangesAsync();
            return preservation;
        }

        public async Task<Studentpreservation?> UpdateAsync(int id, Studentpreservation preservation)
        {
            var existing = await _context.Studentpreservations.FindAsync(id);
            if (existing == null) return null;

            existing.Studentid = preservation.Studentid;
            existing.Classid = preservation.Classid;
            existing.Preservationdate = preservation.Preservationdate;
            existing.Semesterid = preservation.Semesterid;
            existing.Preservationduration = preservation.Preservationduration;
            existing.Reason = preservation.Reason;
            existing.Attachmenturl = preservation.Attachmenturl;
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Studentpreservations.FindAsync(id);
            if (existing == null) return false;

            _context.Studentpreservations.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsStudentAsync(int studentId)
    => await _context.Students.AnyAsync(s => s.Studentid == studentId);

        public async Task<bool> ExistsClassAsync(int classId)
            => await _context.Classes.AnyAsync(c => c.Classid == classId);

        public async Task<bool> ExistsSemesterAsync(int semesterId)
            => await _context.Semesters.AnyAsync(s => s.Semesterid == semesterId);

    }
}
