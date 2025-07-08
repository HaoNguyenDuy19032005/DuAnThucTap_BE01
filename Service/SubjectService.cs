using DuAnThucTapNhom3.Data;
using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom3.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly AppDbContext _context;
        public SubjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Subject> CreateAsync(Subject subject)
        {
            subject.Createdat = DateTime.UtcNow;
            subject.Updatedat = DateTime.UtcNow;
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject?> UpdateAsync(int id, Subject subject)
        {
            var existing = await _context.Subjects.FindAsync(id);
            if (existing == null) return null;

            existing.Subjectname = subject.Subjectname;
            existing.Subjectcode = subject.Subjectcode;
            existing.Defaultperiodssem1 = subject.Defaultperiodssem1;
            existing.Defaultperiodssem1 = subject.Defaultperiodssem2;
            existing.Departmentid = subject.Departmentid;
            existing.Subjecttypeid = subject.Subjecttypeid;
    
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null) return false;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
