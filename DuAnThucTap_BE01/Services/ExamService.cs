using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class ExamService : IExamService
    {
        private readonly ISCDbContext _context;

        public ExamService(ISCDbContext context)
        {
            _context = context;
        }

        // THAY ĐỔI: Nhận các tham số riêng lẻ
        public async Task<PagedResponse<ExamResponseDto>> GetPagedExamsAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Exams.AsNoTracking();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var searchTermLower = searchQuery.ToLower();
                query = query.Where(e => e.Examname.ToLower().Contains(searchTermLower));
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .OrderByDescending(e => e.Examdate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new ExamResponseDto
                {
                    ExamId = e.Examid,
                    ExamName = e.Examname,
                    ExamDate = e.Examdate,
                    DurationMinutes = e.Durationminutes,
                    CreatedAt = e.Createdat,
                    SchoolyearName = e.Schoolyear != null ? e.Schoolyear.Schoolyearname : null,
                    GradelevelName = e.Gradelevel != null ? e.Gradelevel.Gradelevelname : null,
                    SemesterName = e.Semester != null ? e.Semester.Semestername : null,
                    SubjectName = e.Subject != null ? e.Subject.Subjectname : null
                })
                .ToListAsync();

            return new PagedResponse<ExamResponseDto>(pagedData, pageNumber, pageSize, totalRecords);
        }

        public async Task<ExamResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Exams
                .Where(e => e.Examid == id)
                .Select(e => new ExamResponseDto
                {
                    ExamId = e.Examid,
                    ExamName = e.Examname,
                    ExamDate = e.Examdate,
                    DurationMinutes = e.Durationminutes,
                    CreatedAt = e.Createdat,
                    SchoolyearName = e.Schoolyear != null ? e.Schoolyear.Schoolyearname : null,
                    GradelevelName = e.Gradelevel != null ? e.Gradelevel.Gradelevelname : null,
                    SemesterName = e.Semester != null ? e.Semester.Semestername : null,
                    SubjectName = e.Subject != null ? e.Subject.Subjectname : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Exam> CreateAsync(Exam exam)
        {
            exam.Createdat = DateTime.UtcNow;
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task<Exam?> UpdateAsync(int id, Exam updatedExam)
        {
            var existingExam = await _context.Exams.FindAsync(id);
            if (existingExam == null) return null;

            existingExam.Schoolyearid = updatedExam.Schoolyearid;
            existingExam.Gradelevelid = updatedExam.Gradelevelid;
            existingExam.Semesterid = updatedExam.Semesterid;
            existingExam.Subjectid = updatedExam.Subjectid;
            existingExam.Examname = updatedExam.Examname;
            existingExam.Examdate = updatedExam.Examdate;
            existingExam.Durationminutes = updatedExam.Durationminutes;
            existingExam.Classtypeid = updatedExam.Classtypeid;
            existingExam.Graderassignmenttypeid = updatedExam.Graderassignmenttypeid;

            await _context.SaveChangesAsync();
            return existingExam;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null) return false;
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
