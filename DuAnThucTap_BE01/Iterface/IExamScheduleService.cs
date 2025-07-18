using DuAnThucTap_BE01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface IExamScheduleService
    {
        Task<IEnumerable<Examschedule>> GetAllAsync();
        Task<Examschedule?> GetByIdAsync(int examScheduleId);
        Task<Examschedule> CreateAsync(Examschedule examSchedule);
        Task<Examschedule?> UpdateAsync(int examScheduleId, Examschedule updatedExamSchedule);
        Task<bool> DeleteAsync(int examScheduleId);
    }
}