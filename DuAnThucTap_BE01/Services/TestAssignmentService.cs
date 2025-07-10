using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Services;

namespace DuAnThucTap_BE01.Services
{
    public class TestassignmentService : ITestassignment
    {
        private readonly ISCDbContext _context;

        public TestassignmentService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Testassignment>> GetAllAsync()
        {
            return await _context.Testassignments.ToListAsync();
        }

        public async Task<Testassignment?> GetByIdAsync(int id)
        {
            return await _context.Testassignments.FindAsync(id);
        }

        public async Task<Testassignment> CreateAsync(Testassignment testassignment)
        {
            var test = await _context.Tests.FindAsync(testassignment.Testid);
            if (test == null)
                throw new ArgumentException("Testid does not exist.");

            var classEntity = await _context.Classes.FindAsync(testassignment.Classid);
            if (classEntity == null)
                throw new ArgumentException("Classid does not exist.");

            _context.Testassignments.Add(testassignment);
            await _context.SaveChangesAsync();
            return testassignment;
        }

        public async Task<Testassignment?> UpdateAsync(int id, Testassignment testassignment)
        {
            var existing = await _context.Testassignments.FindAsync(id);
            if (existing == null)
                return null;

            var test = await _context.Tests.FindAsync(testassignment.Testid);
            if (test == null)
                throw new ArgumentException("Testid does not exist.");

            var classEntity = await _context.Classes.FindAsync(testassignment.Classid);
            if (classEntity == null)
                throw new ArgumentException("Classid does not exist.");

            existing.Testid = testassignment.Testid;
            existing.Classid = testassignment.Classid;
            existing.Status = testassignment.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var testassignment = await _context.Testassignments.FindAsync(id);
            if (testassignment == null)
                return false;

            _context.Testassignments.Remove(testassignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}