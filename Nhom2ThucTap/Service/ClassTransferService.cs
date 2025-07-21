using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class ClassTransferService : IClassTransferService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClassTransferService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Classtransferhistory>> GetAllAsync()
        {
            return await _context.Classtransferhistories
                .Include(t => t.Student)
                .Include(t => t.Fromclass)
                .Include(t => t.Toclass)
                .Include(t => t.Semester)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Classtransferhistory?> GetByIdAsync(int id)
        {
            return await _context.Classtransferhistories
                .Include(t => t.Student)
                .Include(t => t.Fromclass)
                .Include(t => t.Toclass)
                .Include(t => t.Semester)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Transferid == id);
        }

        // ✅ Sử dụng DTO: xử lý upload file và lưu thông tin
        public async Task<bool> TransferAndUpdateStatusAsync(ClasstransferhistoryCreateDto dto)
        {
            var transfer = new Classtransferhistory
            {
                Studentid = dto.Studentid.Value,
                Fromclassid = dto.Fromclassid.Value,
                Toclassid = dto.Toclassid.Value,
                Semesterid = dto.Semesterid.Value,
                Reason = dto.Reason,
                Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            };


            // ✅ Upload file nếu có
            if (dto.Attachmentfile != null && dto.Attachmentfile.Length > 0)
            {
                var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "classtransfers");
                Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Attachmentfile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Attachmentfile.CopyToAsync(stream);
                }

                transfer.Attachmenturl = Path.Combine("uploads", "classtransfers", fileName).Replace("\\", "/");
            }

            return await TransferAndUpdateStatusAsync(transfer);
        }

        // ✅ Hàm gốc (không cần sửa lại nhiều)
        public async Task<bool> TransferAndUpdateStatusAsync(Classtransferhistory transfer)
        {
            if (transfer == null)
                throw new InvalidOperationException("Dữ liệu chuyển lớp không được để trống.");

            if (transfer.Fromclassid == transfer.Toclassid)
                throw new InvalidOperationException("Lớp chuyển đến không được trùng với lớp hiện tại.");

            var student = await _context.Students.FindAsync(transfer.Studentid);
            if (student == null)
                throw new InvalidOperationException($"Học sinh với ID {transfer.Studentid} không tồn tại.");

            var fromClass = await _context.Classes.FindAsync(transfer.Fromclassid);
            if (fromClass == null)
                throw new InvalidOperationException($"Lớp chuyển đi với ID {transfer.Fromclassid} không tồn tại.");

            var toClass = await _context.Classes.FindAsync(transfer.Toclassid);
            if (toClass == null)
                throw new InvalidOperationException($"Lớp chuyển đến với ID {transfer.Toclassid} không tồn tại.");

            var semester = await _context.Semesters.FindAsync(transfer.Semesterid);
            if (semester == null)
                throw new InvalidOperationException($"Học kỳ với ID {transfer.Semesterid} không tồn tại.");

            var studentStatus = await _context.Studentyearlystatuses
                .Where(s => s.Studentid == transfer.Studentid)
                .OrderByDescending(s =>
                    s.Updatedat ?? (s.Enrollmentdate.HasValue
                        ? s.Enrollmentdate.Value.ToDateTime(TimeOnly.MinValue)
                        : DateTime.MinValue))
                .FirstOrDefaultAsync();

            if (studentStatus == null)
                throw new InvalidOperationException("Không tìm thấy trạng thái học sinh.");

            if (studentStatus.Classid != transfer.Fromclassid)
                throw new InvalidOperationException("Lớp hiện tại của học sinh không khớp với lớp chuyển đi.");

            _context.Classtransferhistories.Add(transfer);

            studentStatus.Classid = transfer.Toclassid;
            studentStatus.Updatedat = transfer.Createdat;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTransferAsync(int id, ClasstransferhistoryCreateDto dto)
        {
            if (dto == null)
                throw new InvalidOperationException("Dữ liệu cập nhật không được để trống.");

            var existing = await _context.Classtransferhistories.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Không tìm thấy bản ghi chuyển lớp để cập nhật.");

            if (dto.Fromclassid == dto.Toclassid)
                throw new InvalidOperationException("Lớp chuyển đến không được trùng với lớp chuyển đi.");

            // Giữ lại file cũ nếu không có file mới được upload
            string attachmentUrl = existing.Attachmenturl;

            // Xử lý upload file nếu có file mới
            if (dto.Attachmentfile != null && dto.Attachmentfile.Length > 0)
            {
                // Xóa file cũ nếu có
                if (!string.IsNullOrEmpty(attachmentUrl))
                {
                    var oldFilePath = Path.Combine(_env.WebRootPath ?? "wwwroot", attachmentUrl.Replace("/", "\\"));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Upload file mới
                var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "classtransfers");
                Directory.CreateDirectory(folderPath);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Attachmentfile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Attachmentfile.CopyToAsync(stream);
                }

                attachmentUrl = Path.Combine("uploads", "classtransfers", fileName).Replace("\\", "/");
            }

            // Cập nhật thông tin
            existing.Studentid = dto.Studentid.Value;
            existing.Fromclassid = dto.Fromclassid.Value;
            existing.Toclassid = dto.Toclassid.Value;
            existing.Semesterid = dto.Semesterid.Value;
            existing.Reason = dto.Reason;
            existing.Attachmenturl = attachmentUrl;
            existing.Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            _context.Classtransferhistories.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteTransferAsync(int id)
        {
            var transfer = await _context.Classtransferhistories.FindAsync(id);
            if (transfer == null)
                throw new KeyNotFoundException("Không tìm thấy bản ghi để xóa.");

            _context.Classtransferhistories.Remove(transfer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(List<Classtransferhistory> Transfers, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            if (page <= 0 || pageSize <= 0)
                throw new ArgumentException("Số trang và kích thước trang phải lớn hơn 0.");

            var query = _context.Classtransferhistories
                .Include(t => t.Student)
                .Include(t => t.Fromclass)
                .Include(t => t.Toclass)
                .Include(t => t.Semester)
                .AsNoTracking();

            var totalCount = await query.CountAsync();
            var transfers = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (transfers, totalCount);
        }
        public async Task<bool> ExistsStudentAsync(int studentId)
    => await _context.Students.AnyAsync(s => s.Studentid == studentId);

        public async Task<bool> ExistsClassAsync(int classId)
            => await _context.Classes.AnyAsync(c => c.Classid == classId);

    }
}
