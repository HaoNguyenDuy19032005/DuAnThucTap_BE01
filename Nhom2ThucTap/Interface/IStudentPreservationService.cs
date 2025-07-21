using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IStudentPreservationService
    {
        Task<IEnumerable<Studentpreservation>> GetAllAsync();
        Task<(List<Studentpreservation> Preservations, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Studentpreservation?> GetByIdAsync(int id);
        Task<Studentpreservation> AddAsync(Studentpreservation preservation);
        Task<Studentpreservation?> UpdateAsync(int id, Studentpreservation preservation);
        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsStudentAsync(int studentId);
        Task<bool> ExistsClassAsync(int classId);
        Task<bool> ExistsSemesterAsync(int semesterId);

    }
}
