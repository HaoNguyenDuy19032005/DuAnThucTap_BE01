using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Helpers;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TeachingAssignmentService : ITeachingAssignmentService
    {
        private readonly ISCDbContext _context;

        public TeachingAssignmentService(ISCDbContext context)
        {
            _context = context;
        }

        // --- Phương thức GetAllAsync và GetByIdAsync giữ nguyên ---
        public async Task<PagedResult<Teachingassignment>> GetAllAsync(string? searchTerm, int pageNumber, int pageSize)
        {
            var query = _context.Teachingassignments
                .Include(t => t.Teacher)
                .Include(t => t.Subject)
                .Include(t => t.Classtype)
                .Include(t => t.Topic)
                .Include(t => t.Schoolyear)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
                query = query.Where(t =>
                    (t.Teacher.Fullname != null && t.Teacher.Fullname.ToLower().Contains(lowerCaseSearchTerm)) ||
                    (t.Subject.Subjectname != null && t.Subject.Subjectname.ToLower().Contains(lowerCaseSearchTerm)) ||
                    (t.Classtype.Classtypename != null && t.Classtype.Classtypename.ToLower().Contains(lowerCaseSearchTerm)) ||
                    (t.Topic.Topicname != null && t.Topic.Topicname.ToLower().Contains(lowerCaseSearchTerm)) ||
                    (t.Notes != null && t.Notes.ToLower().Contains(lowerCaseSearchTerm))
                );
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Teachingassignment>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Teachingassignment?> GetByIdAsync(int id)
        {
            return await _context.Teachingassignments
                .Include(t => t.Teacher)
                .Include(t => t.Subject)
                .Include(t => t.Classtype)
                .Include(t => t.Topic)
                .Include(t => t.Schoolyear)
                .FirstOrDefaultAsync(t => t.Assignmentid == id);
        }

        /// <summary>
        /// Helper private để kiểm tra sự tồn tại của các khóa ngoại.
        /// </summary>
        private async Task ValidateForeignKeysAsync(Teachingassignment teachingAssignment)
        {
            var teacherExists = await _context.Teachers.AnyAsync(t => t.Teacherid == teachingAssignment.Teacherid);
            if (!teacherExists)
                throw new ArgumentException($"Giáo viên với ID {teachingAssignment.Teacherid} không tồn tại.");

            var subjectExists = await _context.Subjects.AnyAsync(s => s.Subjectid == teachingAssignment.Subjectid);
            if (!subjectExists)
                throw new ArgumentException($"Môn học với ID {teachingAssignment.Subjectid} không tồn tại.");

            var schoolYearExists = await _context.Schoolyears.AnyAsync(sy => sy.Schoolyearid == teachingAssignment.Schoolyearid);
            if (!schoolYearExists)
                throw new ArgumentException($"Năm học với ID {teachingAssignment.Schoolyearid} không tồn tại.");

            if (teachingAssignment.Classtypeid.HasValue)
            {
                var classTypeExists = await _context.Classtypes.AnyAsync(ct => ct.Classtypeid == teachingAssignment.Classtypeid.Value);
                if (!classTypeExists)
                    throw new ArgumentException($"Loại lớp với ID {teachingAssignment.Classtypeid.Value} không tồn tại.");
            }

            if (teachingAssignment.Topicid.HasValue)
            {
                var topicExists = await _context.Topiclists.AnyAsync(tp => tp.Topicid == teachingAssignment.Topicid.Value);
                if (!topicExists)
                    throw new ArgumentException($"Chủ đề với ID {teachingAssignment.Topicid.Value} không tồn tại.");
            }
        }

        /// <summary>
        /// Helper private để kiểm tra logic ngày tháng.
        /// </summary>
        private void ValidateDates(Teachingassignment teachingAssignment)
        {
            if (teachingAssignment.Teachingstartdate.HasValue && teachingAssignment.Teachingenddate.HasValue)
            {
                if (teachingAssignment.Teachingenddate.Value < teachingAssignment.Teachingstartdate.Value)
                {
                    throw new ArgumentException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
                }
            }
        }

        public async Task<Teachingassignment> CreateAsync(Teachingassignment teachingAssignment)
        {
            // 1. Kiểm tra các ID (khóa ngoại)
            await ValidateForeignKeysAsync(teachingAssignment);

            // 2. **FIX**: Kiểm tra logic ngày tháng
            ValidateDates(teachingAssignment);

            _context.Teachingassignments.Add(teachingAssignment);
            await _context.SaveChangesAsync();
            return teachingAssignment;
        }

        public async Task<Teachingassignment?> UpdateAsync(int id, Teachingassignment teachingAssignment)
        {
            var existing = await _context.Teachingassignments.FindAsync(id);
            if (existing == null) return null;

            // 1. Kiểm tra các ID (khóa ngoại)
            await ValidateForeignKeysAsync(teachingAssignment);

            // 2. **FIX**: Kiểm tra logic ngày tháng
            ValidateDates(teachingAssignment);

            // 3. **FIX**: Cập nhật từng trường một cách thủ công để đảm bảo an toàn
            existing.Teacherid = teachingAssignment.Teacherid;
            existing.Subjectid = teachingAssignment.Subjectid;
            existing.Classtypeid = teachingAssignment.Classtypeid;
            existing.Topicid = teachingAssignment.Topicid;
            existing.Schoolyearid = teachingAssignment.Schoolyearid;
            existing.Teachingstartdate = teachingAssignment.Teachingstartdate;
            existing.Teachingenddate = teachingAssignment.Teachingenddate;
            existing.Notes = teachingAssignment.Notes;

            await _context.SaveChangesAsync();
            return existing;
        }

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
