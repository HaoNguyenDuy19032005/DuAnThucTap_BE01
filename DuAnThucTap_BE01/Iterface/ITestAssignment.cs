using DuAnThucTap_BE01.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITestassignment
    {
        Task<IEnumerable<TestAssignmentDto>> GetAllAsync(string? searchQuery, int page, int pageSize);
        Task<TestAssignmentDto?> GetByIdAsync(int id);
        Task<TestAssignmentDto> CreateAsync(TestAssignmentRequestDto testassignment);
        Task<TestAssignmentDto?> UpdateAsync(int id, TestAssignmentRequestDto testassignment);
        Task<bool> DeleteAsync(int id);
    }
}