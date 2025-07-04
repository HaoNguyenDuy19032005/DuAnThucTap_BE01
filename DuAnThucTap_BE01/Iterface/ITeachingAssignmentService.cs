using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Iterface
{
    public interface ITeachingAssignmentService
    {
        Task<IEnumerable<TeachingAssignment>> GetAllAsync();
        Task<TeachingAssignment?> GetByIdAsync(int id);
        Task<TeachingAssignment> CreateAsync(TeachingAssignment model);
        Task<TeachingAssignment?> UpdateAsync(int id, TeachingAssignment model);
        Task<bool> DeleteAsync(int id);
    }

}
