using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response; 

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherConcurrentSubjectService
    {
        Task<PagedResponse<TeacherConcurrentSubjectDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);

        Task<TeacherConcurrentSubjectDto?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId);
        Task<(bool Succeeded, string? ErrorMessage, Teacherconcurrentsubject? CreatedAssignment)> CreateAsync(TeacherConcurrentSubjectRequestDto assignmentDto);
        Task<bool> DeleteAsync(int teacherId, int subjectId, int schoolYearId);
    }
}