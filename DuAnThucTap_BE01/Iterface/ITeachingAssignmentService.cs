using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeachingAssignmentService
    {
        Task<IEnumerable<Teachingassignment>> GetAllAsync();
        Task<Teachingassignment?> GetByIdAsync(int id);
        Task<Teachingassignment> CreateAsync(Teachingassignment teachingAssignment);
        Task<Teachingassignment?> UpdateAsync(int id, Teachingassignment teachingAssignment);
        Task<bool> DeleteAsync(int id);
    }
}