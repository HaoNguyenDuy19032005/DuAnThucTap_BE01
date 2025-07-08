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

    public async Task<IEnumerable<Schoolyear>> GetAllAsync()
    {
        return await _context.Schoolyears.ToListAsync();
    }

    public async Task<Schoolyear?> GetByIdAsync(int id)
    {
        return await _context.Schoolyears.FindAsync(id);
    }

    public async Task<Schoolyear> CreateAsync(Schoolyear schoolyear)
    {
        _context.Schoolyears.Add(schoolyear);
        await _context.SaveChangesAsync();
        return schoolyear;
    }

    public async Task<bool> UpdateAsync(int id, Schoolyear schoolyear)
    {
        if (id != schoolyear.Schoolyearid)
            return false;

        _context.Entry(schoolyear).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
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
