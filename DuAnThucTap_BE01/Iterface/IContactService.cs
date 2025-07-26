using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;

namespace DuAnThucTap_BE01.Interface
{
    public interface IContactService
    {
        Task<PagedResponse<ContactDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<ContactDto?> GetByIdAsync(int id);
        Task<Contact> CreateAsync(ContactRequestDto contactDto);
        Task<Contact?> UpdateAsync(int id, ContactRequestDto contactDto);
        Task<bool> DeleteAsync(int id);
    }
}