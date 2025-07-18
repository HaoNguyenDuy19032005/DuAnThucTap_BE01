using DuAnThucTap.Model;

public interface IClassService
{
    Task<PaginatedList<Class>> GetAllAsync(string? search, int pageIndex, int pageSize);
    Task<Class?> GetByIdAsync(int id);
    Task<Class> CreateAsync(CreateClassDto dto);
    Task<Class?> UpdateAsync(int id, CreateClassDto dto); // ✅ KHÔNG có async ở đây!
    Task<bool> DeleteAsync(int id);
}

