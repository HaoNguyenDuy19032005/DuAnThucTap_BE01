// Services/TeacherTrainingHistoryService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherTrainingHistoryService : ITeacherTrainingHistoryService
    {
        private readonly ISCDbContext _context;

        public TeacherTrainingHistoryService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherTrainingHistoryDto>> GetAllAsync()
        {
            // Bỏ .Include() và kiểm tra null trong .Select() để tạo LEFT JOIN an toàn
            return await _context.Teachertraininghistories
                .Select(h => new TeacherTrainingHistoryDto
                {
                    Trainingid = h.Trainingid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null, // SỬA Ở ĐÂY
                    Traininginstitutionname = h.Traininginstitutionname,
                    Majororspecialization = h.Majororspecialization,
                    Startdate = h.Startdate,
                    Enddateorgraduationyear = h.Enddateorgraduationyear,
                    Active = h.Active,
                    Trainingtype = h.Trainingtype,
                    Certificatediplomaname = h.Certificatediplomaname,
                    Attachmenturl = h.Attachmenturl
                }).ToListAsync();
        }

        public async Task<TeacherTrainingHistoryDto?> GetByIdAsync(int id)
        {
            // Tương tự, bỏ .Include() và kiểm tra null
            return await _context.Teachertraininghistories
                .Where(h => h.Trainingid == id)
                .Select(h => new TeacherTrainingHistoryDto
                {
                    Trainingid = h.Trainingid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null, // SỬA Ở ĐÂY
                    Traininginstitutionname = h.Traininginstitutionname,
                    Majororspecialization = h.Majororspecialization,
                    Startdate = h.Startdate,
                    Enddateorgraduationyear = h.Enddateorgraduationyear,
                    Active = h.Active,
                    Trainingtype = h.Trainingtype,
                    Certificatediplomaname = h.Certificatediplomaname,
                    Attachmenturl = h.Attachmenturl
                }).FirstOrDefaultAsync();
        }

        public async Task<Teachertraininghistory> CreateAsync(Teachertraininghistory history)
        {
            _context.Teachertraininghistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<Teachertraininghistory?> UpdateAsync(int id, Teachertraininghistory updatedHistory)
        {
            var existing = await _context.Teachertraininghistories.FindAsync(id);
            if (existing == null) return null;

            // TỐI ƯU: Dùng SetValues để cập nhật ngắn gọn và dễ bảo trì
            _context.Entry(existing).CurrentValues.SetValues(updatedHistory);

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var history = await _context.Teachertraininghistories.FindAsync(id);
            if (history == null) return false;

            _context.Teachertraininghistories.Remove(history);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}