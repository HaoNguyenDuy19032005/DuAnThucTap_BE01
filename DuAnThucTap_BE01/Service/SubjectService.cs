using DuAnThucTap.Data;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SubjectService : ISubjectService
{
    private readonly ApplicationDbContext _context;

    public SubjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Subject>> GetAllAsync(string? search, int pageIndex, int pageSize)
    {
        var query = _context.Subjects
            .Include(s => s.Department)
            .Include(s => s.Subjecttype)
            .Include(s => s.Schoolyear)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(s => s.Subjectname.Contains(search));
        }

        query = query.OrderByDescending(s => s.Createdat);

        return await PaginatedList<Subject>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task<Subject?> GetByIdAsync(int id)
    {
        return await _context.Subjects
            .Include(s => s.Department)
            .Include(s => s.Subjecttype)
            .Include(s => s.Schoolyear)
            .FirstOrDefaultAsync(s => s.Subjectid == id);
    }

    public async Task<Subject> CreateAsync(Subject subject)
    {
        await ValidateForeignKeys(subject);
        subject.Createdat = DateTime.UtcNow;
        subject.Updatedat = DateTime.UtcNow;

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        return subject;
    }

    public async Task<bool> UpdateAsync(int id, Subject subject)
    {
        if (id != subject.Subjectid)
            return false;

        var existing = await _context.Subjects.FindAsync(id);
        if (existing == null)
            return false;

        await ValidateForeignKeys(subject);

        existing.Subjectname = subject.Subjectname;
        existing.Subjectcode = subject.Subjectcode;
        existing.Defaultperiodssem1 = subject.Defaultperiodssem1;
        existing.Defaultperiodssem2 = subject.Defaultperiodssem2;
        existing.Departmentid = subject.Departmentid;
        existing.Subjecttypeid = subject.Subjecttypeid;
        existing.Schoolyearid = subject.Schoolyearid;
        existing.Updatedat = DateTime.UtcNow;

        _context.Subjects.Update(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        if (subject == null)
            return false;

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateForeignKeys(Subject subject)
    {
        if (!await _context.Departments.AnyAsync(d => d.Departmentid == subject.Departmentid))
            throw new ArgumentException("Không tồn tại Khoa với ID được cung cấp.");

        if (!await _context.Subjecttypes.AnyAsync(st => st.Subjecttypeid == subject.Subjecttypeid))
            throw new ArgumentException("Không tồn tại Loại môn học với ID được cung cấp.");

        if (!await _context.Schoolyears.AnyAsync(sy => sy.Schoolyearid == subject.Schoolyearid))
            throw new ArgumentException("Không tồn tại Năm học với ID được cung cấp.");
    }
}
