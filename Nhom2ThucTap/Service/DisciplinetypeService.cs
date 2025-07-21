using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class DisciplinetypeService : IDisciplinetypeService
    {
        private readonly AppDbContext _context;

        public DisciplinetypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Disciplinetype>> GetAllAsync()
        {
            return await _context.Disciplinetypes.ToListAsync();
        }

        public async Task<(List<Disciplinetype> Data, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Disciplinetypes.AsQueryable();
            var total = await query.CountAsync();
            var data = await query
                .OrderBy(d => d.Disciplinetypeid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, total);
        }

        public async Task<Disciplinetype?> GetByIdAsync(int id)
        {
            return await _context.Disciplinetypes.FindAsync(id);
        }

        public async Task<Disciplinetype> AddAsync(Disciplinetype disciplinetype)
        {
            bool exists = await _context.Disciplinetypes
                .AnyAsync(d => d.Typename!.Trim().ToLower() == disciplinetype.Typename!.Trim().ToLower());

            if (exists)
                throw new InvalidOperationException("Loại kỷ luật đã tồn tại.");

            _context.Disciplinetypes.Add(disciplinetype);
            await _context.SaveChangesAsync();
            return disciplinetype;
        }

        public async Task<Disciplinetype?> UpdateAsync(int id, Disciplinetype disciplinetype)
        {
            var existing = await _context.Disciplinetypes.FindAsync(id);
            if (existing == null) return null;

            existing.Typename = disciplinetype.Typename;
            existing.Severity = disciplinetype.Severity;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Disciplinetypes.FindAsync(id);
            if (existing == null) return false;

            _context.Disciplinetypes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
