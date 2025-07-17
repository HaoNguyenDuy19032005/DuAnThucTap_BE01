// Services/TeacherWorkStatusHistoryService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherWorkStatusHistoryService : ITeacherWorkStatusHistoryService
    {
        private readonly ISCDbContext _context;
        public TeacherWorkStatusHistoryService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherWorkStatusHistoryDto> CreateAsync(Teacherworkstatushistory history)
        {
            // Sử dụng transaction để đảm bảo cả hai thao tác cùng thành công hoặc thất bại
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Tìm giáo viên
                var teacher = await _context.Teachers.FindAsync(history.Teacherid);
                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy giáo viên với ID = {history.Teacherid}.");
                }

                // 2. Cập nhật trạng thái và thời gian cho giáo viên
                teacher.Status = history.Statustype;
                teacher.Updatedat = DateTime.UtcNow;

                // 3. Ra lệnh tường minh cho EF Core để theo dõi sự thay đổi này
                _context.Teachers.Update(teacher);

                // 4. Thêm bản ghi lịch sử mới
                history.Createdat = DateTime.UtcNow;
                _context.Teacherworkstatushistories.Add(history);

                // 5. Lưu tất cả thay đổi (cả INSERT và UPDATE)
                await _context.SaveChangesAsync();

                // 6. Hoàn tất giao dịch
                await transaction.CommitAsync();

                // Trả về DTO với đầy đủ thông tin
                return new TeacherWorkStatusHistoryDto
                {
                    Historyid = history.Historyid,
                    Teacherid = history.Teacherid,
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
                // Nếu có lỗi, hủy bỏ tất cả thay đổi
                await transaction.RollbackAsync();
                throw; // Ném lại lỗi để Controller xử lý
            }
        }

        // ... Các phương thức khác được cập nhật để dùng DTO
        public async Task<IEnumerable<TeacherWorkStatusHistoryDto>> GetAllAsync()
        {
            return await _context.Teacherworkstatushistories
                .Include(h => h.Teacher)
                .Select(h => new TeacherWorkStatusHistoryDto
                {
                    //... ánh xạ các trường như CreateAsync
                    Historyid = h.Historyid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher.Fullname,
                    Statustype = h.Statustype,
                    Startdate = h.Startdate,
                    Enddate = h.Enddate,
                    Note = h.Note,
                    Decisionfileurl = h.Decisionfileurl,
                    Createdat = h.Createdat
                }).ToListAsync();
        }

        public async Task<TeacherWorkStatusHistoryDto?> GetByIdAsync(int id)
        {
            return await _context.Teacherworkstatushistories
                .Where(h => h.Historyid == id)
                .Include(h => h.Teacher)
                .Select(h => new TeacherWorkStatusHistoryDto
                {
                    //... ánh xạ các trường như CreateAsync
                    Historyid = h.Historyid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher.Fullname,
                    Statustype = h.Statustype,
                    Startdate = h.Startdate,
                    Enddate = h.Enddate,
                    Note = h.Note,
                    Decisionfileurl = h.Decisionfileurl,
                    Createdat = h.Createdat
                }).FirstOrDefaultAsync();
        }

        public async Task<Teacherworkstatushistory?> UpdateAsync(int id, Teacherworkstatushistory updatedHistory)
        {
            var existing = await _context.Teacherworkstatushistories.FindAsync(id);
            if (existing == null) return null;

            existing.Teacherid = updatedHistory.Teacherid;
            existing.Statustype = updatedHistory.Statustype;
            existing.Startdate = updatedHistory.Startdate;
            existing.Enddate = updatedHistory.Enddate;
            existing.Note = updatedHistory.Note;
            existing.Decisionfileurl = updatedHistory.Decisionfileurl;

            await _context.SaveChangesAsync();
            return existing;
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