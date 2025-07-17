using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
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

        public async Task<PagedResponse<TeacherDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize)
        {
            var query = _context.Teachers.AsQueryable();

            // Logic tìm kiếm
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(t =>
                    (t.Fullname != null && t.Fullname.ToLower().Contains(searchQuery.ToLower())) ||
                    (t.Teachercode != null && t.Teachercode.ToLower().Contains(searchQuery.ToLower()))
                );
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TeacherDto
                {
                    TeacherId = t.Teacherid,
                    TeacherCode = t.Teachercode,
                    Fullname = t.Fullname,
                    Email = t.Email,
                    PhoneNumber = t.Phonenumber,
                    Status = t.Status,
                    AvatarUrl = t.Avatarurl,
                    DepartmentName = t.Department != null ? t.Department.Departmentname : null,
                    SubjectName = t.Subject != null ? t.Subject.Subjectname : null,
                    SchoolyearName = t.Schoolyear != null ? t.Schoolyear.Schoolyearname : null,
                })
                .ToListAsync();

            return new PagedResponse<TeacherDto>(pagedData, pageNumber, pageSize, totalRecords);
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
                Avatarurl = null
            };

            var lastTeacher = await _context.Teachers.OrderByDescending(t => t.Teacherid).FirstOrDefaultAsync();
            int newNumber = 1;
            if (lastTeacher != null && !string.IsNullOrEmpty(lastTeacher.Teachercode))
            {
                if (int.TryParse(lastTeacher.Teachercode.AsSpan(2), out int lastNum))
                {
                    newNumber = lastNum + 1;
                }
            }
            teacher.Teachercode = "GV" + newNumber.ToString("D5");

            teacher.Createdat = DateTime.UtcNow;
            teacher.Updatedat = DateTime.UtcNow;

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher?> UpdateAsync(int id, TeacherRequestDto teacherDto)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);
            if (existingTeacher == null) return null;

            // Cập nhật tất cả các trường từ DTO
            existingTeacher.Fullname = teacherDto.Fullname;
            existingTeacher.Dateofbirth = teacherDto.Dateofbirth;
            existingTeacher.Gender = teacherDto.Gender;
            existingTeacher.Email = teacherDto.Email;
            existingTeacher.Phonenumber = teacherDto.Phonenumber;
            existingTeacher.Status = teacherDto.Status;
            existingTeacher.Departmentid = teacherDto.Departmentid;
            existingTeacher.Subjectid = teacherDto.Subjectid;
            existingTeacher.Schoolyearid = teacherDto.Schoolyearid;
            existingTeacher.Ethnicity = teacherDto.Ethnicity;
            existingTeacher.Hiredate = teacherDto.Hiredate;
            existingTeacher.Nationality = teacherDto.Nationality;
            existingTeacher.Religion = teacherDto.Religion;
            existingTeacher.Alias = teacherDto.Alias;
            existingTeacher.AddressProvincecity = teacherDto.AddressProvincecity;
            existingTeacher.AddressWard = teacherDto.AddressWard;
            existingTeacher.AddressDistrict = teacherDto.AddressDistrict;
            existingTeacher.AddressStreet = teacherDto.AddressStreet;
            existingTeacher.Dateofjoiningtheparty = teacherDto.Dateofjoiningtheparty;
            existingTeacher.Dateofjoininggroup = teacherDto.Dateofjoininggroup;
            existingTeacher.Ispartymember = teacherDto.Ispartymember;
            existingTeacher.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTeacher;
        }

        public async Task<string?> UpdateAvatarAsync(int id, IFormFile avatarFile)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);
            if (existingTeacher == null) return null;

            existingTeacher.Avatarurl = await _firebaseStorage.UploadFileAsync(avatarFile, "teacher_avatars/");
            existingTeacher.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();
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