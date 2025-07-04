using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Service
{
    public class TeachingAssignmentService : ITeachingAssignmentService
    {
        private readonly ApplicationDbContext _context;
        public TeachingAssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeachingAssignment>> GetAllAsync()
        {
            return await _context.TeachingAssignments.Include(t => t.Topic).ToListAsync();
        }

        public async Task<TeachingAssignment?> GetByIdAsync(int id)
        {
            return await _context.TeachingAssignments.Include(t => t.Topic).FirstOrDefaultAsync(t => t.AssignmentID == id);
        }

        public async Task<TeachingAssignment> CreateAsync(TeachingAssignment model)
        {
            _context.TeachingAssignments.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<TeachingAssignment?> UpdateAsync(int id, TeachingAssignment model)
        {
            var existing = await _context.TeachingAssignments.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TeachingAssignments.FindAsync(id);
            if (entity == null) return false;

            _context.TeachingAssignments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
