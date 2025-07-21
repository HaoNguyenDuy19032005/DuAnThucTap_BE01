using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Interface
{ 
    public interface ITestStudentAnswerService
    {
        Task<IEnumerable<TestStudentAnswer>> GetAllAsync();
        Task<TestStudentAnswer?> GetByIdAsync(int id);
        Task<TestStudentAnswer> CreateAsync(TestStudentAnswerDto dto);
        Task<TestStudentAnswer?> UpdateAsync(int id, TestStudentAnswerDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
