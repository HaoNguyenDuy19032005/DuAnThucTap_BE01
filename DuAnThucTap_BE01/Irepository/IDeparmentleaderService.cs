using DuAnThucTap.Model;

namespace DuAnThucTap.Irepository
{
    public interface IDepartmentleadersService
    {
        Task<IEnumerable<Departmentleader>> GetAllAsync(PaginationDto pagination);
        Task<Departmentleader?> GetByIdAsync(int id);
        Task<Departmentleader> CreateAsync(DepartmentleaderCreateDto departmentleader);
        Task<bool> UpdateAsync(int id, DepartmentleaderCreateDto departmentleader);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Departmentleader>> SearchByDepartmentNameAsync(string keyword, PaginationDto pagination);

    }
}