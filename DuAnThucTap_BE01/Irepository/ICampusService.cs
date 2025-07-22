using DuAnThucTap.Model;

namespace DuAnThucTap.Irepository
{
    public interface ICampusService
    {
        Task<IEnumerable<Campus>> GetAllAsync();
        Task<Campus?> GetByIdAsync(int id);
        Task<Campus> CreateAsync(Campus campus);
        Task<bool> UpdateAsync(int id, Campus campus);
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Upload ảnh cho Campus, lưu URL vào trường Imageurl.
        /// Trả về (Success, ImageUrl, ErrorMessage).
        /// </summary>
        Task<(bool Success, string? ImageUrl, string? ErrorMessage)> UploadImageAsync(int id, IFormFile file);
    }
}
