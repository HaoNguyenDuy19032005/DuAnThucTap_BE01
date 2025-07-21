//using Nhom2ThucTap.Data;
//using Nhom2ThucTap.Interface;
//using Nhom2ThucTap.Models;
//using Microsoft.EntityFrameworkCore;

//namespace Nhom2ThucTap.Service
//{
//    public class TestService : ITestService
//    {
//        private readonly AppDbContext _context;
//        public TestService(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Test>> GetAllAsync(string? keyword, int page, int pageSize)
//        {
//            var query = _context.Tests.Include(t => t.Teacher).AsQueryable();
//            if (!string.IsNullOrWhiteSpace(keyword))
//            {
//                query = query.Where(t => t.Title.Contains(keyword));
//            }
//            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
//        }

//        public async Task<Test?> GetByIdAsync(int id) => await _context.Tests.Include(t => t.Teacher).FirstOrDefaultAsync(t => t.Testid == id);

//        public async Task<Test> CreateAsync(Test test)
//        {
//            _context.Tests.Add(test);
//            await _context.SaveChangesAsync();
//            return test;
//        }

//        public async Task<bool> UpdateAsync(int id, Test updated)
//        {
//            var existing = await _context.Tests.FindAsync(id);
//            if (existing == null) return false;

//            _context.Entry(existing).CurrentValues.SetValues(updated);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            var test = await _context.Tests.FindAsync(id);
//            if (test == null) return false;

//            _context.Tests.Remove(test);
//            await _context.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> TeacherExistsAsync(int teacherId) => await _context.Teachers.AnyAsync(t => t.Teacherid == teacherId);

//        public async Task<IEnumerable<Test>> GetUpcomingTestsAsync(int page, int pageSize)
//        {
//            return await _context.Tests
//                .Where(t => t.Starttime > DateTime.UtcNow)
//                .OrderBy(t => t.Starttime)
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();
//        }

//        public async Task<IEnumerable<Test>> GetFinishedTestsAsync(int page, int pageSize)
//        {
//            return await _context.Tests
//                .Where(t => t.Endtime < DateTime.UtcNow)
//                .OrderByDescending(t => t.Endtime)
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();
//        }
//    }
//}
