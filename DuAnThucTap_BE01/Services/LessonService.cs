// File: Services/LessonService.cs
using DuAnThucTap_BE01.Data;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Services
{
    public class LessonService : ILessonService
    {
        private readonly ISCDbContext _context;

        public LessonService(ISCDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<LessonDto>> GetAllLessonsAsync(int pageNumber, int pageSize, string? searchQuery)
        {
            var query = _context.Lessons
                .Include(l => l.Course)
                .Include(l => l.Teacher)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(l => l.Title.ToLower().Contains(searchQuery.ToLower()));
            }

            var totalRecords = await query.CountAsync();

            var lessons = await query
                .OrderByDescending(l => l.Lessonid)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new LessonDto
                {
                    LessonId = l.Lessonid,
                    Title = l.Title,
                    Description = l.Description,
                    StartTime = l.Starttime,
                    EndTime = l.Endtime,
                    DurationInMinutes = l.Durationinminutes,
                    IsRecordingEnabled = l.Isrecordingenabled,
                    AllowStudentSharing = l.Allowstudentsharing,
                    ShareableLink = l.Shareablelink,
                    MeetingId = l.Meetingid,
                    CreatedAt = l.Createdat,
                    CourseName = l.Course.Coursename,
                    TeacherName = l.Teacher.Fullname
                })
                .ToListAsync();

            return new PagedResponse<LessonDto>(lessons, pageNumber, pageSize, totalRecords);
        }

        public async Task<LessonDto?> GetLessonByIdAsync(int id)
        {
            return await _context.Lessons
                .Include(l => l.Course)
                .Include(l => l.Teacher)
                .Where(l => l.Lessonid == id)
                .Select(l => new LessonDto
                {
                    LessonId = l.Lessonid,
                    Title = l.Title,
                    Description = l.Description,
                    StartTime = l.Starttime,
                    EndTime = l.Endtime,
                    DurationInMinutes = l.Durationinminutes,
                    IsRecordingEnabled = l.Isrecordingenabled,
                    AllowStudentSharing = l.Allowstudentsharing,
                    ShareableLink = l.Shareablelink,
                    MeetingId = l.Meetingid,
                    CreatedAt = l.Createdat,
                    CourseName = l.Course.Coursename,
                    TeacherName = l.Teacher.Fullname
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LessonDto?> GetLessonByTitleAsync(string title)
        {
            return await _context.Lessons
                .Include(l => l.Course)
                .Include(l => l.Teacher)
                .Where(l => l.Title.ToLower() == title.ToLower())
                .Select(l => new LessonDto
                {
                    LessonId = l.Lessonid,
                    Title = l.Title,
                    Description = l.Description,
                    StartTime = l.Starttime,
                    EndTime = l.Endtime,
                    DurationInMinutes = l.Durationinminutes,
                    IsRecordingEnabled = l.Isrecordingenabled,
                    AllowStudentSharing = l.Allowstudentsharing,
                    ShareableLink = l.Shareablelink,
                    MeetingId = l.Meetingid,
                    CreatedAt = l.Createdat,
                    CourseName = l.Course.Coursename,
                    TeacherName = l.Teacher.Fullname
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Lesson> CreateLessonAsync(LessonRequestDto lessonDto)
        {
            var lesson = new Lesson
            {
                Courseid = lessonDto.CourseId,
                Teacherid = lessonDto.TeacherId,
                Title = lessonDto.Title,
                Description = lessonDto.Description,
                Starttime = lessonDto.StartTime,
                Endtime = lessonDto.EndTime,
                Password = lessonDto.Password,
                Autostartontime = lessonDto.AutoStartOnTime,
                Isrecordingenabled = lessonDto.IsRecordingEnabled,
                Allowstudentsharing = lessonDto.AllowStudentSharing,
                Durationinminutes = (int)(lessonDto.EndTime.Value - lessonDto.StartTime.Value).TotalMinutes,
                Meetingid = $"MEET_{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                Createdat = DateTime.UtcNow
            };

            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson?> UpdateLessonAsync(int id, LessonRequestDto lessonDto)
        {
            var existingLesson = await _context.Lessons.FindAsync(id);
            if (existingLesson == null)
            {
                return null;
            }

            existingLesson.Courseid = lessonDto.CourseId;
            existingLesson.Teacherid = lessonDto.TeacherId;
            existingLesson.Title = lessonDto.Title;
            existingLesson.Description = lessonDto.Description;
            existingLesson.Starttime = lessonDto.StartTime;
            existingLesson.Endtime = lessonDto.EndTime;
            existingLesson.Password = lessonDto.Password;
            existingLesson.Autostartontime = lessonDto.AutoStartOnTime;
            existingLesson.Isrecordingenabled = lessonDto.IsRecordingEnabled;
            existingLesson.Allowstudentsharing = lessonDto.AllowStudentSharing;
            existingLesson.Durationinminutes = (int)(lessonDto.EndTime.Value - lessonDto.StartTime.Value).TotalMinutes;

            await _context.SaveChangesAsync();
            return existingLesson;
        }

        public async Task<bool> DeleteLessonAsync(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return false;
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}