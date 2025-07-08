// Services/ClassTypeService.cs
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Data;

namespace DuAnThucTapNhom3.Service
{
    public class ClassTypeService : IClassTypeService
    {
        private readonly AppDbContext _context;

        public ClassTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Classtype>> GetAllAsync()
        {
            return await _context.Classtypes.ToListAsync();
        }

        public async Task<Classtype?> GetByIdAsync(int id) => await _context.Classtypes.FindAsync(id);

        public async Task<Classtype> CreateAsync(Classtype classType)
        {
            classType.Createdat = DateTime.UtcNow;
            classType.Updatedat = DateTime.UtcNow;

            _context.Classtypes.Add(classType);
            await _context.SaveChangesAsync();
            return classType;
        }

        public async Task<Classtype?> UpdateAsync(int id, Classtype classType)
        {
            var existing = await _context.Classtypes.FindAsync(id);
            if (existing == null) return null;

            existing.Classtypename = classType.Classtypename;
            existing.Description = classType.Description;
            existing.Isactive = classType.Isactive;
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Classtypes.FindAsync(id);
            if (entity == null) return false;

            _context.Classtypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
