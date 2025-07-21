using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTOs;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Service
{
    public class TestStudentSubmissionService : ITestStudentSubmissionService
    {
        private readonly AppDbContext _context;

        public TestStudentSubmissionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TestStudentSubmission>> GetAllAsync(int page, int pageSize, int? studentId = null, int? testId = null)
        {
            var query = _context.TestStudentSubmissions.AsQueryable();

            if (studentId.HasValue)
                query = query.Where(x => x.StudentId == studentId);

            if (testId.HasValue)
                query = query.Where(x => x.TestId == testId);

            return await query
                .Include(x => x.Student)
                .Include(x => x.Test)
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TestStudentSubmission?> GetByIdAsync(int id)
        {
            return await _context.TestStudentSubmissions
                .Include(x => x.Student)
                .Include(x => x.Test)
                .FirstOrDefaultAsync(x => x.SubmissionId == id);
        }

        public async Task<TestStudentSubmission> CreateAsync(TestStudentSubmissionDto dto)
        {
            var submission = new TestStudentSubmission
            {
                TestId = dto.TestId ?? throw new ArgumentException("TestId is required"),
                StudentId = dto.StudentId ?? throw new ArgumentException("StudentId is required"),
                StartTime = dto.StartTime,
                SubmissionTime = dto.SubmissionTime,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            _context.TestStudentSubmissions.Add(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        public async Task<TestStudentSubmission?> UpdateAsync(int id, TestStudentSubmissionDto dto)
        {
            var existing = await _context.TestStudentSubmissions.FindAsync(id);
            if (existing == null) return null;

            existing.TestId = dto.TestId ?? existing.TestId;
            existing.StudentId = dto.StudentId ?? existing.StudentId;
            existing.StartTime = dto.StartTime;
            existing.SubmissionTime = dto.SubmissionTime;
            existing.Status = dto.Status;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.TestStudentSubmissions.FindAsync(id);
            if (existing == null) return false;

            _context.TestStudentSubmissions.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
