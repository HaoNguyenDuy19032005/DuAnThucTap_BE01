using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class TestsService : ITests
    {
        private readonly ISCDbContext _context;
        private readonly IFirebaseStorageService _firebaseStorage;

        public TestsService(ISCDbContext context, IFirebaseStorageService firebaseStorage)
        {
            _context = context;
            _firebaseStorage = firebaseStorage;
        }

        public async Task<IEnumerable<TestDto>> GetAllAsync(string? searchQuery, int page, int pageSize)
        {
            var query = _context.Tests
                .Include(t => t.Teacher)
                .Select(t => new TestDto
                {
                    Testid = t.Testid,
                    Title = t.Title,
                    Testformat = t.Testformat,
                    Durationinminutes = t.Durationinminutes,
                    Starttime = t.Starttime,
                    Endtime = t.Endtime,
                    Description = t.Description,
                    Classification = t.Classification,
                    Attachmenturl = t.Attachmenturl,
                    Requirestudentattachment = t.Requirestudentattachment,
                    TeacherName = t.Teacher != null ? t.Teacher.Fullname : null
                });

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                query = query.Where(t => t.Title.ToLower().Contains(searchQuery) ||
                                         t.TeacherName.ToLower().Contains(searchQuery));
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return await query.ToListAsync();
        }

        public async Task<TestDto?> GetByIdAsync(int id)
        {
            return await _context.Tests
                .Where(t => t.Testid == id)
                .Include(t => t.Teacher)
                .Select(t => new TestDto
                {
                    Testid = t.Testid,
                    Title = t.Title,
                    Testformat = t.Testformat,
                    Durationinminutes = t.Durationinminutes,
                    Starttime = t.Starttime,
                    Endtime = t.Endtime,
                    Description = t.Description,
                    Classification = t.Classification,
                    Attachmenturl = t.Attachmenturl,
                    Requirestudentattachment = t.Requirestudentattachment,
                    TeacherName = t.Teacher != null ? t.Teacher.Fullname : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Test> CreateAsync(TestRequestDto testDto)
        {
            var teacher = await _context.Teachers.FindAsync(testDto.Teacherid);
            if (teacher == null)
            {
                throw new ArgumentException("Giáo viên không tồn tại.");
            }

            var test = new Test
            {
                Title = testDto.Title,
                Testformat = testDto.Testformat,
                Durationinminutes = testDto.Durationinminutes,
                Starttime = testDto.Starttime,
                Endtime = testDto.Endtime,
                Description = testDto.Description,
                Classification = testDto.Classification,
                Requirestudentattachment = testDto.Requirestudentattachment,
                Teacherid = testDto.Teacherid
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<Test?> UpdateAsync(int id, TestRequestDto testDto)
        {
            var existing = await _context.Tests.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            var teacher = await _context.Teachers.FindAsync(testDto.Teacherid);
            if (teacher == null)
            {
                throw new ArgumentException("Giáo viên không tồn tại.");
            }

            existing.Title = testDto.Title;
            existing.Testformat = testDto.Testformat;
            existing.Durationinminutes = testDto.Durationinminutes;
            existing.Starttime = testDto.Starttime;
            existing.Endtime = testDto.Endtime;
            existing.Description = testDto.Description;
            existing.Classification = testDto.Classification;
            existing.Requirestudentattachment = testDto.Requirestudentattachment;
            existing.Teacherid = testDto.Teacherid;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<string?> UpdateAttachmentAsync(int id, IFormFile attachmentFile)
        {
            var existing = await _context.Tests.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            // Kiểm tra định dạng file
            var allowedExtensions = new[] { ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx", ".jpeg" };
            var extension = Path.GetExtension(attachmentFile.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Chỉ chấp nhận các định dạng file: doc, docx, ppt, pptx, xls, xlsx, jpeg.");
            }

            // Kiểm tra kích thước file (50MB = 50 * 1024 * 1024 bytes)
            if (attachmentFile.Length > 50 * 1024 * 1024)
            {
                throw new ArgumentException("Kích thước file không được vượt quá 50MB.");
            }

            // Upload file lên Firebase
            existing.Attachmenturl = await _firebaseStorage.UploadFileAsync(attachmentFile, "test_attachments/");
            await _context.SaveChangesAsync();
            return existing.Attachmenturl;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return false;
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}