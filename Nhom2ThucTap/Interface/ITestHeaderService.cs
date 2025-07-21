using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;

public interface ITestHeaderService
{
    Task<IEnumerable<TestHeader>> GetAllAsync();
    Task<TestHeader?> GetByIdAsync(int id);
    Task<TestHeader> CreateAsync(TestHeaderDto dto); // ❌ bỏ IFormFile
    Task<TestHeader?> UpdateAsync(int id, TestHeaderDto dto); // ❌ bỏ IFormFile
    Task<bool> DeleteAsync(int id);
}
