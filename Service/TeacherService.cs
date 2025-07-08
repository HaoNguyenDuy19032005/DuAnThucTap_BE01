using DuAnThucTapNhom3.Data;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom3.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _context;
        public TeacherService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }
       
        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _context.Teachers.FindAsync(id) ?? throw new Exception($"Schoolyear with ID {id} not found.");
        }

        public async Task<Teacher> CreateAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher> UpdateAsync(int id, Teacher teacher)
        {
            var existing = await _context.Teachers.FindAsync(id);
#pragma warning disable CS8603 // Possible null reference return.
            if (existing == null) return null;
#pragma warning restore CS8603 // Possible null reference return.

            existing.Fullname = teacher.Fullname;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
