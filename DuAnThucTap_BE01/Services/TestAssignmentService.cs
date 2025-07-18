using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TestassignmentService : ITestassignment
    {
        private readonly ISCDbContext _context;

        public TestassignmentService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TestAssignmentDto>> GetAllAsync(string? searchQuery, int page, int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                throw new ArgumentException("Trang hoặc kích thước trang không hợp lệ.");
            }

            var query = _context.Testassignments
                .Include(ta => ta.Test)
                .Include(ta => ta.Class)
                .Select(ta => new TestAssignmentDto
                {
                    Assignmentid = ta.Assignmentid,
                    TestTitle = ta.Test != null ? ta.Test.Title : null,
                    ClassName = ta.Class != null ? ta.Class.Classname : null,
                    Status = ta.Status
                });

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(ta => (ta.TestTitle != null && ta.TestTitle.ToLower().Contains(searchQuery)) ||
                                          (ta.ClassName != null && ta.ClassName.ToLower().Contains(searchQuery)));
            }

            query = query.OrderBy(ta => ta.Assignmentid).Skip((page - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<TestAssignmentDto?> GetByIdAsync(int id)
        {
            return await _context.Testassignments
                .Include(ta => ta.Test)
                .Include(ta => ta.Class)
                .Where(ta => ta.Assignmentid == id)
                .Select(ta => new TestAssignmentDto
                {
                    Assignmentid = ta.Assignmentid,
                    TestTitle = ta.Test != null ? ta.Test.Title : null,
                    ClassName = ta.Class != null ? ta.Class.Classname : null,
                    Status = ta.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TestAssignmentDto> CreateAsync(TestAssignmentRequestDto testassignment)
        {
            var test = await _context.Tests.FindAsync(testassignment.Testid);
            if (test == null)
                throw new ArgumentException("Testid không tồn tại.");

            var classEntity = await _context.Classes.FindAsync(testassignment.Classid);
            if (classEntity == null)
                throw new ArgumentException("Classid không tồn tại.");

            var existingAssignment = await _context.Testassignments
                .AnyAsync(ta => ta.Testid == testassignment.Testid && ta.Classid == testassignment.Classid);
            if (existingAssignment)
                throw new ArgumentException("Phân công bài kiểm tra cho lớp này đã tồn tại.");

            var newAssignment = new Testassignment
            {
                Testid = testassignment.Testid,
                Classid = testassignment.Classid,
                Status = testassignment.Status
            };

            _context.Testassignments.Add(newAssignment);
            await _context.SaveChangesAsync();

            return new TestAssignmentDto
            {
                Assignmentid = newAssignment.Assignmentid,
                TestTitle = test.Title,
                ClassName = classEntity.Classname,
                Status = newAssignment.Status
            };
        }

        public async Task<TestAssignmentDto?> UpdateAsync(int id, TestAssignmentRequestDto testassignment)
        {
            var existing = await _context.Testassignments.FindAsync(id);
            if (existing == null)
                return null;

            var test = await _context.Tests.FindAsync(testassignment.Testid);
            if (test == null)
                throw new ArgumentException("Testid không tồn tại.");

            var classEntity = await _context.Classes.FindAsync(testassignment.Classid);
            if (classEntity == null)
                throw new ArgumentException("Classid không tồn tại.");

            var existingAssignment = await _context.Testassignments
                .AnyAsync(ta => ta.Testid == testassignment.Testid && ta.Classid == testassignment.Classid && ta.Assignmentid != id);
            if (existingAssignment)
                throw new ArgumentException("Phân công bài kiểm tra cho lớp này đã tồn tại.");

            existing.Testid = testassignment.Testid;
            existing.Classid = testassignment.Classid;
            existing.Status = testassignment.Status;

            await _context.SaveChangesAsync();

            return new TestAssignmentDto
            {
                Assignmentid = existing.Assignmentid,
                TestTitle = test.Title,
                ClassName = classEntity.Classname,
                Status = existing.Status
            };
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