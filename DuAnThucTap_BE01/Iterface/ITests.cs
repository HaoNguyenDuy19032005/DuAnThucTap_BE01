using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITests
    {
        Task<IEnumerable<Test>> GetAllAsync();
        Task<Test?> GetByIdAsync(int id);
        Task<Test> CreateAsync(Test test);
        Task<Test?> UpdateAsync(int id, Test test);
        Task<bool> DeleteAsync(int id);
    }
}