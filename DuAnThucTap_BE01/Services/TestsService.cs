using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TestsService : ITests
    {
        private readonly ISCDbContext _context;

        public TestsService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public async Task<Test?> GetByIdAsync(int id)
        {
            return await _context.Tests.FindAsync(id);
        }

        public async Task<Test> CreateAsync(Test test)
        {
            // Kiểm tra Teacherid tồn tại
            var teacher = await _context.Teachers.FindAsync(test.Teacherid);
            if (teacher == null)
            {
                throw new ArgumentException("Teacherid không tồn tại.");
            }

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<Test?> UpdateAsync(int id, Test test)
        {
            var existing = await _context.Tests.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            // Kiểm tra Teacherid tồn tại
            var teacher = await _context.Teachers.FindAsync(test.Teacherid);
            if (teacher == null)
            {
                throw new ArgumentException("Teacherid không tồn tại.");
            }

            existing.Teacherid = test.Teacherid;
            existing.Title = test.Title;
            existing.Testformat = test.Testformat;
            existing.Durationinminutes = test.Durationinminutes;
            existing.Starttime = test.Starttime;
            existing.Endtime = test.Endtime;
            existing.Description = test.Description;
            existing.Classification = test.Classification;
            existing.Attachmenturl = test.Attachmenturl;
            existing.Requirestudentattachment = test.Requirestudentattachment;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return false;
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}