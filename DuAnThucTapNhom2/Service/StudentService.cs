using DuAnThucTapNhom2.Data;
using DuAnThucTapNhom2.Iterface;
using DuAnThucTapNhom2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DuAnThucTapNhom2.Service
{
    public class StudentService : IStudentService
    {
        private readonly StudentsDbContext _context;

        public StudentService(StudentsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> UpdateAsync(int id, Student student)
        {
            var existing = await _context.Students.FindAsync(id);
            if (existing == null) return null;

            // Không cập nhật khóa chính
            // Gán từng property ngoại trừ Pk
            existing.Studentid = student.Studentid;
            existing.Fullname = student.Fullname;
            existing.Gender = student.Gender;
            existing.Dateofbirth = student.Dateofbirth;
            existing.Studentcode = student.Studentcode;
            existing.Birthplace = student.Birthplace;
            existing.Enrollmentdate = student.Enrollmentdate;
            existing.Ethnicity = student.Ethnicity;
            existing.Admissiontype = student.Admissiontype;
            existing.Religion = student.Religion;
            existing.Status = student.Status;
            existing.AddressProvincecity = student.AddressProvincecity;
            existing.AddressDistrict = student.AddressDistrict;
            existing.AddressWard = student.AddressWard;
            existing.AddressStreet = student.AddressStreet;
            existing.Email = student.Email;
            existing.Phonenumber = student.Phonenumber;
            existing.Fathername = student.Fathername;
            existing.Fatherbirthyear = student.Fatherbirthyear;
            existing.Fatheroccupation = student.Fatheroccupation;
            existing.Mothername = student.Mothername;
            existing.Motherbirthyear = student.Motherbirthyear;
            existing.Motheroccupation = student.Motheroccupation;
            existing.Guardianname = student.Guardianname;
            existing.Guardianbirthyear = student.Guardianbirthyear;
            existing.Guardianoccupation = student.Guardianoccupation;
            existing.Phonenumberfather = student.Phonenumberfather;
            existing.Phonenumbermother = student.Phonenumbermother;
            existing.Phonenumberguardian = student.Phonenumberguardian;
            existing.Profileimageurl = student.Profileimageurl;
            existing.FkSchoolyearid = student.FkSchoolyearid;
            existing.Gradelevelid = student.Gradelevelid;
            existing.FkClassid = student.FkClassid;
            // ... các trường khác nếu có ...

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
