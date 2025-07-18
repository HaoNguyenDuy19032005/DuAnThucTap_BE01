using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
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

        public async Task<IEnumerable<TeacherConcurrentSubjectDto>> GetAllAsync()
        {
            return await _context.Teacherconcurrentsubjects
                .Include(tcs => tcs.Teacher)
                .Include(tcs => tcs.Subject)
                .Include(tcs => tcs.Schoolyear)
                .Select(tcs => new TeacherConcurrentSubjectDto
                {
                    TeacherId = tcs.Teacherid,
                    TeacherName = tcs.Teacher != null ? tcs.Teacher.Fullname : null, // Thêm kiểm tra null an toàn
                    SubjectId = tcs.Subjectid,
                    SubjectName = tcs.Subject != null ? tcs.Subject.Subjectname : null, // Thêm kiểm tra null an toàn
                    SchoolyearId = tcs.Schoolyearid,
                    SchoolyearName = tcs.Schoolyear != null ? tcs.Schoolyear.Schoolyearname : null // Thêm kiểm tra null an toàn
                }).ToListAsync();
        }

        public async Task<TeacherConcurrentSubjectDto?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId)
        {
            return await _context.Teacherconcurrentsubjects
                .Where(tcs => tcs.Teacherid == teacherId && tcs.Subjectid == subjectId && tcs.Schoolyearid == schoolYearId)
                .Include(tcs => tcs.Teacher)
                .Include(tcs => tcs.Subject)
                .Include(tcs => tcs.Schoolyear)
                .Select(tcs => new TeacherConcurrentSubjectDto
                {
                    TeacherId = tcs.Teacherid,
                    TeacherName = tcs.Teacher != null ? tcs.Teacher.Fullname : null,
                    SubjectId = tcs.Subjectid,
                    SubjectName = tcs.Subject != null ? tcs.Subject.Subjectname : null,
                    SchoolyearId = tcs.Schoolyearid,
                    SchoolyearName = tcs.Schoolyear != null ? tcs.Schoolyear.Schoolyearname : null
                }).FirstOrDefaultAsync();
        }

        // Cập nhật phương thức CreateAsync để nhận DTO
        public async Task<(bool Succeeded, string? ErrorMessage, Teacherconcurrentsubject? CreatedAssignment)> CreateAsync(TeacherConcurrentSubjectRequestDto assignmentDto)
        {
            bool isDuplicate = await _context.Teacherconcurrentsubjects.AnyAsync(tcs =>
                tcs.Teacherid == assignmentDto.TeacherId &&
                tcs.Subjectid == assignmentDto.SubjectId &&
                tcs.Schoolyearid == assignmentDto.SchoolYearId);

            if (isDuplicate)
            {
                return (false, "Phân công này đã tồn tại.", null);
            }

            // Ánh xạ từ DTO sang Model Entity
            var assignment = new Teacherconcurrentsubject
            {
                Teacherid = assignmentDto.TeacherId,
                Subjectid = assignmentDto.SubjectId,
                Schoolyearid = assignmentDto.SchoolYearId
            };

            _context.Teacherconcurrentsubjects.Add(assignment);
            await _context.SaveChangesAsync();
            return (true, null, assignment);
        }

        public async Task<bool> DeleteAsync(int teacherId, int subjectId, int schoolYearId)
        {
            var assignment = await _context.Teacherconcurrentsubjects
                                         .FindAsync(teacherId, subjectId, schoolYearId);
            if (assignment == null) return false;

            _context.Teacherconcurrentsubjects.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}