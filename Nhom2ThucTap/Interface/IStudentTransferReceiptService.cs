using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IStudentTransferReceiptService
    {
        Task<IEnumerable<StudentTransferReceipt>> GetAllAsync();
        Task<(List<StudentTransferReceipt> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<StudentTransferReceipt?> GetByIdAsync(int id);
        Task<StudentTransferReceipt> CreateAsync(StudentTransferReceipt receipt);
        Task<bool> UpdateAsync(int id, StudentTransferReceipt receipt);
        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsSemesterAsync(int semesterId);
    }
}
