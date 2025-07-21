using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Models;

[ApiController]
[Route("api/[controller]")]
public class TestStudentAnswerController : ControllerBase
{
    private readonly ITestStudentAnswerService _service;
    private readonly AppDbContext _context;

    public TestStudentAnswerController(ITestStudentAnswerService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest(new { message = "ID không hợp lệ." });

        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound(new { message = "Không tìm thấy câu trả lời." });

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TestStudentAnswerDto dto)
    {
        try
        {
            // Kiểm tra dữ liệu đầu vào
            var hasSelectedOption = !string.IsNullOrWhiteSpace(dto.SelectedOption);
            var hasAnswerContent = !string.IsNullOrWhiteSpace(dto.AnswerContent);

            if (!hasSelectedOption && !hasAnswerContent)
                return BadRequest(new { message = "Bạn phải nhập câu trả lời: trắc nghiệm hoặc tự luận." });

            if (hasSelectedOption)
            {
                if (dto.SelectedOption!.Length != 1 || !char.IsLetter(dto.SelectedOption[0]))
                    return BadRequest(new { message = "Câu trả lời trắc nghiệm chỉ được phép 1 ký tự từ A-Z." });
            }

            if (hasAnswerContent)
            {
                if (dto.AnswerContent!.Length < 10)
                    return BadRequest(new { message = "Câu trả lời tự luận phải có ít nhất 10 ký tự." });
            }

            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.InnerException?.Message ?? ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] TestStudentAnswerDto dto)
    {
        if (id <= 0)
            return BadRequest(new { message = "ID không hợp lệ." });

        try
        {
            var hasSelectedOption = !string.IsNullOrWhiteSpace(dto.SelectedOption);
            var hasAnswerContent = !string.IsNullOrWhiteSpace(dto.AnswerContent);

            if (!hasSelectedOption && !hasAnswerContent)
                return BadRequest(new { message = "Bạn phải nhập câu trả lời: trắc nghiệm hoặc tự luận." });

            if (hasSelectedOption)
            {
                if (dto.SelectedOption!.Length != 1 || !char.IsLetter(dto.SelectedOption[0]))
                    return BadRequest(new { message = "Câu trả lời trắc nghiệm chỉ được phép 1 ký tự từ A-Z." });
            }

            if (hasAnswerContent)
            {
                if (dto.AnswerContent!.Length < 10)
                    return BadRequest(new { message = "Câu trả lời tự luận phải có ít nhất 10 ký tự." });
            }

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { message = "Không tìm thấy câu trả lời để cập nhật." });

            return Ok(updated);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi máy chủ", detail = ex.InnerException?.Message ?? ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest(new { message = "ID không hợp lệ." });

        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "Không tìm thấy câu trả lời để xoá." });

        return Ok(new { message = "Xoá thành công." });
    }
}
