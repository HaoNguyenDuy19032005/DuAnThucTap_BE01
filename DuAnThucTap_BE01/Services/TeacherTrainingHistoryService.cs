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
            return await _context.Teachertraininghistories
                .Select(h => new TeacherTrainingHistoryDto
                {
                    Trainingid = h.Trainingid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null,
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
            return await _context.Teachertraininghistories
                .Where(h => h.Trainingid == id)
                .Select(h => new TeacherTrainingHistoryDto
                {
                    Trainingid = h.Trainingid,
                    Teacherid = h.Teacherid,
                    TeacherName = h.Teacher != null ? h.Teacher.Fullname : null,
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

        public async Task<Teachertraininghistory> CreateAsync(TeacherTrainingHistoryRequestDto historyDto)
        {
            var history = new Teachertraininghistory
            {
                Teacherid = historyDto.Teacherid,
                Traininginstitutionname = historyDto.Traininginstitutionname,
                Majororspecialization = historyDto.Majororspecialization,
                Startdate = historyDto.Startdate,
                Enddateorgraduationyear = historyDto.Enddateorgraduationyear,
                Active = historyDto.Active,
                Trainingtype = historyDto.Trainingtype,
                Certificatediplomaname = historyDto.Certificatediplomaname,
                Attachmenturl = historyDto.Attachmenturl
            };

            _context.Teachertraininghistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<Teachertraininghistory?> UpdateAsync(int id, TeacherTrainingHistoryRequestDto updatedHistoryDto)
        {
            var existing = await _context.Teachertraininghistories.FindAsync(id);
            if (existing == null) return null;

            // Ánh xạ các thuộc tính từ DTO sang entity hiện có
            existing.Teacherid = updatedHistoryDto.Teacherid;
            existing.Traininginstitutionname = updatedHistoryDto.Traininginstitutionname;
            existing.Majororspecialization = updatedHistoryDto.Majororspecialization;
            existing.Startdate = updatedHistoryDto.Startdate;
            existing.Enddateorgraduationyear = updatedHistoryDto.Enddateorgraduationyear;
            existing.Active = updatedHistoryDto.Active;
            existing.Trainingtype = updatedHistoryDto.Trainingtype;
            existing.Certificatediplomaname = updatedHistoryDto.Certificatediplomaname;
            existing.Attachmenturl = updatedHistoryDto.Attachmenturl;

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