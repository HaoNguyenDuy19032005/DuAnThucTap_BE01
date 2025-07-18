using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(t =>
                    (t.Fullname != null && t.Fullname.ToLower().Contains(searchQuery.ToLower())) ||
                    (t.Teachercode != null && t.Teachercode.ToLower().Contains(searchQuery.ToLower()))
                );
            }

            var totalRecords = await query.CountAsync();

            var pagedData = await query
                .OrderByDescending(t => t.Teacherid)
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

        public async Task<Teacher> CreateAsync(TeacherRequestDto teacherDto, IFormFile? avatarFile)
        {
            // 1. Xử lý tải file avatar nếu có
            string? avatarUrl = null;
            if (avatarFile != null && avatarFile.Length > 0)
            {
                avatarUrl = await _firebaseStorage.UploadFileAsync(avatarFile, "teacher_avatars/");
            }

            // 2. Chuyển đổi các chuỗi ngày tháng từ DTO
            DateOnly.TryParse(teacherDto.Dateofbirth, out var dateOfBirth);
            DateOnly.TryParse(teacherDto.Hiredate, out var hireDate);
            DateOnly.TryParse(teacherDto.Dateofjoiningtheparty, out var partyDate);
            DateOnly.TryParse(teacherDto.Dateofjoininggroup, out var groupDate);

            // 3. Ánh xạ từ DTO sang Model
            var teacher = new Teacher
            {
                Fullname = teacherDto.Fullname,
                Email = teacherDto.Email,
                Phonenumber = teacherDto.Phonenumber,
                Gender = teacherDto.Gender,
                Status = teacherDto.Status,
                Ethnicity = teacherDto.Ethnicity,
                Nationality = teacherDto.Nationality,
                Religion = teacherDto.Religion,
                Alias = teacherDto.Alias,
                AddressProvincecity = teacherDto.AddressProvincecity,
                AddressWard = teacherDto.AddressWard,
                AddressDistrict = teacherDto.AddressDistrict,
                AddressStreet = teacherDto.AddressStreet,
                Ispartymember = teacherDto.Ispartymember,
                Departmentid = teacherDto.Departmentid,
                Subjectid = teacherDto.Subjectid,
                Schoolyearid = teacherDto.Schoolyearid,
                Avatarurl = avatarUrl,
                Dateofbirth = string.IsNullOrEmpty(teacherDto.Dateofbirth) ? null : dateOfBirth,
                Hiredate = string.IsNullOrEmpty(teacherDto.Hiredate) ? null : hireDate,
                Dateofjoiningtheparty = string.IsNullOrEmpty(teacherDto.Dateofjoiningtheparty) ? null : partyDate,
                Dateofjoininggroup = string.IsNullOrEmpty(teacherDto.Dateofjoininggroup) ? null : groupDate,
                Createdat = DateTime.UtcNow,
                Updatedat = DateTime.UtcNow
            };

            // 4. Logic tạo mã giáo viên
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

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher?> UpdateAsync(int id, TeacherRequestDto teacherDto, IFormFile? avatarFile)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);
            if (existingTeacher == null) return null;

            // 1. Xử lý tải file avatar nếu có file mới
            if (avatarFile != null && avatarFile.Length > 0)
            {
                existingTeacher.Avatarurl = await _firebaseStorage.UploadFileAsync(avatarFile, "teacher_avatars/");
            }

            // 2. Chuyển đổi các chuỗi ngày tháng từ DTO
            DateOnly.TryParse(teacherDto.Dateofbirth, out var dateOfBirth);
            DateOnly.TryParse(teacherDto.Hiredate, out var hireDate);
            DateOnly.TryParse(teacherDto.Dateofjoiningtheparty, out var partyDate);
            DateOnly.TryParse(teacherDto.Dateofjoininggroup, out var groupDate);

            // 3. Cập nhật các thông tin khác từ DTO
            existingTeacher.Fullname = teacherDto.Fullname;
            existingTeacher.Email = teacherDto.Email;
            existingTeacher.Phonenumber = teacherDto.Phonenumber;
            existingTeacher.Gender = teacherDto.Gender;
            existingTeacher.Status = teacherDto.Status;
            existingTeacher.Ethnicity = teacherDto.Ethnicity;
            existingTeacher.Nationality = teacherDto.Nationality;
            existingTeacher.Religion = teacherDto.Religion;
            existingTeacher.Alias = teacherDto.Alias;
            existingTeacher.AddressProvincecity = teacherDto.AddressProvincecity;
            existingTeacher.AddressWard = teacherDto.AddressWard;
            existingTeacher.AddressDistrict = teacherDto.AddressDistrict;
            existingTeacher.AddressStreet = teacherDto.AddressStreet;
            existingTeacher.Ispartymember = teacherDto.Ispartymember;
            existingTeacher.Departmentid = teacherDto.Departmentid;
            existingTeacher.Subjectid = teacherDto.Subjectid;
            existingTeacher.Schoolyearid = teacherDto.Schoolyearid;
            existingTeacher.Dateofbirth = string.IsNullOrEmpty(teacherDto.Dateofbirth) ? null : dateOfBirth;
            existingTeacher.Hiredate = string.IsNullOrEmpty(teacherDto.Hiredate) ? null : hireDate;
            existingTeacher.Dateofjoiningtheparty = string.IsNullOrEmpty(teacherDto.Dateofjoiningtheparty) ? null : partyDate;
            existingTeacher.Dateofjoininggroup = string.IsNullOrEmpty(teacherDto.Dateofjoininggroup) ? null : groupDate;
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