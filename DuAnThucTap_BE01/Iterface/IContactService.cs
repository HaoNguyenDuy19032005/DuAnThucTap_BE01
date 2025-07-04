using DuAnThucTap_BE01.Models;
namespace DuAnThucTap_BE01.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(Guid id);
        Task<Contact> CreateAsync(Contact contact);
        Task<Contact?> UpdateAsync(Guid id, Contact contact);
        Task<bool> DeleteAsync(Guid id);
    }
}