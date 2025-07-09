
using DuAnThucTapNhom3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DashboardsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardAsync(int schoolyearId, int gradelevelId)
        {
            var totalStudents = await _context.SchoolYearlyStatuses
             .Include(s => s.Student) // nếu cần dùng dữ liệu từ Student
             .Where(s => s.SchoolYearId == schoolyearId && s.Student != null)
             .Select(s => s.StudentId)
             .Distinct()
             .CountAsync();
            //test
            var test = await _context.Schoolyears
                .Include(x => x.SchoolYearlyStatuses)
                .Select(x => x.Schoolyearid)
                .CountAsync();
            //test
            int countSchoolyear = await _context.Schoolyears.Distinct().CountAsync();
            int usedSchoolyears = await _context.SchoolYearlyStatuses
                 .Select(x => x.SchoolYear.Schoolyearid)
                 .Distinct()
                 .CountAsync();


            // 2. Tổng số giáo viên có dạy trong năm học
            var totalTeachers = await _context.SchoolYearlyStatuses
    .Include(s => s.Teacher)
    .Where(s => s.SchoolYearId == schoolyearId &&
                s.Teacher != null)
    .Select(s => s.TeacherId)
    .Distinct()
    .CountAsync();


            // 3. Tổng số học sinh theo khối
            var totalStudentsOfGradelevel = await _context.SchoolYearlyStatuses
                 .Where(s => s.SchoolYearId == schoolyearId &&
                             s.GradelevelId == gradelevelId &&
                             s.Status == "Active")
                 .CountAsync();

            var fromDate = DateTime.UtcNow.AddDays(-6).Date;
            var toDate = DateTime.UtcNow.Date.AddDays(1); // tới hết hôm nay

            var dailyLoginStats = await _context.LoginLogs
                .Where(log => log.LoginTime >= fromDate && log.LoginTime < toDate)
                .GroupBy(log => log.LoginTime.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"), // 👉 chuyển về string TẠI ĐÂY
                    Count = g.Count()
                })
                .ToListAsync();


            // 5. Thống kê học lực từng lớp trong khối
            var classStats = await _context.Classes
                .Include(x => x.SchoolYearlyStatuses)
                .Where(c => c.Gradelevelid == gradelevelId && c.Schoolyearid == schoolyearId)
                .Select(c => new
                {
                    ClassName = c.Classname,
                    Excellent = _context.StudentSemesterSummaries
                        .Count(s => s.StudentsInSemeterSumamryModel.Any(st => st.Class.Classid == c.Classid) &&
                                    s.AverageScore >= 8 &&
                                    s.Semester.Schoolyearid == schoolyearId),
                    Good = _context.StudentSemesterSummaries
                        .Count(s => s.StudentsInSemeterSumamryModel.Any(st => st.Class.Classid == c.Classid) &&
                                    s.AverageScore >= 6.5 && s.AverageScore < 8 &&
                                    s.Semester.Schoolyearid == schoolyearId),
                    Average = _context.StudentSemesterSummaries
                        .Count(s => s.StudentsInSemeterSumamryModel.Any(st => st.Class.Classid == c.Classid) &&
                                    s.AverageScore >= 5 && s.AverageScore < 6.5 &&
                                    s.Semester.Schoolyearid == schoolyearId),
                    Weak = _context.StudentSemesterSummaries
                        .Count(s => s.StudentsInSemeterSumamryModel.Any(st => st.Class.Classid == c.Classid) &&
                                    s.AverageScore < 5 &&
                                    s.Semester.Schoolyearid == schoolyearId)
                })
                .ToListAsync();

            // 6. Tổng hợp kết quả
            var dashboardData = new
            {
                TotalStudents = totalStudents,
                TotalTeachers = totalTeachers,
                TotalStudentsOfGradelevel = totalStudentsOfGradelevel,
                TotalLoginCount = dailyLoginStats,
                ClassScoreStatistics = classStats,
                Test = test,
                UsedSchoolyears = usedSchoolyears,
                CountSchoolyear = countSchoolyear
            };

            return Ok(dashboardData);
        }
    }
}