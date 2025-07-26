using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Helpers; // Thêm helper PagedResult
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITopicListService
    {
        /// <summary>
        /// Lấy danh sách chủ đề có phân trang và tìm kiếm.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm (tên hoặc mô tả chủ đề).</param>
        /// <param name="pageNumber">Số trang.</param>
        /// <param name="pageSize">Số mục mỗi trang.</param>
        /// <returns>Đối tượng PagedResult chứa danh sách chủ đề.</returns>
        Task<PagedResult<Topiclist>> GetAllAsync(string? searchTerm, int pageNumber, int pageSize);

        Task<Topiclist?> GetByIdAsync(int id);
        Task<Topiclist> CreateAsync(Topiclist topicList);
        Task<Topiclist?> UpdateAsync(int id, Topiclist topicList);
        Task<bool> DeleteAsync(int id);
    }
}
