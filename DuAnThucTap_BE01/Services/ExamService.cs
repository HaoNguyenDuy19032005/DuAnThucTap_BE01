using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class ExamService : IExamService
    {
        private readonly ISCDbContext _context;

        public ExamService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<Exam> CreateAsync(Exam exam)
        {
            exam.Createdat = DateTime.UtcNow; // Thêm thời gian tạo nếu cần
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null) return false;

            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            return await _context.Exams
                .Include(e => e.Classtype)
                .Include(e => e.Gradelevel)
                .Include(e => e.Graderassignmenttype)
                .Include(e => e.Schoolyear)
                .Include(e => e.Semester)
                .Include(e => e.Subject)
                .Include(e => e.Examschedules)
                .ToListAsync();
        }

        public async Task<Exam?> GetByIdAsync(int id)
        {
            return await _context.Exams
                .Include(e => e.Classtype)
                .Include(e => e.Gradelevel)
                .Include(e => e.Graderassignmenttype)
                .Include(e => e.Schoolyear)
                .Include(e => e.Semester)
                .Include(e => e.Subject)
                .Include(e => e.Examschedules)
                .FirstOrDefaultAsync(e => e.Examid == id);
        }

        public async Task<Exam?> UpdateAsync(int id, Exam updatedExam)
        {
            var existingExam = await _context.Exams.FindAsync(id);
            if (existingExam == null) return null;

            existingExam.Schoolyearid = updatedExam.Schoolyearid;
            existingExam.Gradelevelid = updatedExam.Gradelevelid;
            existingExam.Semesterid = updatedExam.Semesterid;
            existingExam.Subjectid = updatedExam.Subjectid;
            existingExam.Examname = updatedExam.Examname;
            existingExam.Examdate = updatedExam.Examdate;
            existingExam.Durationminutes = updatedExam.Durationminutes;
            existingExam.Classtypeid = updatedExam.Classtypeid;
            existingExam.Graderassignmenttypeid = updatedExam.Graderassignmenttypeid;
            

            await _context.SaveChangesAsync();
            return existingExam;
        }
    }
}