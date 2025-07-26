using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface // Lưu ý: sửa "Iterface" thành "Interface" nếu đây là lỗi đánh máy
{
    public interface ITestAssignment
    {
        Task<IEnumerable<Testassignment>> GetAllAsync();
        Task<Testassignment> GetByIdAsync(int id);
        Task<Testassignment> CreateAsync(Testassignment testAssignment);
        Task<Testassignment> UpdateAsync(int id, Testassignment testAssignment);
        Task<bool> DeleteAsync(int id);
    }
}