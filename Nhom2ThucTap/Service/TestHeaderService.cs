using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Models;
using Microsoft.EntityFrameworkCore;

public class TestHeaderService : ITestHeaderService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public TestHeaderService(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IEnumerable<TestHeader>> GetAllAsync()
    {
        return await _context.TestHeaders.ToListAsync();
    }

    public async Task<TestHeader?> GetByIdAsync(int id)
    {
        return await _context.TestHeaders.FindAsync(id);
    }

    public async Task<TestHeader> CreateAsync(TestHeaderDto dto)
    {
        await ValidateInputAsync(dto);

        var entity = new TestHeader
        {
            SubjectId = dto.SubjectId,
            ClassId = dto.ClassId,
            Title = dto.Title!,
            TestFormat = dto.TestFormat!,
            DurationInMinutes = dto.DurationInMinutes,
            StartTime = EnsureUtc(dto.StartTime),
            RequireStudentAttachment = dto.RequireStudentAttachment,
            SubmissionRules = dto.SubmissionRules,
            AttachmentUrl = await SaveFileAsync(dto.Attachment),
            CreatedAt = DateTime.UtcNow
        };

        _context.TestHeaders.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TestHeader?> UpdateAsync(int id, TestHeaderDto dto)
    {
        var entity = await _context.TestHeaders.FindAsync(id);
        if (entity == null) return null;

        await ValidateInputAsync(dto);

        entity.SubjectId = dto.SubjectId;
        entity.ClassId = dto.ClassId;
        entity.Title = dto.Title!;
        entity.TestFormat = dto.TestFormat!;
        entity.DurationInMinutes = dto.DurationInMinutes;
        entity.StartTime = EnsureUtc(dto.StartTime);
        entity.RequireStudentAttachment = dto.RequireStudentAttachment;
        entity.SubmissionRules = dto.SubmissionRules;
        entity.UpdatedAt = DateTime.UtcNow;

        if (dto.Attachment != null)
        {
            entity.AttachmentUrl = await SaveFileAsync(dto.Attachment);
        }

        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.TestHeaders.FindAsync(id);
        if (entity == null) return false;

        _context.TestHeaders.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateInputAsync(TestHeaderDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("Tiêu đề không được để trống.");

        if (!await _context.Subjects.AnyAsync(s => s.Subjectid == dto.SubjectId))
            throw new ArgumentException("Môn học không tồn tại.");

        if (!await _context.Classes.AnyAsync(c => c.Classid == dto.ClassId))
            throw new ArgumentException("Lớp học không tồn tại.");
    }

    private async Task<string?> SaveFileAsync(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return null;

        var uploads = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploads))
            Directory.CreateDirectory(uploads);

        var filename = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filepath = Path.Combine(uploads, filename);

        using var stream = new FileStream(filepath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/{filename}";
    }

    private DateTime EnsureUtc(DateTime dateTime)
    {
        return dateTime.Kind switch
        {
            DateTimeKind.Utc => dateTime,
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
            _ => dateTime
        };
    }
}
