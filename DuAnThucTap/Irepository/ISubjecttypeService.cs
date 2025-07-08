using DuAnThucTap.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISubjecttypeService
{
    Task<IEnumerable<Subjecttype>> GetAllAsync();
    Task<Subjecttype?> GetByIdAsync(int id);
    Task<Subjecttype> CreateAsync(Subjecttype subjecttype);
    Task<bool> UpdateAsync(int id, Subjecttype subjecttype);
    Task<bool> DeleteAsync(int id);
}
