
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Http;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto?> GetByIdAsync(int id);

        Task<Teacher> CreateAsync(TeacherRequestDto teacherDto);

        Task<Teacher?> UpdateAsync(int id, TeacherRequestDto teacherDto);
        Task<string?> UpdateAvatarAsync(int id, IFormFile avatarFile);

        Task<bool> DeleteAsync(int id);
    }
}