using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentExemptionService : IStudentExemptionService
    {
        private readonly AppDbContext _context;

        public StudentExemptionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Studentexemption>> GetAllAsync()
        {
            return await _context.Studentexemptions
                .Include(e => e.Student)
                .Include(e => e.Object)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<Studentexemption> Exemptions, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Studentexemptions
                .Include(e => e.Student)
                .Include(e => e.Object)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(e => e.Studentexemptionid)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<Studentexemption?> GetByIdAsync(int id)
        {
            return await _context.Studentexemptions
                .Include(e => e.Student)
                .Include(e => e.Object)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Studentexemptionid == id);
        }

        public async Task<Studentexemption> AddAsync(Studentexemption exemption)
        {
            _context.Studentexemptions.Add(exemption);
            await _context.SaveChangesAsync();
            return exemption;
        }

        public async Task<Studentexemption?> UpdateAsync(int id, Studentexemption exemption)
        {
            var existing = await _context.Studentexemptions.FindAsync(id);
            if (existing == null) return null;

            existing.Studentid = exemption.Studentid;
            existing.Objectid = exemption.Objectid;
            existing.Formofexemption = exemption.Formofexemption;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Studentexemptions.FindAsync(id);
            if (existing == null) return false;

            _context.Studentexemptions.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
