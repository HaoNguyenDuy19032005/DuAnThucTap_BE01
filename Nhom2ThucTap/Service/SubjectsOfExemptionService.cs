using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class SubjectsOfExemptionService : ISubjectsOfExemptionService
    {
        private readonly AppDbContext _context;

        public SubjectsOfExemptionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subjectsofexemption>> GetAllAsync()
        {
            return await _context.Subjectsofexemptions
                .Include(s => s.Studentexemptions)
                .ToListAsync();
        }

        public async Task<(List<Subjectsofexemption> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Subjectsofexemptions.Include(s => s.Studentexemptions);
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task<Subjectsofexemption?> GetByIdAsync(int id)
        {
            return await _context.Subjectsofexemptions
                .Include(s => s.Studentexemptions)
                .FirstOrDefaultAsync(e => e.Objectid == id);
        }

        public async Task<Subjectsofexemption> AddAsync(Subjectsofexemption exemption)
        {
            _context.Subjectsofexemptions.Add(exemption);
            await _context.SaveChangesAsync();
            return exemption;
        }

        public async Task<Subjectsofexemption?> UpdateAsync(int id, Subjectsofexemption exemption)
        {
            var existing = await _context.Subjectsofexemptions.FindAsync(id);
            if (existing == null) return null;

            existing.Exemptionname = exemption.Exemptionname;
            existing.Description = exemption.Description;
            existing.Isactive = exemption.Isactive;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Subjectsofexemptions.FindAsync(id);
            if (existing == null) return false;

            _context.Subjectsofexemptions.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
