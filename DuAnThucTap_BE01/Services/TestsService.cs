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
                                         (t.TeacherName != null && t.TeacherName.ToLower().Contains(searchQuery)));
            }

            query = query.OrderBy(t => t.Testid).Skip((page - 1) * pageSize).Take(pageSize);
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

        public async Task<TestDto> CreateAsync(TestRequestDto testDto)
        {
            var teacher = await _context.Teachers.FindAsync(testDto.Teacherid);
            if (teacher == null)
            {
                throw new ArgumentException("Giáo viên không tồn tại.");
            }

            string? attachmentUrl = null;
            if (testDto.AttachmentFile != null && testDto.AttachmentFile.Length > 0)
            {
                attachmentUrl = await _firebaseStorage.UploadFileAsync(testDto.AttachmentFile, "test_attachments/");
            }

            var test = new Test
            {
                Title = testDto.Title,
                Testformat = testDto.Testformat,
                Durationinminutes = testDto.Durationinminutes,
                Starttime = testDto.Starttime.HasValue ? DateTime.SpecifyKind(testDto.Starttime.Value, DateTimeKind.Utc) : null,
                Endtime = testDto.Endtime.HasValue ? DateTime.SpecifyKind(testDto.Endtime.Value, DateTimeKind.Utc) : null,
                Description = testDto.Description,
                Classification = testDto.Classification,
                Requirestudentattachment = testDto.Requirestudentattachment,
                Teacherid = testDto.Teacherid,
                Attachmenturl = attachmentUrl
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return new TestDto
            {
                Testid = test.Testid,
                Title = test.Title,
                Testformat = test.Testformat,
                Durationinminutes = test.Durationinminutes,
                Starttime = test.Starttime,
                Endtime = test.Endtime,
                Description = test.Description,
                Classification = test.Classification,
                Attachmenturl = test.Attachmenturl,
                Requirestudentattachment = test.Requirestudentattachment,
                TeacherName = teacher.Fullname
            };
        }

        public async Task<TestDto?> UpdateAsync(int id, TestRequestDto testDto)
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

            if (testDto.AttachmentFile != null && testDto.AttachmentFile.Length > 0)
            {
                existing.Attachmenturl = await _firebaseStorage.UploadFileAsync(testDto.AttachmentFile, "test_attachments/");
            }

            existing.Title = testDto.Title;
            existing.Testformat = testDto.Testformat;
            existing.Durationinminutes = testDto.Durationinminutes;
            existing.Starttime = testDto.Starttime.HasValue ? DateTime.SpecifyKind(testDto.Starttime.Value, DateTimeKind.Utc) : null;
            existing.Endtime = testDto.Endtime.HasValue ? DateTime.SpecifyKind(testDto.Endtime.Value, DateTimeKind.Utc) : null;
            existing.Description = testDto.Description;
            existing.Classification = testDto.Classification;
            existing.Requirestudentattachment = testDto.Requirestudentattachment;
            existing.Teacherid = testDto.Teacherid;

            await _context.SaveChangesAsync();

            return new TestDto
            {
                Testid = existing.Testid,
                Title = existing.Title,
                Testformat = existing.Testformat,
                Durationinminutes = existing.Durationinminutes,
                Starttime = existing.Starttime,
                Endtime = existing.Endtime,
                Description = existing.Description,
                Classification = existing.Classification,
                Attachmenturl = existing.Attachmenturl,
                Requirestudentattachment = existing.Requirestudentattachment,
                TeacherName = teacher.Fullname
            };
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