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
  
            var existing = await GetByIdAsync(assignment.Teacherid, assignment.Subjectid, assignment.Schoolyearid);
            if (existing != null)
            {
                throw new InvalidOperationException("This assignment already exists.");
            }

            _context.Teacherconcurrentsubjects.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        // Đã sửa các tham số từ Guid thành int cho khớp với interface
        public async Task<bool> DeleteAsync(int teacherId, int subjectId, int schoolYearId)
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

        // Đã sửa các tham số từ Guid thành int cho khớp với interface
        public async Task<Teacherconcurrentsubject?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId)
        {
            return await _context.Teacherconcurrentsubjects.FindAsync(teacherId, subjectId, schoolYearId);
        }
    }
}