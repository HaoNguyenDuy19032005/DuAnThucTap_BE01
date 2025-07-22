using DuAnThucTap.Data;
using DuAnThucTap.Irepository;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Service
{
    public class CampusService : ICampusService
    {
        private readonly ApplicationDbContext _db;
        private readonly IFirebaseStorageService _firebase;

        public CampusService(
            ApplicationDbContext db,
            IFirebaseStorageService firebase)
        {
            _db = db;
            _firebase = firebase;
        }

        public async Task<IEnumerable<Campus>> GetAllAsync()
            => await _db.Campuses.ToListAsync();

        public async Task<Campus?> GetByIdAsync(int id)
            => await _db.Campuses.FindAsync(id);

        public async Task<Campus> CreateAsync(Campus campus)
        {
            _db.Campuses.Add(campus);
            await _db.SaveChangesAsync();
            return campus;
        }

        public async Task<bool> UpdateAsync(int id, Campus campus)
        {
            if (id != campus.Campusid) return false;
            _db.Entry(campus).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Campuses.FindAsync(id);
            if (entity == null) return false;
            _db.Campuses.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<(bool Success, string? ImageUrl, string? ErrorMessage)> UploadImageAsync(int id, IFormFile file)
        {
            // 1. Lấy campus
            var campus = await _db.Campuses.FindAsync(id);
            if (campus == null)
                return (false, null, "Không tìm thấy Campus.");

            // 2. Validate file
            if (file == null || file.Length == 0)
                return (false, null, "File ảnh không hợp lệ.");

            try
            {
                // 3. Upload lên Firebase
                await using var stream = file.OpenReadStream();
                var url = await _firebase.UploadFileAsync(
                    fileStream: stream,
                    fileName: file.FileName,
                    contentType: file.ContentType
                );

                // 4. Cập nhật URL vào DB
                campus.Imageurl = url;
                _db.Entry(campus).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return (true, url, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
