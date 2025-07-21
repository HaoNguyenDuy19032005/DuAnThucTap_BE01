using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentYearlyStatusService : IStudentYearlyStatusService
    {
        private readonly AppDbContext _context;

        public StudentYearlyStatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Studentyearlystatus>> GetAllAsync()
        {
            return await _context.Studentyearlystatuses
                .Include(s => s.Student)
                .Include(s => s.Class)
                .Include(s => s.Gradelevel)
                .Include(s => s.Schoolyear)
                .ToListAsync();
        }

        public async Task<(List<Studentyearlystatus> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Studentyearlystatuses
                .Include(s => s.Student)
                .Include(s => s.Class)
                .Include(s => s.Gradelevel)
                .Include(s => s.Schoolyear);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }

        public async Task<Studentyearlystatus?> GetByIdAsync(int id)
        {
            return await _context.Studentyearlystatuses
                .Include(s => s.Student)
                .Include(s => s.Class)
                .Include(s => s.Gradelevel)
                .Include(s => s.Schoolyear)
                .FirstOrDefaultAsync(s => s.Studentyearlystatusid == id);
        }

        public async Task<Studentyearlystatus?> GetByStudentIdAsync(int studentId)
        {
            return await _context.Studentyearlystatuses
                .Include(s => s.Student)
                .Include(s => s.Class)
                .Include(s => s.Gradelevel)
                .Include(s => s.Schoolyear)
                .FirstOrDefaultAsync(s => s.Studentid == studentId);
        }

        public async Task AddAsync(Studentyearlystatus status)
        {
            status.Createdat = DateTime.Now;
            _context.Studentyearlystatuses.Add(status);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Studentyearlystatus status)
        {
            var exists = await _context.Studentyearlystatuses.AnyAsync(x => x.Studentyearlystatusid == status.Studentyearlystatusid);
            if (!exists) return false;

            status.Updatedat = DateTime.Now;
            _context.Entry(status).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var status = await _context.Studentyearlystatuses.FindAsync(id);
            if (status == null) return false;

            _context.Studentyearlystatuses.Remove(status);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsValidForeignKeysAsync(Studentyearlystatus status)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.Studentid == status.Studentid);
            var classExists = await _context.Classes.AnyAsync(c => c.Classid == status.Classid);
            var yearExists = await _context.Schoolyears.AnyAsync(y => y.Schoolyearid == status.Schoolyearid);
            var gradeExists = await _context.Gradelevels.AnyAsync(g => g.Gradelevelid == status.Gradelevelid);

            return studentExists && classExists && yearExists && gradeExists;
        }

    }
}
