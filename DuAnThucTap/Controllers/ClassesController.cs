using DuAnThucTap.Model;
using DuAnThucTap.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _service;

    public ClassesController(IClassService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetClasses(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5)
    {
        var result = await _service.GetAllAsync(search, page, pageSize);

        if (result.Count == 0)
        {
            return NotFound(new
            {
                message = "Không tìm thấy lớp học nào phù hợp.",
                search = search,
                page = page
            });
        }

        return Ok(new
        {
            message = "Lấy danh sách lớp học thành công",
            currentPage = result.PageIndex,
            totalPages = result.TotalPage,
            data = result
        });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetClass(int id)
    {
        var data = await _service.GetByIdAsync(id);
        if (data == null)
            return NotFound(new { message = "Không tìm thấy lớp học." });

        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> PostClass([FromBody] CreateClassDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetClass), new { id = created.Classid },
                new { message = "Tạo lớp học thành công", data = created });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutClass(int id, [FromBody] CreateClassDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound(new { message = "Không tìm thấy lớp học để cập nhật." });

            return Ok(new
            {
                message = "Cập nhật lớp học thành công",
                data = updated // trả về luôn object mới để client hiển thị
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
            return NotFound(new { message = "Không tìm thấy lớp học để xoá." });

        return Ok(new { message = "Xoá lớp học thành công" });
    }
}
