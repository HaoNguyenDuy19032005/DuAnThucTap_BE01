using DuAnThucTap.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task<Department> CreateAsync(Department department);
    Task<bool> UpdateAsync(int id, Department department);
    Task<bool> DeleteAsync(int id);
}
