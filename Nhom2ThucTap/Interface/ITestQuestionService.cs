using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nhom2ThucTap.Service
{
    public interface ITestQuestionService
    {
        Task<IEnumerable<TestQuestionItem>> GetAllAsync();
        Task<TestQuestionItem?> GetByIdAsync(int id);
        Task<TestQuestionItem> CreateAsync(TestQuestionItemDto dto);
        Task<TestQuestionItem?> UpdateAsync(int id, TestQuestionItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
