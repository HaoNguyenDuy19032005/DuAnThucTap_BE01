using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();

        // Sửa Guid thành int
        Task<Contact?> GetByIdAsync(int id);

        Task<Contact> CreateAsync(Contact contact);

        // Sửa Guid thành int
        Task<Contact?> UpdateAsync(int id, Contact contact);

        // Sửa Guid thành int
        Task<bool> DeleteAsync(int id);
    }
}