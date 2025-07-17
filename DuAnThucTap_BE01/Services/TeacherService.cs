// Services/TeacherService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ISCDbContext _context;

        public TeacherService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            // Dùng .Select để tạo DTO trực tiếp từ câu lệnh SQL.
            // Cách này rất hiệu quả và tự động xử lý LEFT JOIN khi có kiểm tra null.
            return await _context.Teachers
                .Select(t => new TeacherDto
                {
                    TeacherId = t.Teacherid,
                    TeacherCode = t.Teachercode,
                    Fullname = t.Fullname,
                    DateOfBirth = t.Dateofbirth,
                    Gender = t.Gender,
                    Ethnicity = t.Ethnicity,
                    HireDate = t.Hiredate,
                    Nationality = t.Nationality,
                    Religion = t.Religion,
                    Status = t.Status,
                    Alias = t.Alias,
                    AddressProvinceCity = t.AddressProvincecity,
                    AddressWard = t.AddressWard,
                    AddressDistrict = t.AddressDistrict,
                    AddressStreet = t.AddressStreet,
                    Email = t.Email,
                    PhoneNumber = t.Phonenumber,
                    DateOfJoiningTheParty = t.Dateofjoiningtheparty,
                    AvatarUrl = t.Avatarurl,
                    DateOfJoiningGroup = t.Dateofjoininggroup,
                    IsPartyMember = t.Ispartymember,
                    CreatedAt = t.Createdat,
                    UpdatedAt = t.Updatedat,
                    // Kiểm tra null để đảm bảo không lỗi và tạo LEFT JOIN
                    DepartmentName = t.Department != null ? t.Department.Departmentname : null,
                    SubjectName = t.Subject != null ? t.Subject.Subjectname : null,
                    SchoolyearName = t.Schoolyear != null ? t.Schoolyear.Schoolyearname : null // Giả sử trường tên là 'Name'
                }).ToListAsync();
        }

        public async Task<TeacherDto?> GetByIdAsync(int id)
        {
            // Tối ưu lại GetById để dùng .Select giống GetAllAsync, hiệu quả hơn
            return await _context.Teachers
                .Where(t => t.Teacherid == id)
                .Select(t => new TeacherDto
                {
                    // Copy toàn bộ các trường như ở GetAllAsync
                    TeacherId = t.Teacherid,
                    TeacherCode = t.Teachercode,
                    Fullname = t.Fullname,
                    DateOfBirth = t.Dateofbirth,
                    Gender = t.Gender,
                    Email = t.Email,
                    Status = t.Status,
                    PhoneNumber = t.Phonenumber,
                    // ... các trường khác
                    DepartmentName = t.Department != null ? t.Department.Departmentname : null,
                    SubjectName = t.Subject != null ? t.Subject.Subjectname : null,
                    SchoolyearName = t.Schoolyear != null ? t.Schoolyear.Schoolyearname : null
                }).FirstOrDefaultAsync();
        }

        public async Task<Teacher> CreateAsync(Teacher teacher)
        {
            // Tự động sinh Teachercode
            var lastTeacher = await _context.Teachers
                .OrderByDescending(t => t.Teacherid)
                .FirstOrDefaultAsync();

            int newNumber = 1;
            if (lastTeacher != null && !string.IsNullOrEmpty(lastTeacher.Teachercode) && lastTeacher.Teachercode.Length > 2)
            {
                int.TryParse(lastTeacher.Teachercode.AsSpan(2), out int lastNumber);
                newNumber = lastNumber + 1;
            }
            teacher.Teachercode = "GV" + newNumber.ToString("D5");

            teacher.Createdat = DateTime.UtcNow;
            teacher.Updatedat = DateTime.UtcNow;

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher?> UpdateAsync(int id, Teacher updatedTeacher)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);
            if (existingTeacher == null) return null;


            _context.Entry(existingTeacher).CurrentValues.SetValues(updatedTeacher);
            existingTeacher.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTeacher;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return false;

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}