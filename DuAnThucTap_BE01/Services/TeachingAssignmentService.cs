using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TeachingAssignmentService : ITeachingAssignmentService
    {
        private readonly ISCDbContext _context;

        public TeachingAssignmentService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teachingassignment>> GetAllAsync()
        {
            return await _context.Teachingassignments.ToListAsync();
        }

        public async Task<Teachingassignment?> GetByIdAsync(int id)
        {
            return await _context.Teachingassignments.FindAsync(id);
        }

        public async Task<Teachingassignment> CreateAsync(Teachingassignment teachingAssignment)
        {
            // Kiểm tra khóa ngoại
            if (!await _context.Teachers.AnyAsync(t => t.Teacherid == teachingAssignment.Teacherid))
                throw new ArgumentException("Teacherid không tồn tại.");
            if (!await _context.Subjects.AnyAsync(s => s.Subjectid == teachingAssignment.Subjectid))
                throw new ArgumentException("Subjectid không tồn tại.");
            if (!await _context.Schoolyears.AnyAsync(sy => sy.Schoolyearid == teachingAssignment.Schoolyearid))
                throw new ArgumentException("Schoolyearid không tồn tại.");
            if (teachingAssignment.Classtypeid.HasValue && !await _context.Classtypes.AnyAsync(ct => ct.Classtypeid == teachingAssignment.Classtypeid))
                throw new ArgumentException("Classtypeid không tồn tại.");
            if (teachingAssignment.Topicid.HasValue && !await _context.Topiclists.AnyAsync(t => t.Topicid == teachingAssignment.Topicid))
                throw new ArgumentException("Topicid không tồn tại.");

            _context.Teachingassignments.Add(teachingAssignment);
            await _context.SaveChangesAsync();
            return teachingAssignment;
        }

        public async Task<Teachingassignment?> UpdateAsync(int id, Teachingassignment updatedAssignment)
        {
            var existing = await _context.Teachingassignments.FindAsync(id);
            if (existing == null) return null;

            // Kiểm tra khóa ngoại
            if (!await _context.Teachers.AnyAsync(t => t.Teacherid == updatedAssignment.Teacherid))
                throw new ArgumentException("Teacherid không tồn tại.");
            if (!await _context.Subjects.AnyAsync(s => s.Subjectid == updatedAssignment.Subjectid))
                throw new ArgumentException("Subjectid không tồn tại.");
            if (!await _context.Schoolyears.AnyAsync(sy => sy.Schoolyearid == updatedAssignment.Schoolyearid))
                throw new ArgumentException("Schoolyearid không tồn tại.");
            if (updatedAssignment.Classtypeid.HasValue && !await _context.Classtypes.AnyAsync(ct => ct.Classtypeid == updatedAssignment.Classtypeid))
                throw new ArgumentException("Classtypeid không tồn tại.");
            if (updatedAssignment.Topicid.HasValue && !await _context.Topiclists.AnyAsync(t => t.Topicid == updatedAssignment.Topicid))
                throw new ArgumentException("Topicid không tồn tại.");

            existing.Teacherid = updatedAssignment.Teacherid;
            existing.Subjectid = updatedAssignment.Subjectid;
            existing.Classtypeid = updatedAssignment.Classtypeid;
            existing.Topicid = updatedAssignment.Topicid;
            existing.Schoolyearid = updatedAssignment.Schoolyearid;
            existing.Teachingstartdate = updatedAssignment.Teachingstartdate;
            existing.Teachingenddate = updatedAssignment.Teachingenddate;
            existing.Notes = updatedAssignment.Notes;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var assignment = await _context.Teachingassignments.FindAsync(id);
            if (assignment == null) return false;

            _context.Teachingassignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}