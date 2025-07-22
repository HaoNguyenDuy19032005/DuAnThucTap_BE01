using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IAnnouncementService
    {
        Task<(List<Announcement>, int)> GetPagedAsync(int page, int pageSize);
        Task<Announcement?> GetByIdAsync(int id);
        Task<(bool isSuccess, string message)> CreateAsync(Announcement announcement);
        Task<(bool isSuccess, string message)> UpdateAsync(int id, Announcement announcement);
        Task<(bool isSuccess, string message)> DeleteAsync(int id);
    }
}
