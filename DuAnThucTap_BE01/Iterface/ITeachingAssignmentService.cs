using DuAnThucTap_BE01.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeachingAssignmentService
    {
        Task<IEnumerable<TeachingAssignmentDto>> GetAllAsync();
        Task<TeachingAssignmentDto?> GetByIdAsync(Guid id);
        Task<TeachingAssignmentDto> CreateAsync(TeachingAssignmentDto teachingAssignment);
        Task<TeachingAssignmentDto?> UpdateAsync(Guid id, TeachingAssignmentDto updatedTeachingAssignment);
        Task<bool> DeleteAsync(Guid id);
    }
}