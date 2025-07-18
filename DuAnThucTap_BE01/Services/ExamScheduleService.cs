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
    public class ExamScheduleService : IExamScheduleService
    {
        private readonly ISCDbContext _context;

        public ExamScheduleService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<ExamScheduleResponseDto>> GetPagedExamSchedulesAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Examschedules.AsNoTracking();

            // Tìm kiếm theo tên kỳ thi hoặc tên lớp
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var searchTermLower = searchQuery.ToLower();
                query = query.Where(es =>
                    (es.Exam != null && es.Exam.Examname.ToLower().Contains(searchTermLower)) ||
                    (es.Class != null && es.Class.Classname.ToLower().Contains(searchTermLower))
                );
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .OrderByDescending(es => es.Examscheduleid)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(es => new ExamScheduleResponseDto
                {
                    ExamScheduleId = es.Examscheduleid,
                    ExamId = es.Examid,
                    ClassId = es.Classid,
                    ExamName = es.Exam != null ? es.Exam.Examname : null,
                    ExamDate = es.Exam != null ? es.Exam.Examdate : null,
                    ClassName = es.Class != null ? es.Class.Classname : null,
                    SchoolYearName = es.Class != null && es.Class.Schoolyear != null ? es.Class.Schoolyear.Schoolyearname : null
                })
                .ToListAsync();

            return new PagedResponse<ExamScheduleResponseDto>(pagedData, pageNumber, pageSize, totalRecords);
        }

        public async Task<ExamScheduleResponseDto?> GetByIdAsync(int examScheduleId)
        {
            return await _context.Examschedules
                .Where(es => es.Examscheduleid == examScheduleId)
                .Select(es => new ExamScheduleResponseDto
                {
                    ExamScheduleId = es.Examscheduleid,
                    ExamId = es.Examid,
                    ClassId = es.Classid,
                    ExamName = es.Exam != null ? es.Exam.Examname : null,
                    ExamDate = es.Exam != null ? es.Exam.Examdate : null,
                    ClassName = es.Class != null ? es.Class.Classname : null,
                    SchoolYearName = es.Class != null && es.Class.Schoolyear != null ? es.Class.Schoolyear.Schoolyearname : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Examschedule> CreateAsync(ExamScheduleDto examScheduleDto)
        {
            // Kiểm tra trùng lặp
            var isDuplicate = await _context.Examschedules
                .AnyAsync(es => es.Examid == examScheduleDto.Examid && es.Classid == examScheduleDto.Classid);

            if (isDuplicate)
            {
                throw new InvalidOperationException("Lịch thi cho lớp này đã tồn tại.");
            }

            var examSchedule = new Examschedule
            {
                Examid = examScheduleDto.Examid,
                Classid = examScheduleDto.Classid
            };

            _context.Examschedules.Add(examSchedule);
            await _context.SaveChangesAsync();
            return examSchedule;
        }

        public async Task<Examschedule?> UpdateAsync(int examScheduleId, ExamScheduleDto updatedExamScheduleDto)
        {
            var existingExamSchedule = await _context.Examschedules.FindAsync(examScheduleId);
            if (existingExamSchedule == null) return null;

            // Kiểm tra trùng lặp khi cập nhật
            var isDuplicate = await _context.Examschedules
                .AnyAsync(es => es.Examid == updatedExamScheduleDto.Examid
                               && es.Classid == updatedExamScheduleDto.Classid
                               && es.Examscheduleid != examScheduleId); // Loại trừ bản ghi hiện tại

            if (isDuplicate)
            {
                throw new InvalidOperationException("Lịch thi cho lớp này đã tồn tại.");
            }

            existingExamSchedule.Examid = updatedExamScheduleDto.Examid;
            existingExamSchedule.Classid = updatedExamScheduleDto.Classid;

            await _context.SaveChangesAsync();
            return existingExamSchedule;
        }

        public async Task<bool> DeleteAsync(int examScheduleId)
        {
            var examSchedule = await _context.Examschedules.FindAsync(examScheduleId);
            if (examSchedule == null) return false;

            _context.Examschedules.Remove(examSchedule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
