using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Student>, int)> GetPagedStudentsAsync(int page, int pageSize, string? keyword)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(s => s.Fullname.Contains(keyword) || s.Studentcode.Contains(keyword));
            }

            int total = await query.CountAsync();
            var students = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (students, total);
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var existing = await _context.Students.FindAsync(student.Studentid);
            if (existing == null)
                throw new KeyNotFoundException("Không tìm thấy sinh viên.");

            _context.Entry(existing).CurrentValues.SetValues(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                throw new KeyNotFoundException("Không tìm thấy sinh viên.");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsStudentCodeExistsAsync(string studentCode, int? excludeId = null)
        {
            return await _context.Students.AnyAsync(s =>
                s.Studentcode == studentCode &&
                (!excludeId.HasValue || s.Studentid != excludeId));
        }
    }
}
