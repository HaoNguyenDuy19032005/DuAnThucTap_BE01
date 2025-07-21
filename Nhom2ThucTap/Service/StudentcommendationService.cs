using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public class StudentcommendationService : IStudentcommendationService
    {
        private readonly AppDbContext _context;

        public StudentcommendationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Studentcommendation>> GetAllAsync()
        {
            return await _context.Studentcommendations
                .Include(s => s.Commendationtype)
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(List<Studentcommendation> Commendations, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            var query = _context.Studentcommendations
                .Include(s => s.Commendationtype)
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var commendations = await query
                .OrderByDescending(s => s.Commendationdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (commendations, totalCount);
        }

        public async Task<Studentcommendation?> GetByIdAsync(int id)
        {
            return await _context.Studentcommendations
                .Include(s => s.Commendationtype)
                .Include(s => s.Student)
                .Include(s => s.Semester)
                .Include(s => s.Schoolinfo)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Commendationid == id);
        }

        public async Task<Studentcommendation> AddAsync(Studentcommendation commendation)
        {
            commendation.Createdat = DateTime.Now;
            _context.Studentcommendations.Add(commendation);
            await _context.SaveChangesAsync();
            return commendation;
        }

        public async Task<Studentcommendation?> UpdateAsync(int id, Studentcommendation commendation)
        {
            var existing = await _context.Studentcommendations.FindAsync(id);
            if (existing == null) return null;

            existing.Studentid = commendation.Studentid;
            existing.Semesterid = commendation.Semesterid;
            existing.Schoolinfoid = commendation.Schoolinfoid;
            existing.Commendationtypeid = commendation.Commendationtypeid;
            existing.Commendationdate = commendation.Commendationdate;
            existing.Content = commendation.Content;
            existing.Attachmenturl = commendation.Attachmenturl;
            existing.Createdat = commendation.Createdat;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Studentcommendations.FindAsync(id);
            if (entity == null) return false;

            _context.Studentcommendations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
