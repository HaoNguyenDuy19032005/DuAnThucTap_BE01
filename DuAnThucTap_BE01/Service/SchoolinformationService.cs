using DuAnThucTap.Data;
using DuAnThucTap.Model;
using DuAnThucTap.Service;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SchoolinformationService : ISchoolinformationService
{
    private readonly ApplicationDbContext _context;
    private readonly IFirebaseStorageService _firebase;

    public SchoolinformationService(
      ApplicationDbContext context,
      IFirebaseStorageService firebase)
    {
        _context = context;
        _firebase = firebase;
    }

    public async Task<IEnumerable<Schoolinformation>> GetAllAsync()
    {
        return await _context.Schoolinformations.ToListAsync();
    }

    public async Task<Schoolinformation?> GetByIdAsync(int id)
    {
        return await _context.Schoolinformations.FindAsync(id);
    }

    public async Task<Schoolinformation> CreateAsync(Schoolinformation school)
    {
        _context.Schoolinformations.Add(school);
        await _context.SaveChangesAsync();
        return school;
    }

    public async Task<bool> UpdateAsync(int id, Schoolinformation school)
    {
        if (id != school.Schoolinfoid) return false;

        _context.Entry(school).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Schoolinformations.FindAsync(id);
        if (entity == null) return false;

        _context.Schoolinformations.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(bool Success, string? LogoUrl, string? ErrorMessage)>
         UploadLogoAsync(int id, IFormFile logo)
    {
        // 1. Lấy entity
        var school = await _context.Schoolinformations.FindAsync(id);
        if (school == null)
            return (false, null, "Không tìm thấy Schoolinformation.");

        // 2. Validate file
        if (logo == null || logo.Length == 0)
            return (false, null, "File logo không hợp lệ.");

        try
        {
            // 3. Upload lên Firebase
            await using var stream = logo.OpenReadStream();
            var url = await _firebase.UploadFileAsync(
                fileStream: stream,
                fileName: logo.FileName,
                contentType: logo.ContentType
            );

            // 4. Lưu URL vào DB
            school.Logourl = url;
            _context.Entry(school).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return (true, url, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}
