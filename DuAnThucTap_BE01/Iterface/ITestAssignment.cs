using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Iterface
{
    public interface ITestAssignment
    {
        Task<IEnumerable<TestAssignment>> GetAllAsync();
        Task<TestAssignment> GetByIdAsync(int id);
        Task<TestAssignment> CreateAsync(TestAssignment testAssignment);
        Task<TestAssignment> UpdateAsync(int id, TestAssignment testAssignment);
        Task<bool> DeleteAsync(int id);
    }
}