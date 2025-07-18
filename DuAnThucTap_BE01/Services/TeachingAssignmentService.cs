using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeachingAssignmentService : ITeachingAssignmentService
    {
        private readonly ISCDbContext _context;

        public TeachingAssignmentService(ISCDbContext context)
        {
            _context = context;
        }

        // Helper để thêm các .Include()
        private IQueryable<Teachingassignment> GetQueryWithIncludes()
        {
            return _context.Teachingassignments
                .Include(t => t.Teacher)
                .Include(t => t.Subject)
                .Include(t => t.Schoolyear)
                .Include(t => t.Classtype)
                .Include(t => t.Topic);
        }

        public async Task<IEnumerable<Teachingassignment>> GetAllAsync()
        {
            // Sử dụng helper
            return await GetQueryWithIncludes().ToListAsync();
        }

        public async Task<Teachingassignment?> GetByIdAsync(int id)
        {
            // Sử dụng helper
            return await GetQueryWithIncludes()
                         .FirstOrDefaultAsync(t => t.Assignmentid == id);
        }

        private async Task ValidateForeignKeysAsync(Teachingassignment assignment)
        {
            if (!await _context.Teachers.AnyAsync(t => t.Teacherid == assignment.Teacherid))
                throw new ArgumentException("Mã giáo viên không tồn tại.");
            if (!await _context.Subjects.AnyAsync(s => s.Subjectid == assignment.Subjectid))
                throw new ArgumentException("Mã môn học không tồn tại.");
            if (!await _context.Schoolyears.AnyAsync(sy => sy.Schoolyearid == assignment.Schoolyearid))
                throw new ArgumentException("Mã năm học không tồn tại.");
            if (assignment.Classtypeid.HasValue && !await _context.Classtypes.AnyAsync(ct => ct.Classtypeid == assignment.Classtypeid))
                throw new ArgumentException("Mã loại lớp không tồn tại.");
            if (assignment.Topicid.HasValue && !await _context.Topiclists.AnyAsync(t => t.Topicid == assignment.Topicid))
                throw new ArgumentException("Mã chủ đề không tồn tại.");
        }

        public async Task<Teachingassignment> CreateAsync(Teachingassignment teachingAssignment)
        {
            // Bắt lỗi ngày
            if (teachingAssignment.Teachingstartdate.HasValue && teachingAssignment.Teachingenddate.HasValue && teachingAssignment.Teachingenddate < teachingAssignment.Teachingstartdate)
            {
                throw new ArgumentException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
            }

            await ValidateForeignKeysAsync(teachingAssignment);

            _context.Teachingassignments.Add(teachingAssignment);
            await _context.SaveChangesAsync();
            return teachingAssignment;
        }

        public async Task<Teachingassignment?> UpdateAsync(int id, Teachingassignment updatedAssignment)
        {
            var existing = await _context.Teachingassignments.FindAsync(id);
            if (existing == null) return null;

            // Bắt lỗi ngày
            if (updatedAssignment.Teachingstartdate.HasValue && updatedAssignment.Teachingenddate.HasValue && updatedAssignment.Teachingenddate < updatedAssignment.Teachingstartdate)
            {
                throw new ArgumentException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
            }

            await ValidateForeignKeysAsync(updatedAssignment);

            // Cập nhật các trường
            _context.Entry(existing).CurrentValues.SetValues(updatedAssignment);

            await _context.SaveChangesAsync();
            return existing;
        }

        // DeleteAsync giữ nguyên
        public async Task<bool> DeleteAsync(int id)
        {
            var assignment = await _context.Teachingassignments.FindAsync(id);
            if (assignment == null) return false;

            _context.Teachingassignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}