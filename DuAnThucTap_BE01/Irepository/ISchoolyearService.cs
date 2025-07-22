using DuAnThucTap.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISchoolyearService
{
    Task<PaginatedList<Schoolyear>> GetAllAsync(string? search, int pageIndex, int pageSize);
    Task<Schoolyear?> GetByIdAsync(int id);
    Task<Schoolyear> CreateAsync(Schoolyear schoolyear);
    Task<bool> UpdateAsync(int id, Schoolyear schoolyear);
    Task<bool> DeleteAsync(int id);

}
