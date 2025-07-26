using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
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

        public async Task<PagedResponse<ContactDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Contacts
                .Include(c => c.Teacher)
                .Include(c => c.Student)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerCaseQuery = searchQuery.ToLower();
                query = query.Where(c =>
                    (c.Fullname.ToLower().Contains(lowerCaseQuery)) ||
                    (c.Relationship.ToLower().Contains(lowerCaseQuery)) ||
                    (c.Phonenumber.Contains(searchQuery)) || // SĐT không cần ToLower
                    (c.Teacher != null && c.Teacher.Fullname.ToLower().Contains(lowerCaseQuery)) ||
                    (c.Student != null && c.Student.Fullname.ToLower().Contains(lowerCaseQuery))
                );
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ContactDto
                {
                    Contactid = c.Contactid,
                    Fullname = c.Fullname,
                    Relationship = c.Relationship,
                    Address = c.Address,
                    Phonenumber = c.Phonenumber,
                    TeacherName = c.Teacher != null ? c.Teacher.Fullname : null,
                }).ToListAsync();

            return new PagedResponse<ContactDto>(pagedData, pageNumber, pageSize, totalRecords);
        }

        public async Task<ContactDto?> GetByIdAsync(int id)
        {
            return await _context.Contacts
                .Where(c => c.Contactid == id)
                .Include(c => c.Teacher)
                .Include(c => c.Student)
                .Select(c => new ContactDto
                {
                    Contactid = c.Contactid,
                    Fullname = c.Fullname,
                    Relationship = c.Relationship,
                    Address = c.Address,
                    Phonenumber = c.Phonenumber,
                    TeacherName = c.Teacher != null ? c.Teacher.Fullname : null,
                }).FirstOrDefaultAsync();
        }

        public async Task<Contact> CreateAsync(ContactRequestDto contactDto)
        {
            var contact = new Contact
            {
                Fullname = contactDto.Fullname,
                Relationship = contactDto.Relationship,
                Address = contactDto.Address,
                Phonenumber = contactDto.Phonenumber,
                Teacherid = contactDto.Teacherid,
                Studentid = contactDto.Studentid
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact?> UpdateAsync(int id, ContactRequestDto updatedDto)
        {
            var existing = await _context.Contacts.FindAsync(id);
            if (existing == null) return null;

            existing.Fullname = updatedDto.Fullname;
            existing.Relationship = updatedDto.Relationship;
            existing.Address = updatedDto.Address;
            existing.Phonenumber = updatedDto.Phonenumber;
            existing.Teacherid = updatedDto.Teacherid;
            existing.Studentid = updatedDto.Studentid;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null) return false;

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}