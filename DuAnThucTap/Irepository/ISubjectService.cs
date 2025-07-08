using DuAnThucTap.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISubjectService
{
    Task<IEnumerable<Subject>> GetAllAsync();
    Task<Subject?> GetByIdAsync(int id);
    Task<Subject> CreateAsync(Subject subject);
    Task<bool> UpdateAsync(int id, Subject subject);
    Task<bool> DeleteAsync(int id);
}
