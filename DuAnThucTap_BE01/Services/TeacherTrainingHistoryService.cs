using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
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

        public async Task<Teachertraininghistory> CreateAsync(TeacherTrainingHistoryRequestDto historyDto, IFormFile? file)
        {
            string? attachmentUrl = null;
            if (file != null && file.Length > 0)
            {
                attachmentUrl = await _firebaseStorageService.UploadFileAsync(file, "teacher-training-attachments");
            }

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

            string? newAttachmentUrl = null;
            if (file != null && file.Length > 0)
            {
                newAttachmentUrl = await _firebaseStorageService.UploadFileAsync(file, "teacher-training-attachments");
            }
            else 
            {
                newAttachmentUrl = updatedHistoryDto.Attachmenturl; 
            }


            existing.Teacherid = updatedHistoryDto.Teacherid;
            existing.Traininginstitutionname = updatedHistoryDto.Traininginstitutionname;
            existing.Majororspecialization = updatedHistoryDto.Majororspecialization;
            existing.Startdate = updatedHistoryDto.Startdate;
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