using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Helpers; // Thêm helper
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeachingAssignmentService
    {
        /// <summary>
        /// Lấy danh sách phân công có phân trang và tìm kiếm.
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm.</param>
        /// <param name="pageNumber">Số trang.</param>
        /// <param name="pageSize">Số mục mỗi trang.</param>
        /// <returns>Đối tượng PagedResult chứa danh sách phân công.</returns>
        Task<PagedResult<Teachingassignment>> GetAllAsync(string? searchTerm, int pageNumber, int pageSize);

        Task<Teachingassignment?> GetByIdAsync(int id);
        Task<Teachingassignment> CreateAsync(Teachingassignment teachingAssignment);
        Task<Teachingassignment?> UpdateAsync(int id, Teachingassignment teachingAssignment);
        Task<bool> DeleteAsync(int id);
    }
}
