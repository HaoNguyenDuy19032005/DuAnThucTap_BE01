using System.Collections.Generic;
using System.Threading.Tasks;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Services
{
    public interface ITestassignment
    {
        Task<IEnumerable<Testassignment>> GetAllAsync();
        Task<Testassignment?> GetByIdAsync(int id);
        Task<Testassignment> CreateAsync(Testassignment testassignment);
        Task<Testassignment?> UpdateAsync(int id, Testassignment testassignment);
        Task<bool> DeleteAsync(int id);
    }
}