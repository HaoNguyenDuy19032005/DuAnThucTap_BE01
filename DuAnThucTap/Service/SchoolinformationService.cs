using DuAnThucTap.Data;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SchoolinformationService : ISchoolinformationService
{
    private readonly ApplicationDbContext _context;

    public SchoolinformationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Schoolinformation>> GetAllAsync()
    {
        return await _context.Schoolinformations.ToListAsync();
    }

    public async Task<Schoolinformation?> GetByIdAsync(int id)
    {
        return await _context.Schoolinformations.FindAsync(id);
    }

    public async Task<Schoolinformation> CreateAsync(Schoolinformation school)
    {
        _context.Schoolinformations.Add(school);
        await _context.SaveChangesAsync();
        return school;
    }

    public async Task<bool> UpdateAsync(int id, Schoolinformation school)
    {
        if (id != school.Schoolinfoid) return false;

        _context.Entry(school).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Schoolinformations.FindAsync(id);
        if (entity == null) return false;

        _context.Schoolinformations.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
