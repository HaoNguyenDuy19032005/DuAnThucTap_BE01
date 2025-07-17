using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response; 
using Microsoft.AspNetCore.Http;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherService
    {
        Task<PagedResponse<TeacherDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<TeacherDto?> GetByIdAsync(int id);
        Task<Teacher> CreateAsync(TeacherRequestDto teacherDto);
        Task<Teacher?> UpdateAsync(int id, TeacherRequestDto teacherDto);
        Task<string?> UpdateAvatarAsync(int id, IFormFile avatarFile);
        Task<bool> DeleteAsync(int id);
    }
}