using DuAnThucTap.Data;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SubjecttypeService : ISubjecttypeService
{
    private readonly ApplicationDbContext _context;

    public SubjecttypeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Subjecttype>> GetAllAsync()
    {
        return await _context.Subjecttypes.ToListAsync();
    }

    public async Task<Subjecttype?> GetByIdAsync(int id)
    {
        return await _context.Subjecttypes.FindAsync(id);
    }

    public async Task<Subjecttype> CreateAsync(Subjecttype subjecttype)
    {
        _context.Subjecttypes.Add(subjecttype);
        await _context.SaveChangesAsync();
        return subjecttype;
    }

    public async Task<bool> UpdateAsync(int id, Subjecttype subjecttype)
    {
        if (id != subjecttype.Subjecttypeid)
            return false;

        _context.Entry(subjecttype).State = EntityState.Modified;

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
        var entity = await _context.Subjecttypes.FindAsync(id);
        if (entity == null) return false;

        _context.Subjecttypes.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
