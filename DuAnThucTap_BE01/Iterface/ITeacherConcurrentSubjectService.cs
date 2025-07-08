using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherConcurrentSubjectService
    {
        Task<IEnumerable<Teacherconcurrentsubject>> GetAllAsync();
        Task<Teacherconcurrentsubject?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId);

        Task<Teacherconcurrentsubject> CreateAsync(Teacherconcurrentsubject assignment);
        Task<bool> DeleteAsync(int teacherId, int subjectId, int schoolYearId);
    }
}