using DuAnThucTap_BE01.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Iterface
{
    public interface ITests
    {
        Task<IEnumerable<TestDto>> GetAllAsync(string? searchQuery, int page, int pageSize);
        Task<TestDto?> GetByIdAsync(int id);
        Task<TestDto> CreateAsync(TestRequestDto testDto);
        Task<TestDto?> UpdateAsync(int id, TestRequestDto testDto);
        Task<bool> DeleteAsync(int id);
    }
}