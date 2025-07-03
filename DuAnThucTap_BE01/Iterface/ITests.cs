using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Iterface
{
    public interface ITests
    {
        Task<IEnumerable<Tests>> GetAllAsync();
        Task<Tests> GetByIdAsync(int id);
        Task<Tests> CreateAsync(Tests test);
        Task<Tests> UpdateAsync(int id, Tests test);
        Task<bool> DeleteAsync(int id);
    }
}