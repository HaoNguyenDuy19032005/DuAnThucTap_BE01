using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using DuAnThucTap_BE01.Interface;

namespace DuAnThucTap_BE01.Services
{
    public class ExamService : IExamService
    {
        private readonly AppDbContext _context;

        public ExamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            return await _context.Exams.ToListAsync();
        }

        public async Task<Exam?> GetByIdAsync(int id)
        {
            return await _context.Exams.FindAsync(id);
        }

        public async Task<Exam> CreateAsync(Exam newExam)
        {
            newExam.CreatedAt = DateTime.UtcNow;

            _context.Exams.Add(newExam);
            await _context.SaveChangesAsync();
            return newExam;
        }

        public async Task<Exam?> UpdateAsync(int id, Exam updatedExam)
        {
            var existing = await _context.Exams.FindAsync(id);
            if (existing == null) return null;

            existing.SchoolYearID = updatedExam.SchoolYearID;
            existing.GradeLevelID = updatedExam.GradeLevelID;
            existing.SemesterID = updatedExam.SemesterID;
            existing.SubjectID = updatedExam.SubjectID;
            existing.ExamName = updatedExam.ExamName;
            existing.ExamDate = updatedExam.ExamDate;
            existing.DurationMinutes = updatedExam.DurationMinutes;
            existing.CreatedAt = updatedExam.CreatedAt;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Exams.FindAsync(id);
            if (entity == null) return false;

            _context.Exams.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}