using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface IExamService
    {
        Task<IEnumerable<Exam>> GetAllAsync();
        Task<Exam?> GetByIdAsync(int id);
        Task<Exam> CreateAsync(Exam exam);
        Task<Exam?> UpdateAsync(int id, Exam exam);
        Task<bool> DeleteAsync(int id);
    }
}