using DuAnThucTap.Data;
using DuAnThucTap.Model;
using Microsoft.EntityFrameworkCore;

public class ClassService : IClassService
{
    private readonly ApplicationDbContext _context;

    public ClassService(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task<List<string>> ValidateForeignKeys(CreateClassDto dto)
    {
        var errors = new List<string>();

        if (dto.Schoolyearid.HasValue && !await _context.Schoolyears.AnyAsync(s => s.Schoolyearid == dto.Schoolyearid))
            errors.Add($"Schoolyearid {dto.Schoolyearid} không tồn tại.");

        if (dto.Gradelevelid.HasValue && !await _context.Gradelevels.AnyAsync(g => g.Gradelevelid == dto.Gradelevelid))
            errors.Add($"Gradelevelid {dto.Gradelevelid} không tồn tại.");

        if (dto.Classtypeid.HasValue && !await _context.Classtypes.AnyAsync(c => c.Classtypeid == dto.Classtypeid))
            errors.Add($"Classtypeid {dto.Classtypeid} không tồn tại.");

        if (dto.Teacherid.HasValue && !await _context.Teachers.AnyAsync(t => t.Teacherid == dto.Teacherid))
            errors.Add($"Teacherid {dto.Teacherid} không tồn tại.");

        if (dto.SubjectIds != null && dto.SubjectIds.Any())
        {
            foreach (var subjectId in dto.SubjectIds)
            {
                if (!await _context.Subjects.AnyAsync(s => s.Subjectid == subjectId))
                    errors.Add($"Subjectid {subjectId} không tồn tại.");
            }
        }

        return errors;
    }

    public async Task<PaginatedList<Class>> GetAllAsync(string? search, int pageIndex, int pageSize)
    {
        var query = _context.Classes
            .Include(c => c.ClassSubjects)
                .ThenInclude(cs => cs.Subject)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => c.Classname.Contains(search));
        }

        query = query.OrderByDescending(c => c.Createdat);

        return await PaginatedList<Class>.CreateAsync(query, pageIndex, pageSize);
    }

    public async Task<Class?> GetByIdAsync(int id)
    {
        return await _context.Classes
            .Include(c => c.ClassSubjects)
                .ThenInclude(cs => cs.Subject)
            .FirstOrDefaultAsync(c => c.Classid == id);
    }

    public async Task<Class> CreateAsync(CreateClassDto dto)
    {
        var errors = await ValidateForeignKeys(dto);
        if (errors.Any())
            throw new ArgumentException(string.Join(" | ", errors));

        if (dto.Classtypeid.HasValue)
        {
            var classtype = await _context.Classtypes
                .FirstOrDefaultAsync(c => c.Classtypeid == dto.Classtypeid);

            if (classtype == null)
                throw new ArgumentException($"Classtypeid {dto.Classtypeid} không tồn tại.");

            if (!classtype.Isactive)
                throw new ArgumentException($"Loại lớp (Classtypeid {dto.Classtypeid}) đang bị vô hiệu hoá, không thể tạo lớp.");
        }


        if (dto.Maxstudents < 30 || dto.Maxstudents > 45)
            throw new ArgumentException("Số lượng học sinh phải nằm trong khoảng từ 30 đến 45.");

        var @class = new Class
        {
            Classname = dto.Classname,
            Maxstudents = dto.Maxstudents,
            Description = dto.Description,
            Schoolyearid = dto.Schoolyearid,
            Gradelevelid = dto.Gradelevelid,
            Classtypeid = dto.Classtypeid,
            Teacherid = dto.Teacherid,
            Createdat = DateTime.UtcNow,
            Updatedat = DateTime.UtcNow,
            ClassSubjects = dto.SubjectIds?.Select(sid => new ClassSubject
            {
                Subjectid = sid
            }).ToList()
        };

        _context.Classes.Add(@class);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(@class.Classid);
    }


    public async Task<Class?> UpdateAsync(int id, CreateClassDto dto)
    {
        if (dto.Classtypeid.HasValue)
        {
            var classtype = await _context.Classtypes
                .FirstOrDefaultAsync(c => c.Classtypeid == dto.Classtypeid);

            if (classtype == null)
                throw new ArgumentException($"Classtypeid {dto.Classtypeid} không tồn tại.");

            if (!classtype.Isactive)
                throw new ArgumentException($"Loại lớp (Classtypeid {dto.Classtypeid}) đang bị vô hiệu hoá, không thể cập nhật lớp.");
        }

        var @class = await _context.Classes
            .Include(c => c.ClassSubjects)
            .FirstOrDefaultAsync(c => c.Classid == id);

        if (@class == null) return null;

        var errors = await ValidateForeignKeys(dto);
        if (errors.Any())
            throw new ArgumentException(string.Join(" | ", errors));

        if (dto.Maxstudents < 30 || dto.Maxstudents > 45)
            throw new ArgumentException("Số lượng học sinh phải nằm trong khoảng từ 30 đến 45.");

        @class.Classname = dto.Classname;
        @class.Maxstudents = dto.Maxstudents;
        @class.Description = dto.Description;
        @class.Schoolyearid = dto.Schoolyearid;
        @class.Gradelevelid = dto.Gradelevelid;
        @class.Classtypeid = dto.Classtypeid;
        @class.Teacherid = dto.Teacherid;
        @class.Updatedat = DateTime.UtcNow;

        _context.ClassSubjects.RemoveRange(@class.ClassSubjects);

        @class.ClassSubjects = dto.SubjectIds?.Select(subjectId => new ClassSubject
        {
            Classid = @class.Classid,
            Subjectid = subjectId
        }).ToList() ?? new List<ClassSubject>();

        await _context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }



    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Classes
            .Include(c => c.ClassSubjects)
            .FirstOrDefaultAsync(c => c.Classid == id);

        if (entity == null) return false;

        _context.ClassSubjects.RemoveRange(entity.ClassSubjects); // xóa liên kết
        _context.Classes.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }
}
