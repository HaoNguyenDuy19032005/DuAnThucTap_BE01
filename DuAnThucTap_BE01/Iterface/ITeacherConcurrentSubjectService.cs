using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherConcurrentSubjectService
    {
        Task<IEnumerable<TeacherConcurrentSubjectDto>> GetAllAsync();
        Task<TeacherConcurrentSubjectDto?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId);

        Task<(bool Succeeded, string? ErrorMessage, Teacherconcurrentsubject? CreatedAssignment)> CreateAsync(TeacherConcurrentSubjectRequestDto assignmentDto);
        Task<bool> DeleteAsync(int teacherId, int subjectId, int schoolYearId);
    }
}