using DuAnThucTap.Data;
using DuAnThucTap.Irepository;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Service
{
    public class DepartmentleaderService : IDepartmentleadersService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentleaderService(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<List<string>> ValidateForeignKeys(DepartmentleaderCreateDto dto)
        {
            var errors = new List<string>();

            if (!await _context.Departments.AnyAsync(d => d.Departmentid == dto.Departmentid))
                errors.Add($"Departmentid {dto.Departmentid} không tồn tại.");

            if (!await _context.Schoolyears.AnyAsync(s => s.Schoolyearid == dto.Schoolyearid))
                errors.Add($"Schoolyearid {dto.Schoolyearid} không tồn tại.");

            if (!await _context.Teachers.AnyAsync(t => t.Teacherid == dto.Teacherid))
                errors.Add($"Teacherid {dto.Teacherid} không tồn tại.");

            return errors;
        }

        public async Task<IEnumerable<Departmentleader>> GetAllAsync(PaginationDto pagination)
        {
            return await _context.Departmentleaders
                .Include(d => d.Department)
                .Include(d => d.Schoolyear)
                .Include(d => d.Teacher)
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }

        public async Task<Departmentleader?> GetByIdAsync(int id)
        {
            return await _context.Departmentleaders
                .Include(d => d.Department)
                .Include(d => d.Schoolyear)
                .Include(d => d.Teacher)
                .FirstOrDefaultAsync(d => d.Departmentleaderid == id);
        }

        public async Task<Departmentleader> CreateAsync(DepartmentleaderCreateDto dto)
        {
            var errors = await ValidateForeignKeys(dto);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            bool teacherExists = await _context.TeacherConcurrentSubjects
                .AnyAsync(tcs => tcs.TeacherID == dto.Teacherid);

            if (!teacherExists)
                throw new ArgumentException($"Giáo viên với ID: {dto.Teacherid} không tồn tại trong danh sách giảng dạy.");

            bool teachesInYear = await _context.TeacherConcurrentSubjects
                .AnyAsync(tcs => tcs.TeacherID == dto.Teacherid &&
                                 tcs.SchoolYearID == dto.Schoolyearid);

            if (!teachesInYear)
                throw new ArgumentException($"Giáo viên không dạy trong năm học SchoolyearID: {dto.Schoolyearid}.");

            bool teachesSubjectInDepartment = await _context.TeacherConcurrentSubjects
                .AnyAsync(tcs => tcs.TeacherID == dto.Teacherid &&
                                 tcs.SchoolYearID == dto.Schoolyearid &&
                                 tcs.Subject.Departmentid == dto.Departmentid);

            if (!teachesSubjectInDepartment)
                throw new ArgumentException($"Giáo viên không dạy môn học nào thuộc bộ môn {dto.Departmentid} trong năm học này.");

            var existingLeader = await _context.Departmentleaders
                .FirstOrDefaultAsync(dl =>
                    dl.Departmentid == dto.Departmentid &&
                    dl.Schoolyearid == dto.Schoolyearid &&
                    dl.Teacherid == dto.Teacherid &&
                    dl.Enddate == null);

            if (existingLeader != null)
                throw new ArgumentException("Giáo viên đã là trưởng bộ môn trong năm học này, không thể tạo mới.");

            var entity = new Departmentleader
            {
                Departmentid = dto.Departmentid,
                Schoolyearid = dto.Schoolyearid,
                Teacherid = dto.Teacherid,
                Startdate = DateTime.UtcNow
            };

            _context.Departmentleaders.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.Departmentleaderid) ?? entity;
        }

        public async Task<bool> UpdateAsync(int id, DepartmentleaderCreateDto dto)
        {
            var entity = await _context.Departmentleaders.FindAsync(id);
            if (entity == null) return false;

            var errors = await ValidateForeignKeys(dto);
            if (errors.Any())
                throw new ArgumentException(string.Join(" | ", errors));

            entity.Departmentid = dto.Departmentid;
            entity.Schoolyearid = dto.Schoolyearid;
            entity.Teacherid = dto.Teacherid;
            entity.Enddate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Departmentleaders.FindAsync(id);
            if (entity == null) return false;

            _context.Departmentleaders.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Departmentleader>> SearchByDepartmentNameAsync(string keyword, PaginationDto pagination)
        {
            return await _context.Departmentleaders
                .Include(dl => dl.Department)
                .Include(dl => dl.Teacher)
                .Where(dl => dl.Department != null &&
                             EF.Functions.Like(dl.Department.Departmentname, $"%{keyword}%"))
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }

    }
}
