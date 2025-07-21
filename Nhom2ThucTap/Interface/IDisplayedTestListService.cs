using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Interface
{
    public interface IDisplayedTestListService
    {
        Task<IEnumerable<DisplayedTestList>> GetAllAsync(string? keyword, int page, int pageSize);
        Task<IEnumerable<DisplayedTestList>> GetUpcomingAsync(int page, int pageSize);
        Task<IEnumerable<DisplayedTestList>> GetFinishedAsync(int page, int pageSize);
        Task<DisplayedTestList?> GetByIdAsync(int id);
        Task<DisplayedTestList> CreateAsync(DisplayedTestList item);
        Task<bool> UpdateAsync(int id, DisplayedTestList updated);
        Task<bool> DeleteAsync(int id);
        Task<bool> SubjectExistsAsync(int subjectId);
        Task<bool> TeacherExistsAsync(int teacherId);
    }

}
