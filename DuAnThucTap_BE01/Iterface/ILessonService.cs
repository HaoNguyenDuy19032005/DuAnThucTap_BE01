// File: Interface/ILessonService.cs
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ILessonService
    {
        Task<PagedResponse<LessonDto>> GetAllLessonsAsync(int pageNumber, int pageSize, string? searchQuery);
        Task<LessonDto?> GetLessonByIdAsync(int id);
        Task<Lesson> CreateLessonAsync(LessonRequestDto lessonDto);
        Task<Lesson?> UpdateLessonAsync(int id, LessonRequestDto lessonDto);
        Task<bool> DeleteLessonAsync(int id);
    }
}