using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;

public interface IClassTransferService
{
    Task<List<Classtransferhistory>> GetAllAsync();
    Task<Classtransferhistory?> GetByIdAsync(int id);
    Task<(List<Classtransferhistory> Transfers, int TotalCount)> GetPagedAsync(int page, int pageSize);
    Task<bool> TransferAndUpdateStatusAsync(ClasstransferhistoryCreateDto transferDto);
    Task<bool> UpdateTransferAsync(int id, ClasstransferhistoryCreateDto transferDto); // Sử dụng chung DTO
    Task<bool> DeleteTransferAsync(int id);
    Task<bool> ExistsStudentAsync(int studentId);
    Task<bool> ExistsClassAsync(int classId);

}