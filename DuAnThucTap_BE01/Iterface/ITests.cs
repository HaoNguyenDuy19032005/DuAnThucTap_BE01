using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITests
    {
        Task<IEnumerable<TestDto>> GetAllAsync(string? searchQuery, int page, int pageSize);
        Task<TestDto?> GetByIdAsync(int id);
        Task<Test> CreateAsync(TestRequestDto testDto);
        Task<Test?> UpdateAsync(int id, TestRequestDto testDto);
        Task<string?> UpdateAttachmentAsync(int id, IFormFile attachmentFile);
        Task<bool> DeleteAsync(int id);
    }
}