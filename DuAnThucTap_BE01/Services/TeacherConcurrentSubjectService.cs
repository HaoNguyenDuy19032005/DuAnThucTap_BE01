// Services/TeacherConcurrentSubjectService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
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
                    TeacherName = tcs.Teacher.Fullname,
                    SubjectId = tcs.Subjectid,
                    SubjectName = tcs.Subject.Subjectname,
                    SchoolyearId = tcs.Schoolyearid,
                    SchoolyearName = tcs.Schoolyear.Schoolyearname
                }).ToListAsync();
        }

        public async Task<TeacherConcurrentSubjectDto?> GetByIdAsync(int teacherId, int subjectId, int schoolYearId)
        {
            // FindAsync không hỗ trợ Include, ta phải dùng FirstOrDefaultAsync
            return await _context.Teacherconcurrentsubjects
                .Where(tcs => tcs.Teacherid == teacherId && tcs.Subjectid == subjectId && tcs.Schoolyearid == schoolYearId)
                .Include(tcs => tcs.Teacher)
                .Include(tcs => tcs.Subject)
                .Include(tcs => tcs.Schoolyear)
                .Select(tcs => new TeacherConcurrentSubjectDto
                {
                    TeacherId = tcs.Teacherid,
                    TeacherName = tcs.Teacher.Fullname,
                    SubjectId = tcs.Subjectid,
                    SubjectName = tcs.Subject.Subjectname,
                    SchoolyearId = tcs.Schoolyearid,
                    SchoolyearName = tcs.Schoolyear.Schoolyearname
                }).FirstOrDefaultAsync();
        }

        public async Task<(bool Succeeded, string? ErrorMessage)> CreateAsync(Teacherconcurrentsubject assignment)
        {
            bool isDuplicate = await _context.Teacherconcurrentsubjects.AnyAsync(tcs =>
                tcs.Teacherid == assignment.Teacherid &&
                tcs.Subjectid == assignment.Subjectid &&
                tcs.Schoolyearid == assignment.Schoolyearid);

            if (isDuplicate)
            {
                return (false, "Phân công này đã tồn tại.");
            }

            _context.Teacherconcurrentsubjects.Add(assignment);
            await _context.SaveChangesAsync();
            return (true, null);
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