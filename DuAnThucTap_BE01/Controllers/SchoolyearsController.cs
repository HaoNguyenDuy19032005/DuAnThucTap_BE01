using DuAnThucTap.Data;
using DuAnThucTap.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolyearsController : ControllerBase
    {
        private readonly ISchoolyearService _service;
        private readonly ApplicationDbContext _context;

        public SchoolyearsController(ISchoolyearService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchoolyears(
          [FromQuery] string? search,
          [FromQuery] int page = 1,
          [FromQuery] int pageSize = 5)
        {
            var result = await _service.GetAllAsync(search, page, pageSize);

            if (result.Count == 0)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy kết quả nào phù hợp với tìm kiếm.",
                    search = search,
                    page = page
                });
            }

            return Ok(new
            {
                message = "Lấy danh sách năm học thành công",
                currentPage = result.PageIndex,
                totalPages = result.TotalPage,
                data = result
            });
        }



        [HttpPost]
        public async Task<ActionResult<Schoolyear>> PostSchoolyear(Schoolyear schoolyear)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(schoolyear);
                return CreatedAtAction(nameof(GetSchoolyears), new { id = created.Schoolyearid },
                    new { message = "Tạo mới năm học thành công", data = created });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolyear(int id, Schoolyear schoolyear)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (schoolyear.Startyear >= schoolyear.Endyear)
                return BadRequest(new { message = "Năm bắt đầu phải nhỏ hơn năm kết thúc." });

            var schoolInfoExists = await _context.Schoolinformations
                .AnyAsync(s => s.Schoolinfoid == schoolyear.Schoolinfoid);

            if (!schoolInfoExists)
                return BadRequest(new { message = $"Schoolinfoid {schoolyear.Schoolinfoid} không tồn tại." });

            schoolyear.Updatedat = DateTime.Now;
            var success = await _service.UpdateAsync(id, schoolyear);
            if (!success)
                return NotFound(new { message = "Không tìm thấy năm học để cập nhật." });

            return Ok(new { message = "Cập nhật năm học thành công" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolyear(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Không tìm thấy năm học để xoá." });

            return Ok(new { message = "Xoá năm học thành công" });
        }
    }
}
