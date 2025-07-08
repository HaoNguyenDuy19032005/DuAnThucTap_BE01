using DuAnThucTap.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISchoolyearService
{
    Task<IEnumerable<Schoolyear>> GetAllAsync();
    Task<Schoolyear?> GetByIdAsync(int id);
    Task<Schoolyear> CreateAsync(Schoolyear schoolyear);
    Task<bool> UpdateAsync(int id, Schoolyear schoolyear);
    Task<bool> DeleteAsync(int id);
}
