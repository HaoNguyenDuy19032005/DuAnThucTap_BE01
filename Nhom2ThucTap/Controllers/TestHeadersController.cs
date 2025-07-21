using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Services;

[ApiController]
[Route("api/[controller]")]
public class TestHeaderController : ControllerBase
{
    private readonly ITestHeaderService _service;
    private readonly AppDbContext _context;

    public TestHeaderController(ITestHeaderService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound(new { message = "Không tìm thấy đề thi." });

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TestHeaderDto dto)
    {
        // Kiểm tra thủ công
        if (dto.SubjectId <= 0)
            return BadRequest(new { message = "Mã môn học không hợp lệ." });

        if (dto.ClassId <= 0)
            return BadRequest(new { message = "Mã lớp học không hợp lệ." });

        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest(new { message = "Tiêu đề không được để trống." });

        if (!await _context.Subjects.AnyAsync(s => s.Subjectid == dto.SubjectId))
            return BadRequest(new { message = "Môn học không tồn tại." });

        if (!await _context.Classes.AnyAsync(c => c.Classid == dto.ClassId))
            return BadRequest(new { message = "Lớp học không tồn tại." });

        try
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Lỗi máy chủ",
                detail = ex.InnerException?.Message ?? ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] TestHeaderDto dto)
    {
        if (dto.SubjectId <= 0)
            return BadRequest(new { message = "Mã môn học không hợp lệ." });

        if (dto.ClassId <= 0)
            return BadRequest(new { message = "Mã lớp học không hợp lệ." });

        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest(new { message = "Tiêu đề không được để trống." });

        if (!await _context.Subjects.AnyAsync(s => s.Subjectid == dto.SubjectId))
            return BadRequest(new { message = "Môn học không tồn tại." });

        if (!await _context.Classes.AnyAsync(c => c.Classid == dto.ClassId))
            return BadRequest(new { message = "Lớp học không tồn tại." });

        try
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy đề thi để cập nhật." });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Lỗi máy chủ",
                detail = ex.InnerException?.Message ?? ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByIdAsync(id);
        if (existing == null)
            return NotFound(new { message = "Không tìm thấy đề thi để xoá." });

        var success = await _service.DeleteAsync(id);
        return Ok(new { message = "Xoá đề thi thành công." });
    }
}
