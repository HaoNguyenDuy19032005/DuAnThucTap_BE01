using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ISCDbContext _context;
        private readonly IFirebaseStorageService _firebaseStorage;

        public TeacherService(ISCDbContext context, IFirebaseStorageService firebaseStorage)
        {
            _context = context;
            _firebaseStorage = firebaseStorage;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
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
                    DepartmentName = t.Department != null ? t.Department.Departmentname : null,
                    SubjectName = t.Subject != null ? t.Subject.Subjectname : null,
                    SchoolyearName = t.Schoolyear != null ? t.Schoolyear.Schoolyearname : null
                }).ToListAsync();
        }

        public async Task<TeacherDto?> GetByIdAsync(int id)
        {
            return await _context.Teachers
                .Where(t => t.Teacherid == id)
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
                    DepartmentName = t.Department != null ? t.Department.Departmentname : null,
                    SubjectName = t.Subject != null ? t.Subject.Subjectname : null,
                    SchoolyearName = t.Schoolyear != null ? t.Schoolyear.Schoolyearname : null
                }).FirstOrDefaultAsync();
        }

        public async Task<Teacher> CreateAsync(TeacherRequestDto teacherDto)
        {
            var teacher = new Teacher
            {
                // Gán tất cả dữ liệu từ DTO, không có AvatarUrl
                Fullname = teacherDto.Fullname,
                Dateofbirth = teacherDto.Dateofbirth,
                Gender = teacherDto.Gender,
                Email = teacherDto.Email,
                Phonenumber = teacherDto.Phonenumber,
                Status = teacherDto.Status,
                Departmentid = teacherDto.Departmentid,
                Subjectid = teacherDto.Subjectid,
                Schoolyearid = teacherDto.Schoolyearid,
                Ethnicity = teacherDto.Ethnicity,
                Hiredate = teacherDto.Hiredate,
                Nationality = teacherDto.Nationality,
                Religion = teacherDto.Religion,
                Alias = teacherDto.Alias,
                AddressProvincecity = teacherDto.AddressProvincecity,
                AddressWard = teacherDto.AddressWard,
                AddressDistrict = teacherDto.AddressDistrict,
                AddressStreet = teacherDto.AddressStreet,
                Dateofjoiningtheparty = teacherDto.Dateofjoiningtheparty,
                Dateofjoininggroup = teacherDto.Dateofjoininggroup,
                Ispartymember = teacherDto.Ispartymember,
                // Avatarurl sẽ trống khi mới tạo
                Avatarurl = null
            };

            // Logic tạo mã giáo viên giữ nguyên
            var lastTeacher = await _context.Teachers.OrderByDescending(t => t.Teacherid).FirstOrDefaultAsync();
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

        // UpdateAsync đã được cập nhật
        public async Task<Teacher?> UpdateAsync(int id, TeacherRequestDto teacherDto)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);
            if (existingTeacher == null) return null;

            // Gán lại tất cả các giá trị từ DTO
            existingTeacher.Fullname = teacherDto.Fullname;
            existingTeacher.Dateofbirth = teacherDto.Dateofbirth;
            // ... gán tất cả các trường khác tương tự ...
            existingTeacher.Ispartymember = teacherDto.Ispartymember;

            // Bỏ logic xử lý avatarFile ở đây

            existingTeacher.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTeacher;
        }

        // Phương thức MỚI để xử lý upload
        public async Task<string?> UpdateAvatarAsync(int id, IFormFile avatarFile)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);
            if (existingTeacher == null)
            {
                // Trả về null nếu không tìm thấy giáo viên
                return null;
            }

            // Chỉ xử lý upload file và cập nhật URL
            existingTeacher.Avatarurl = await _firebaseStorage.UploadFileAsync(avatarFile, "teacher_avatars/");
            existingTeacher.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Trả về URL của ảnh mới
            return existingTeacher.Avatarurl;
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