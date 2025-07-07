
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ISCDbContext _context;

        public TeacherService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> CreateAsync(Teacher teacher)
        {
            teacher.Createdat = DateTime.UtcNow;
            teacher.Updatedat = DateTime.UtcNow;
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(Guid id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<Teacher?> UpdateAsync(Guid id, Teacher updatedTeacher)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);
            if (existingTeacher == null) return null;

            existingTeacher.Fullname = updatedTeacher.Fullname;
            existingTeacher.Teachercode = updatedTeacher.Teachercode;
            existingTeacher.Dateofbirth = updatedTeacher.Dateofbirth;
            existingTeacher.Gender = updatedTeacher.Gender;
            existingTeacher.Phonenumber = updatedTeacher.Phonenumber;
            existingTeacher.Status = updatedTeacher.Status;
            existingTeacher.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTeacher;
        }
    }
}