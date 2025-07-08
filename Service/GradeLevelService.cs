// Services/GradeLevelService.cs
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;
using DuAnThucTapNhom3.Iterface;
using System.Diagnostics;
using DuAnThucTapNhom3.Data;

namespace DuAnThucTapNhom3.Service
{
    public class GradeLevelService : IGradeLevelService
    {
        private readonly AppDbContext _context;

        public GradeLevelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gradelevel>> GetAllAsync()
        {
            return await _context.Gradelevels.ToListAsync();
        }

        public async Task<Gradelevel?> GetByIdAsync(int id)
        {
            return await _context.Gradelevels.FindAsync(id);
        }

        public async Task<Gradelevel> CreateAsync(Gradelevel gradeLevel)
        {
            gradeLevel.Createdat = DateTime.UtcNow;
            gradeLevel.Updatedat = DateTime.UtcNow;

            _context.Gradelevels.Add(gradeLevel);
            await _context.SaveChangesAsync();
            return gradeLevel;
        }

        public async Task<Gradelevel?> UpdateAsync(int id, Gradelevel gradeLevel)
        {
            var existing = await _context.Gradelevels.FindAsync(id) ?? throw new KeyNotFoundException($"Gradelevel with ID {id} not found.");
            existing.Gradelevelname = gradeLevel.Gradelevelname;
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Gradelevels.FindAsync(id);
            if (entity == null) return false;

            _context.Gradelevels.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
