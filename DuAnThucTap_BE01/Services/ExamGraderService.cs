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
    public class ExamGraderService : IExamGraderService
    {
        private readonly ISCDbContext _context;

        public ExamGraderService(ISCDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagedResponse<ExamGraderResponseDto>> GetPagedExamGradersAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Examgraders.AsNoTracking();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var searchTermLower = searchQuery.ToLower();
                query = query.Where(eg => eg.Teacher != null && eg.Teacher.Fullname != null && eg.Teacher.Fullname.ToLower().Contains(searchTermLower));
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .OrderByDescending(eg => eg.Examgraderid)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(eg => new ExamGraderResponseDto
                {
                    ExamGraderId = eg.Examgraderid,
                    ExamScheduleId = eg.Examscheduleid,
                    TeacherId = eg.Teacherid,
                    TeacherName = eg.Teacher != null ? eg.Teacher.Fullname : null,
                    TeacherCode = eg.Teacher != null ? eg.Teacher.Teachercode : null,
                    ExamName = eg.Examschedule != null && eg.Examschedule.Exam != null ? eg.Examschedule.Exam.Examname : null,
                    ExamDate = eg.Examschedule != null && eg.Examschedule.Exam != null ? eg.Examschedule.Exam.Examdate : null,
                    ClassName = eg.Examschedule != null && eg.Examschedule.Class != null ? eg.Examschedule.Class.Classname : null
                })
                .ToListAsync();

            return new PagedResponse<ExamGraderResponseDto>(pagedData, pageNumber, pageSize, totalRecords);
        }

        public async Task<ExamGraderResponseDto?> GetByIdAsync(int examGraderId)
        {
            return await _context.Examgraders
                .Where(eg => eg.Examgraderid == examGraderId)
                .Select(eg => new ExamGraderResponseDto
                {
                    ExamGraderId = eg.Examgraderid,
                    ExamScheduleId = eg.Examscheduleid,
                    TeacherId = eg.Teacherid,
                    TeacherName = eg.Teacher != null ? eg.Teacher.Fullname : null,
                    TeacherCode = eg.Teacher != null ? eg.Teacher.Teachercode : null,
                    ExamName = eg.Examschedule != null && eg.Examschedule.Exam != null ? eg.Examschedule.Exam.Examname : null,
                    ExamDate = eg.Examschedule != null && eg.Examschedule.Exam != null ? eg.Examschedule.Exam.Examdate : null,
                    ClassName = eg.Examschedule != null && eg.Examschedule.Class != null ? eg.Examschedule.Class.Classname : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Examgrader> CreateAsync(ExamGraderDto examGraderDto)
        {
            // --- SỬA LỖI: Thêm logic kiểm tra trùng lặp ---
            var isDuplicate = await _context.Examgraders
                .AnyAsync(eg => eg.Examscheduleid == examGraderDto.Examscheduleid && eg.Teacherid == examGraderDto.Teacherid);

            if (isDuplicate)
            {
                // Ném ra một lỗi rõ ràng để Controller có thể bắt được
                throw new InvalidOperationException("Giáo viên này đã được phân công cho lịch thi này.");
            }

            var examGrader = new Examgrader
            {
                Examscheduleid = examGraderDto.Examscheduleid,
                Teacherid = examGraderDto.Teacherid
            };

            _context.Examgraders.Add(examGrader);
            await _context.SaveChangesAsync();
            return examGrader;
        }

        public async Task<Examgrader?> UpdateAsync(int examGraderId, ExamGraderDto updatedExamGraderDto)
        {
            var existingExamGrader = await _context.Examgraders.FindAsync(examGraderId);
            if (existingExamGrader == null) return null;

            // --- SỬA LỖI: Thêm logic kiểm tra trùng lặp khi cập nhật ---
            var isDuplicate = await _context.Examgraders
                .AnyAsync(eg => eg.Examscheduleid == updatedExamGraderDto.Examscheduleid
                               && eg.Teacherid == updatedExamGraderDto.Teacherid
                               && eg.Examgraderid != examGraderId); // Loại trừ bản ghi đang được cập nhật

            if (isDuplicate)
            {
                throw new InvalidOperationException("Giáo viên này đã được phân công cho lịch thi này.");
            }

            existingExamGrader.Examscheduleid = updatedExamGraderDto.Examscheduleid;
            existingExamGrader.Teacherid = updatedExamGraderDto.Teacherid;

            await _context.SaveChangesAsync();
            return existingExamGrader;
        }

        public async Task<bool> DeleteAsync(int examGraderId)
        {
            var examGrader = await _context.Examgraders.FindAsync(examGraderId);
            if (examGrader == null) return false;

            _context.Examgraders.Remove(examGrader);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
