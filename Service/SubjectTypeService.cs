using DuAnThucTapNhom3.Data;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom3.Service
{
    public class SubjectTypeService : ISubjectTypeService
    {
        private readonly AppDbContext _context;

        public SubjectTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subjecttype>> GetAllAsync()
        {
            return await _context.Subjecttypes.ToListAsync();
        }

        public async Task<Subjecttype?> GetByIdAsync(int id)
        {
            return await _context.Subjecttypes.FindAsync(id);
        }

        public async Task<Subjecttype> CreateAsync(Subjecttype subjectType)
        {
            subjectType.Createdat = DateTime.UtcNow;
            subjectType.Updatedat = DateTime.UtcNow;
            _context.Subjecttypes.Add(subjectType);
            await _context.SaveChangesAsync();
            return subjectType;
        }

        public async Task<Subjecttype?> UpdateAsync(int id, Subjecttype subjectType)
        {
            var existing = await _context.Subjecttypes.FindAsync(id);
            if (existing == null) return null;

            existing.Subjecttypename = subjectType.Subjecttypename;
            existing.Description = subjectType.Description;
            existing.Isactive = subjectType.Isactive;
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Subjecttypes.FindAsync(id);
            if (entity == null) return false;

            _context.Subjecttypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
