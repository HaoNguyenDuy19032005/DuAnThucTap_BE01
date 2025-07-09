using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface IExamGraderService
    {
        Task<IEnumerable<Examgrader>> GetAllAsync();
        Task<Examgrader?> GetByIdAsync(int id);
        Task<Examgrader> CreateAsync(Examgrader examGrader);
        Task<Examgrader?> UpdateAsync(int id, Examgrader examGrader);
        Task<bool> DeleteAsync(int id);
    }
}