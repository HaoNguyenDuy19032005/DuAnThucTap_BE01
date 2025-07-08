using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class ContactService : IContactService
    {
        private readonly ISCDbContext _context;
        public ContactService(ISCDbContext context)
        {
            _context = context;
        }
        public async Task<Contact> CreateAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        // Sửa Guid thành int
        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        // Sửa Guid thành int
        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact?> UpdateAsync(int id, Contact updatedContact)
        {
            var existing = await _context.Contacts.FindAsync(id);
            if (existing == null) return null;

            // Gán thủ công
            existing.Fullname = updatedContact.Fullname;
            existing.Relationship = updatedContact.Relationship;
            existing.Address = updatedContact.Address;
            existing.Phonenumber = updatedContact.Phonenumber;
            existing.Teacherid = updatedContact.Teacherid;
            existing.Studentid = updatedContact.Studentid;

            await _context.SaveChangesAsync();
            return existing;
        }
    }
}