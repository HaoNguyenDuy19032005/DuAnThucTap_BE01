using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface ISchoolTransferHistoryService
    {
        Task<IEnumerable<Schooltransferhistory>> GetAllAsync();
        Task<(List<Schooltransferhistory> Transfers, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Schooltransferhistory?> GetByIdAsync(int id);
        Task<Schooltransferhistory> CreateAsync(Schooltransferhistory transfer);
        Task<bool> DeleteAsync(int id);

        Task<Schooltransferhistory?> UpdateAsync(int id, Schooltransferhistory transfer);
    }
}
