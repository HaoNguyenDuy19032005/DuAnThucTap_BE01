using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class CommendationtypeService : ICommendationtypeService
    {
        private readonly AppDbContext _context;

        public CommendationtypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Commendationtype>> GetAllAsync()
        {
            return await _context.Commendationtypes.ToListAsync();
        }

        public async Task<(List<Commendationtype> Data, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Commendationtypes.AsQueryable();
            var total = await query.CountAsync();
            var data = await query
                .OrderBy(c => c.Commendationtypeid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, total);
        }

        public async Task<Commendationtype?> GetByIdAsync(int id)
        {
            return await _context.Commendationtypes.FindAsync(id);
        }

        public async Task<Commendationtype> AddAsync(Commendationtype commendationtype)
        {
            var exists = await _context.Commendationtypes
                .AnyAsync(c => c.Typename.Trim().ToLower() == commendationtype.Typename.Trim().ToLower());

            if (exists)
                throw new InvalidOperationException("Loại khen thưởng này đã tồn tại.");

            _context.Commendationtypes.Add(commendationtype);
            await _context.SaveChangesAsync();
            return commendationtype;
        }

        public async Task<Commendationtype?> UpdateAsync(int id, Commendationtype commendationtype)
        {
            var existing = await _context.Commendationtypes.FindAsync(id);
            if (existing == null) return null;

            existing.Typename = commendationtype.Typename;
            _context.Commendationtypes.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Commendationtypes.FindAsync(id);
            if (entity == null) return false;

            _context.Commendationtypes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
