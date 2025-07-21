using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsubjectsummaryController : ControllerBase
    {
        private readonly IStudentsubjectsummaryService _service;

        public StudentsubjectsummaryController(IStudentsubjectsummaryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (list, total) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new
            {
                TotalCount = total,
                Page = page,
                PageSize = pageSize,
                Data = list
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _service.GetByIdAsync(id);
            if (record == null)
            {
                return NotFound(new { message = "Không tìm thấy bản ghi." });
            }
            return Ok(record);
        }

    //    [HttpPost]
    //    public async Task<IActionResult> Create([FromBody] Studentsubjectsummary model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        model.Averagescore = null; // Trigger sẽ tính

    //        try
    //        {
    //            await _service.AddAsync(model);
    //            return Ok(new { message = "Tạo bản ghi thành công. Điểm sẽ được tính tự động." });
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
    //        }
    //    }

    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> Update(int id, [FromBody] Studentsubjectsummary model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        var existing = await _service.GetByIdAsync(id);
    //        if (existing == null)
    //        {
    //            return NotFound(new { message = "Không tìm thấy bản ghi để cập nhật." });
    //        }

    //        model.Averagescore = null; // Không cho chỉnh điểm bằng tay

    //        try
    //        {
    //            await _service.UpdateAsync(id, model);
    //            return Ok(new { message = "Cập nhật bản ghi thành công." });
    //        }
    //        catch (Exception ex)
    //        {
    //            return BadRequest(new { message = "Đã xảy ra lỗi: " + ex.Message });
    //        }
    //    }

    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> Delete(int id)
    //    {
    //        var existing = await _service.GetByIdAsync(id);
    //        if (existing == null)
    //        {
    //            return NotFound(new { message = "Không tìm thấy bản ghi để xoá." });
    //        }

    //        await _service.DeleteAsync(id);
    //        return Ok(new { message = "Xoá bản ghi thành công." });
    //    }
    }
}
