using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Models;

public class TestStudentAnswerService : ITestStudentAnswerService
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public TestStudentAnswerService(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IEnumerable<TestStudentAnswer>> GetAllAsync()
    {
        return await _context.TestStudentAnswers.ToListAsync();
    }

    public async Task<TestStudentAnswer?> GetByIdAsync(int id)
    {
        return await _context.TestStudentAnswers.FindAsync(id);
    }

    public async Task<TestStudentAnswer> CreateAsync(TestStudentAnswerDto dto)
    {
        await ValidateAsync(dto);

        var answer = new TestStudentAnswer
        {
            SubmissionId = dto.SubmissionId,
            QuestionId = dto.QuestionId,
            SelectedOption = dto.SelectedOption,
            AnswerContent = dto.AnswerContent,
            AnswerAttachmentUrl = await SaveFileAsync(dto.Attachment),
            CreatedAt = DateTime.UtcNow
        };

        _context.TestStudentAnswers.Add(answer);
        await _context.SaveChangesAsync();

        return answer;
    }

    public async Task<TestStudentAnswer?> UpdateAsync(int id, TestStudentAnswerDto dto)
    {
        var existing = await _context.TestStudentAnswers.FindAsync(id);
        if (existing == null) return null;

        await ValidateAsync(dto);

        existing.SubmissionId = dto.SubmissionId;
        existing.QuestionId = dto.QuestionId;
        existing.SelectedOption = dto.SelectedOption;
        existing.AnswerContent = dto.AnswerContent;
        existing.UpdatedAt = DateTime.UtcNow;

        if (dto.Attachment != null)
        {
            existing.AnswerAttachmentUrl = await SaveFileAsync(dto.Attachment);
        }

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.TestStudentAnswers.FindAsync(id);
        if (item == null) return false;

        _context.TestStudentAnswers.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task ValidateAsync(TestStudentAnswerDto dto)
    {
        var errors = new List<string>();

        if (dto.SubmissionId <= 0 || !await _context.TestStudentSubmissions.AnyAsync(x => x.SubmissionId == dto.SubmissionId))
            errors.Add("ID bài nộp không hợp lệ hoặc không tồn tại.");

        if (dto.QuestionId != null && !await _context.TestQuestionItems.AnyAsync(x => x.QuestionId == dto.QuestionId))
            errors.Add("Câu hỏi không tồn tại.");

        // ✅ Không kiểm tra định dạng file hay dung lượng file nữa

        if (errors.Any())
            throw new ArgumentException(string.Join(" | ", errors));
    }

    private async Task<string?> SaveFileAsync(IFormFile? file)
    {
        if (file == null || file.Length == 0) return null;

        var folder = Path.Combine(_env.WebRootPath, "answer_uploads");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        var filename = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var path = Path.Combine(folder, filename);

        using var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/answer_uploads/{filename}";
    }
}
