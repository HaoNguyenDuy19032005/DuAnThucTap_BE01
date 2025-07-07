// Services/TeacherConcurrentSubjectService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherConcurrentSubjectService : ITeacherConcurrentSubjectService
    {
        private readonly ISCDbContext _context;

        public TeacherConcurrentSubjectService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Teacherconcurrentsubject> CreateAsync(Teacherconcurrentsubject assignment)
        {
            // Kiểm tra xem phân công đã tồn tại chưa để tránh lỗi khóa chính
            var existing = await GetByIdAsync(assignment.Teacherid, assignment.Subjectid, assignment.Schoolyearid);
            if (existing != null)
            {
                throw new InvalidOperationException("This assignment already exists.");
            }

            _context.Teacherconcurrentsubjects.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<bool> DeleteAsync(Guid teacherId, Guid subjectId, Guid schoolYearId)
        {
            var assignment = await GetByIdAsync(teacherId, subjectId, schoolYearId);
            if (assignment == null) return false;

            _context.Teacherconcurrentsubjects.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Teacherconcurrentsubject>> GetAllAsync()
        {
            return await _context.Teacherconcurrentsubjects.ToListAsync();
        }

        public async Task<Teacherconcurrentsubject?> GetByIdAsync(Guid teacherId, Guid subjectId, Guid schoolYearId)
        {
            return await _context.Teacherconcurrentsubjects.FindAsync(teacherId, subjectId, schoolYearId);
        }
    }
}