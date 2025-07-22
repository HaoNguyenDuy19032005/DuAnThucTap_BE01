using DuAnThucTap.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISchoolinformationService
{
    Task<IEnumerable<Schoolinformation>> GetAllAsync();
    Task<Schoolinformation?> GetByIdAsync(int id);
    Task<Schoolinformation> CreateAsync(Schoolinformation school);
    Task<bool> UpdateAsync(int id, Schoolinformation school);
    Task<bool> DeleteAsync(int id);
    Task<(bool Success, string? LogoUrl, string? ErrorMessage)> UploadLogoAsync(
         int id,
         IFormFile logo
     );
}