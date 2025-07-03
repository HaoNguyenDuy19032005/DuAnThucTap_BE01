using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Iterface
{
    public interface IExamService
    {
        Task<IEnumerable<Exam>> GetAllAsync();
        Task<Exam?> GetByIdAsync(int id);
        Task<Exam> CreateAsync(Exam newExam);
        Task<Exam?> UpdateAsync(int id, Exam updatedExam);
        Task<bool> DeleteAsync(int id);
    }
}