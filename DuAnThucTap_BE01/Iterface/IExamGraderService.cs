using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface IExamGraderService
    {
        Task<IEnumerable<Examgrader>> GetAllAsync();
        Task<Examgrader?> GetByIdAsync(int examGraderId);
        Task<Examgrader> CreateAsync(Examgrader examGrader);
        Task<Examgrader?> UpdateAsync(int examGraderId, Examgrader updatedExamGrader);
        Task<bool> DeleteAsync(int examGraderId);
    }
}