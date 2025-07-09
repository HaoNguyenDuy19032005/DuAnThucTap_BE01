using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TestAssignmentService : ITestAssignment
    {
        private readonly ISCDbContext _context;

        public TestAssignmentService(ISCDbContext context)
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

        public async Task<Testassignment> CreateAsync(Testassignment testAssignment)
        {
            // Kiểm tra Testid và Classid tồn tại
            var test = await _context.Tests.FindAsync(testAssignment.Testid);
            var classEntity = await _context.Classes.FindAsync(testAssignment.Classid);
            if (test == null || classEntity == null)
            {
                throw new ArgumentException("Testid hoặc Classid không tồn tại.");
            }

            // Kiểm tra ràng buộc unique (testid, classid)
            var existingAssignment = await _context.Testassignments
                .FirstOrDefaultAsync(ta => ta.Testid == testAssignment.Testid && ta.Classid == testAssignment.Classid);
            if (existingAssignment != null)
            {
                throw new ArgumentException("Testassignment với Testid và Classid này đã tồn tại.");
            }

            _context.Testassignments.Add(testAssignment);
            await _context.SaveChangesAsync();
            return testAssignment;
        }

        public async Task<Testassignment?> UpdateAsync(int id, Testassignment testAssignment)
        {
            var existing = await _context.Testassignments.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            // Kiểm tra Testid và Classid tồn tại
            var test = await _context.Tests.FindAsync(testAssignment.Testid);
            var classEntity = await _context.Classes.FindAsync(testAssignment.Classid);
            if (test == null || classEntity == null)
            {
                throw new ArgumentException("Testid hoặc Classid không tồn tại.");
            }

            // Kiểm tra ràng buộc unique (testid, classid) nếu thay đổi
            var duplicate = await _context.Testassignments
                .FirstOrDefaultAsync(ta => ta.Testid == testAssignment.Testid && ta.Classid == testAssignment.Classid && ta.Assignmentid != id);
            if (duplicate != null)
            {
                throw new ArgumentException("Testassignment với Testid và Classid này đã tồn tại.");
            }

            existing.Testid = testAssignment.Testid;
            existing.Classid = testAssignment.Classid;
            existing.Status = testAssignment.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var testAssignment = await _context.Testassignments.FindAsync(id);
            if (testAssignment == null)
            {
                return false;
            }

            _context.Testassignments.Remove(testAssignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}