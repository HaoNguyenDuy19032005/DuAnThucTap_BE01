using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos; // Đảm bảo đã import Dtos namespace
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
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

        public async Task<PagedResponse<TeacherWorkStatusHistoryDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Teacherworkstatushistories
                .Include(h => h.Teacher)
                .AsQueryable();

            // 1. LOGIC TÌM KIẾM
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerCaseQuery = searchQuery.ToLower();
                query = query.Where(h =>
                    (h.Teacher != null && h.Teacher.Fullname.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Statustype.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Note != null && h.Note.ToLower().Contains(lowerCaseQuery))
                );
            }

            // 2. LẤY TỔNG SỐ BẢN GHI
            var totalRecords = await query.CountAsync();

            // 3. LOGIC PHÂN TRANG VÀ LẤY DỮ LIỆU
            var pagedData = await query
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

            // 4. TRẢ VỀ KẾT QUẢ ĐÃ ĐÓNG GÓI
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

        // Cập nhật phương thức CreateAsync để nhận DTO
        public async Task<TeacherWorkStatusHistoryDto> CreateAsync(TeacherWorkStatusHistoryRequestDto historyDto)
        {
            // Sử dụng transaction để đảm bảo cả hai thao tác cùng thành công hoặc thất bại
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Tìm giáo viên
                var teacher = await _context.Teachers.FindAsync(historyDto.Teacherid);
                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy giáo viên với ID = {historyDto.Teacherid}.");
                }

                // 2. Cập nhật trạng thái và thời gian cho giáo viên
                teacher.Status = historyDto.Statustype;
                teacher.Updatedat = DateTime.UtcNow;

                // 3. Ánh xạ từ DTO sang Model Entity
                var history = new Teacherworkstatushistory
                {
                    Teacherid = historyDto.Teacherid,
                    Statustype = historyDto.Statustype,
                    Startdate = historyDto.Startdate,
                    Enddate = historyDto.Enddate,
                    Note = historyDto.Note,
                    Decisionfileurl = historyDto.Decisionfileurl,
                    Createdat = DateTime.UtcNow // Đặt thời gian tạo
                };

                // 4. Thêm bản ghi lịch sử mới
                _context.Teacherworkstatushistories.Add(history);

                // 5. Lưu tất cả thay đổi (cả INSERT và UPDATE)
                await _context.SaveChangesAsync();

                // 6. Hoàn tất giao dịch
                await transaction.CommitAsync();

                // Trả về DTO với đầy đủ thông tin
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
                // Nếu có lỗi, hủy bỏ tất cả thay đổi
                await transaction.RollbackAsync();
                throw; // Ném lại lỗi để Controller xử lý
            }
        }

        // Cập nhật phương thức UpdateAsync để nhận DTO
        public async Task<TeacherWorkStatusHistoryDto?> UpdateAsync(int id, TeacherWorkStatusHistoryRequestDto updatedHistoryDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existing = await _context.Teacherworkstatushistories.FindAsync(id);
                if (existing == null) return null;

                // 1. Cập nhật các thuộc tính từ DTO vào entity hiện có
                existing.Teacherid = updatedHistoryDto.Teacherid;
                existing.Statustype = updatedHistoryDto.Statustype;
                existing.Startdate = updatedHistoryDto.Startdate;
                existing.Enddate = updatedHistoryDto.Enddate;
                existing.Note = updatedHistoryDto.Note;
                existing.Decisionfileurl = updatedHistoryDto.Decisionfileurl;

                // 2. Cập nhật trạng thái của giáo viên liên quan (nếu cần)
                var teacher = await _context.Teachers.FindAsync(updatedHistoryDto.Teacherid);
                if (teacher == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy giáo viên với ID = {updatedHistoryDto.Teacherid}.");
                }
                teacher.Status = updatedHistoryDto.Statustype;
                teacher.Updatedat = DateTime.UtcNow;
                _context.Teachers.Update(teacher); // Ra lệnh tường minh cho EF Core

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Trả về DTO sau khi cập nhật
                return new TeacherWorkStatusHistoryDto
                {
                    Historyid = existing.Historyid,
                    TeacherName = teacher.Fullname,
                    Statustype = existing.Statustype,
                    Startdate = existing.Startdate,
                    Enddate = existing.Enddate,
                    Note = existing.Note,
                    Decisionfileurl = existing.Decisionfileurl,
                    Createdat = existing.Createdat // Giữ nguyên thời gian tạo ban đầu
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