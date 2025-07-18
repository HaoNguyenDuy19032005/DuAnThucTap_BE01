using DuAnThucTap.Data;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SchoolyearService : ISchoolyearService
{
    private readonly ApplicationDbContext _context;

    public SchoolyearService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Schoolyear>> GetAllAsync(string? search, int pageIndex, int pageSize)
    {
        var query = _context.Schoolyears.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(s => s.Schoolyearname.Contains(search));
        }

        query = query.OrderByDescending(s => s.Startyear);

        var paginated = await Task.Run(() =>
            PaginatedList<Schoolyear>.Create(query, pageIndex, pageSize)
        );

        return paginated;
    }


    public async Task<Schoolyear?> GetByIdAsync(int id)
    {
        return await _context.Schoolyears.FindAsync(id);
    }

    public async Task<Schoolyear> CreateAsync(Schoolyear schoolyear)
    {
        if (schoolyear.Startyear >= schoolyear.Endyear)
            throw new ArgumentException("Năm bắt đầu phải nhỏ hơn năm kết thúc.");

        var schoolInfoExists = await _context.Schoolinformations
            .AnyAsync(s => s.Schoolinfoid == schoolyear.Schoolinfoid);

        if (!schoolInfoExists)
            throw new ArgumentException($"Schoolinfoid {schoolyear.Schoolinfoid} không tồn tại.");

        schoolyear.Createdat = DateTime.UtcNow;

        _context.Schoolyears.Add(schoolyear);
        await _context.SaveChangesAsync();
        return schoolyear;
    }

    public async Task<bool> UpdateAsync(int id, Schoolyear schoolyear)
    {
        if (schoolyear.Startyear >= schoolyear.Endyear)
            throw new ArgumentException("Năm bắt đầu phải nhỏ hơn năm kết thúc.");

        var schoolInfoExists = await _context.Schoolinformations
            .AnyAsync(s => s.Schoolinfoid == schoolyear.Schoolinfoid);
        if (!schoolInfoExists)
            throw new ArgumentException($"Schoolinfoid {schoolyear.Schoolinfoid} không tồn tại.");

        var existing = await _context.Schoolyears.FirstOrDefaultAsync(s => s.Schoolyearid == id);
        if (existing == null)
            return false;

        // Gán từng field
        existing.Schoolyearname = schoolyear.Schoolyearname;
        existing.Startyear = schoolyear.Startyear;
        existing.Endyear = schoolyear.Endyear;
        existing.Schoolinfoid = schoolyear.Schoolinfoid;
        existing.Updatedat = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Schoolyears.FindAsync(id);
        if (entity == null) return false;

        _context.Schoolyears.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

