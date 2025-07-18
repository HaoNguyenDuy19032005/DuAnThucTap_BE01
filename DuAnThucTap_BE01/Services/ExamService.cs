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

        public async Task<PagedResponse<ExamResponseDto>> GetPagedExamsAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            // --- SỬA LỖI: Thêm các .Include() để tải dữ liệu liên quan ---
            var query = _context.Exams
                .Include(e => e.Schoolyear)
                .Include(e => e.Gradelevel)
                .Include(e => e.Semester)
                .Include(e => e.Subject)
                .AsNoTracking();

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
                    // Giờ đây e.Schoolyear, e.Gradelevel... sẽ có dữ liệu
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
            // --- SỬA LỖI: Thêm các .Include() để tải dữ liệu liên quan ---
            return await _context.Exams
                .Include(e => e.Schoolyear)
                .Include(e => e.Gradelevel)
                .Include(e => e.Semester)
                .Include(e => e.Subject)
                .AsNoTracking()
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
