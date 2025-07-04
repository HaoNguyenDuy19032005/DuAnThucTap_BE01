using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;

        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> CreateAsync(Department newDepartment)
        {
            newDepartment.CreatedAt = DateTime.UtcNow;

            _context.Departments.Add(newDepartment);
            await _context.SaveChangesAsync();
            return newDepartment;
        }

        public async Task<Department?> UpdateAsync(int id, Department updatedDepartment)
        {
            var existing = await _context.Departments.FindAsync(id);
            if (existing == null) return null;

            existing.DepartmentName = updatedDepartment.DepartmentName;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity == null) return false;

            _context.Departments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
