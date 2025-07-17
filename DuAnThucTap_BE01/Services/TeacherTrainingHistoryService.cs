using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherTrainingHistoryService : ITeacherTrainingHistoryService
    {
        private readonly ISCDbContext _context;
        private readonly IFirebaseStorageService _firebaseStorageService; 

        public TeacherTrainingHistoryService(ISCDbContext context, IFirebaseStorageService firebaseStorageService)
        {
            _context = context;
            _firebaseStorageService = firebaseStorageService;
        }

        public async Task<PagedResponse<TeacherTrainingHistoryDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Teachertraininghistories
                                .Include(h => h.Teacher) 
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var lowerCaseQuery = searchQuery.ToLower();
                query = query.Where(h =>
                    (h.Teacher != null && h.Teacher.Fullname.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Traininginstitutionname != null && h.Traininginstitutionname.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Majororspecialization != null && h.Majororspecialization.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Trainingtype != null && h.Trainingtype.ToLower().Contains(lowerCaseQuery)) ||
                    (h.Certificatediplomaname != null && h.Certificatediplomaname.ToLower().Contains(lowerCaseQuery))
                );
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

            return new PagedResponse<TeacherTrainingHistoryDto>(pagedData, pageNumber, pageSize, totalRecords);
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

        public async Task<Teachertraininghistory> CreateAsync(TeacherTrainingHistoryRequestDto historyDto, IFormFile? file)
        {
            string? attachmentUrl = null;
            if (file != null && file.Length > 0)
            {
                attachmentUrl = await _firebaseStorageService.UploadFileAsync(file, "teacher-training-attachments");
            }

            // Chuyển đổi string sang DateOnly?
            DateOnly.TryParse(historyDto.Startdate, out DateOnly startDate);

            var history = new Teachertraininghistory
            {
                Teacherid = historyDto.Teacherid,
                Traininginstitutionname = historyDto.Traininginstitutionname,
                Majororspecialization = historyDto.Majororspecialization,
                // Gán giá trị đã chuyển đổi
                Startdate = historyDto.Startdate == null ? null : startDate,
                Enddateorgraduationyear = historyDto.Enddateorgraduationyear,
                Active = historyDto.Active,
                Trainingtype = historyDto.Trainingtype,
                Certificatediplomaname = historyDto.Certificatediplomaname,
                Attachmenturl = attachmentUrl
            };

            _context.Teachertraininghistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<Teachertraininghistory?> UpdateAsync(int id, TeacherTrainingHistoryRequestDto updatedHistoryDto, IFormFile? file)
        {
            var existing = await _context.Teachertraininghistories.FindAsync(id);
            if (existing == null) return null;

            string? newAttachmentUrl = existing.Attachmenturl;
            if (file != null && file.Length > 0)
            {
                newAttachmentUrl = await _firebaseStorageService.UploadFileAsync(file, "teacher-training-attachments");
            }
            else if (!string.IsNullOrEmpty(updatedHistoryDto.Attachmenturl))
            {
                newAttachmentUrl = updatedHistoryDto.Attachmenturl;
            }

            DateOnly.TryParse(updatedHistoryDto.Startdate, out DateOnly startDate);

            existing.Teacherid = updatedHistoryDto.Teacherid;
            existing.Traininginstitutionname = updatedHistoryDto.Traininginstitutionname;
            existing.Majororspecialization = updatedHistoryDto.Majororspecialization;
            existing.Startdate = updatedHistoryDto.Startdate == null ? null : startDate;
            existing.Enddateorgraduationyear = updatedHistoryDto.Enddateorgraduationyear;
            existing.Active = updatedHistoryDto.Active;
            existing.Trainingtype = updatedHistoryDto.Trainingtype;
            existing.Certificatediplomaname = updatedHistoryDto.Certificatediplomaname;
            existing.Attachmenturl = newAttachmentUrl;

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