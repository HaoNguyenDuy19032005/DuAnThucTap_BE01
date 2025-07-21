using Nhom2ThucTap.DTOs;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Service
{
    public interface ITestStudentSubmissionService
    {
        Task<IEnumerable<TestStudentSubmission>> GetAllAsync(int page, int pageSize, int? studentId = null, int? testId = null);
        Task<TestStudentSubmission?> GetByIdAsync(int id);
        Task<TestStudentSubmission> CreateAsync(TestStudentSubmissionDto dto);
        Task<TestStudentSubmission?> UpdateAsync(int id, TestStudentSubmissionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
