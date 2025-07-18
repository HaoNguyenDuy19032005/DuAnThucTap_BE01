using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherWorkStatusHistoryService : ITeacherWorkStatusHistoryService
    {
        private readonly ISCDbContext _context;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public TeacherWorkStatusHistoryService(ISCDbContext context, IFirebaseStorageService firebaseStorageService)
        {
            _context = context;
            _firebaseStorageService = firebaseStorageService;
        }

        public async Task<PagedResponse<TeacherWorkStatusHistoryDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Teacherworkstatushistories
                .Include(h => h.Teacher)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerCaseQuery = searchQuery.ToLower();
                query = query.Where(h =>
                    (h.Teacher != null && h.Teacher.Fullname.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Statustype.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Note != null && h.Note.ToLower().Contains(lowerCaseQuery))
                );
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .OrderByDescending(h => h.Startdate) // Sắp xếp theo ngày bắt đầu gần nhất
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(h => new TeacherWorkStatusHistoryDto
                {
                    Historyid = h.Historyid,
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null,
                    Statustype = h.Statustype,
                    Startdate = h.Startdate,
                    Enddate = h.Enddate,
                    Note = h.Note,
                    Decisionfileurl = h.Decisionfileurl,
                    Createdat = h.Createdat
                }).ToListAsync();

            return new PagedResponse<TeacherWorkStatusHistoryDto>(pagedData, pageNumber, pageSize, totalRecords);
        }

        public async Task<TeacherWorkStatusHistoryDto?> GetByIdAsync(int id)
        {
            return await _context.Teacherworkstatushistories
                .Where(h => h.Historyid == id)
                .Include(h => h.Teacher)
                .Select(h => new TeacherWorkStatusHistoryDto
                {
                    Historyid = h.Historyid,
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null,
                    Statustype = h.Statustype,
                    Startdate = h.Startdate,
                    Enddate = h.Enddate,
                    Note = h.Note,
                    Decisionfileurl = h.Decisionfileurl,
                    Createdat = h.Createdat
                }).FirstOrDefaultAsync();
        }

        public async Task<TeacherWorkStatusHistoryDto> CreateAsync(TeacherWorkStatusHistoryRequestDto historyDto, IFormFile? file)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Tìm giáo viên
                var teacher = await _context.Teachers.FindAsync(historyDto.Teacherid);
                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy giáo viên với ID = {historyDto.Teacherid}.");
                }

                // 2. Xử lý tải file
                string? decisionFileUrl = null;
                if (file != null && file.Length > 0)
                {
                    decisionFileUrl = await _firebaseStorageService.UploadFileAsync(file, "teacher-status-decisions");
                }

                // 3. Cập nhật trạng thái cho giáo viên
                teacher.Status = historyDto.Statustype;
                teacher.Updatedat = DateTime.UtcNow;

                // 4. Chuyển đổi ngày tháng từ string sang DateOnly
                DateOnly.TryParse(historyDto.Startdate, out var startDate);
                DateOnly.TryParse(historyDto.Enddate, out var endDate);

                // 5. Tạo bản ghi lịch sử mới
                var history = new Teacherworkstatushistory
                {
                    Teacherid = historyDto.Teacherid,
                    Statustype = historyDto.Statustype,
                    Startdate = startDate,
                    Enddate = string.IsNullOrEmpty(historyDto.Enddate) ? null : endDate,
                    Note = historyDto.Note,
                    Decisionfileurl = decisionFileUrl,
                    Createdat = DateTime.UtcNow
                };

                _context.Teacherworkstatushistories.Add(history);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // 6. Trả về DTO đã được tạo
                return new TeacherWorkStatusHistoryDto
                {
                    Historyid = history.Historyid,
                    TeacherName = teacher.Fullname,
                    Statustype = history.Statustype,
                    Startdate = history.Startdate,
                    Enddate = history.Enddate,
                    Note = history.Note,
                    Decisionfileurl = history.Decisionfileurl,
                    Createdat = history.Createdat
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<TeacherWorkStatusHistoryDto?> UpdateAsync(int id, TeacherWorkStatusHistoryRequestDto updatedHistoryDto, IFormFile? file)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.Teacherworkstatushistories.FindAsync(id);
                if (existing == null) return null;

                // 1. Xử lý tải file (nếu có file mới)
                if (file != null && file.Length > 0)
                {
                    existing.Decisionfileurl = await _firebaseStorageService.UploadFileAsync(file, "teacher-status-decisions");
                }
                else
                {
                    existing.Decisionfileurl = updatedHistoryDto.Decisionfileurl; // Giữ lại URL cũ nếu không có file mới
                }

                // 2. Chuyển đổi ngày tháng từ string sang DateOnly
                DateOnly.TryParse(updatedHistoryDto.Startdate, out var startDate);
                DateOnly.TryParse(updatedHistoryDto.Enddate, out var endDate);

                // 3. Cập nhật các thuộc tính
                existing.Teacherid = updatedHistoryDto.Teacherid;
                existing.Statustype = updatedHistoryDto.Statustype;
                existing.Startdate = startDate;
                existing.Enddate = string.IsNullOrEmpty(updatedHistoryDto.Enddate) ? null : endDate;
                existing.Note = updatedHistoryDto.Note;

                // 4. Cập nhật trạng thái của giáo viên liên quan
                var teacher = await _context.Teachers.FindAsync(updatedHistoryDto.Teacherid);
                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy giáo viên với ID = {updatedHistoryDto.Teacherid}.");
                }
                teacher.Status = updatedHistoryDto.Statustype;
                teacher.Updatedat = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // 5. Trả về DTO đã được cập nhật
                return new TeacherWorkStatusHistoryDto
                {
                    Historyid = existing.Historyid,
                    TeacherName = teacher.Fullname,
                    Statustype = existing.Statustype,
                    Startdate = existing.Startdate,
                    Enddate = existing.Enddate,
                    Note = existing.Note,
                    Decisionfileurl = existing.Decisionfileurl,
                    Createdat = existing.Createdat
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var history = await _context.Teacherworkstatushistories.FindAsync(id);
            if (history == null) return false;

            _context.Teacherworkstatushistories.Remove(history);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}