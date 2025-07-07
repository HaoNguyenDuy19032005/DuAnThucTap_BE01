using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherConcurrentSubjectService
    {
        Task<IEnumerable<Teacherconcurrentsubject>> GetAllAsync();
        Task<Teacherconcurrentsubject?> GetByIdAsync(Guid teacherId, Guid subjectId, Guid schoolYearId);
        Task<Teacherconcurrentsubject> CreateAsync(Teacherconcurrentsubject assignment);
        Task<bool> DeleteAsync(Guid teacherId, Guid subjectId, Guid schoolYearId);
    }
}