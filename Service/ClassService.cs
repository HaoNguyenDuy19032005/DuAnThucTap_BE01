// Services/ClassService.cs
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Data;

namespace DuAnThucTapNhom3.Service
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _context;

        public ClassService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await _context.Classes.AsNoTracking().ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes.FindAsync(id)
                ?? throw new KeyNotFoundException($"Class with ID {id} not found.");
        }

        public async Task<Class> CreateAsync(Class newClass)
        {
            newClass.Createdat = DateTime.UtcNow;
            newClass.Updatedat = DateTime.UtcNow;

            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();
            return newClass;
        }

        public async Task<Class?> UpdateAsync(int id, Class updatedClass)
        {
            var existing = await _context.Classes.FindAsync(id);
            if (existing == null) return null;

            existing.Classname = updatedClass.Classname;
            existing.Originalfilename = updatedClass.Originalfilename;
            existing.Storedfilepath = updatedClass.Storedfilepath;
            existing.Maxstudents = updatedClass.Maxstudents;
            existing.Schoolyearid = updatedClass.Schoolyearid;
            existing.Gradelevelid = updatedClass.Gradelevelid;
            existing.Classtypeid = updatedClass.Classtypeid;
            existing.Homeroomteacherid = updatedClass.Homeroomteacherid;
            existing.Subjectid = updatedClass.Subjectid;
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Classes.FindAsync(id);
            if (entity == null) return false;

            _context.Classes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
