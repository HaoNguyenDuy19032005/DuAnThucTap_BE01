using DuAnThucTap.DTOs;
using DuAnThucTap.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradetypesController : ControllerBase
    {
        private readonly IGradetypeService _service;

        public GradetypesController(IGradetypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
      [FromQuery] string? search,
      [FromQuery] int page = 1,
      [FromQuery] int pageSize = 5)
        {
            var result = await _service.GetAllAsync(search, page, pageSize);

            if (result.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Không tìm thấy loại điểm nào phù hợp.",
                    search,
                    page
                });
            }

            return Ok(new
            {
                success = true,
                message = "Lấy danh sách loại điểm thành công.",
                currentPage = result.PageIndex,
                totalPages = result.TotalPage,
                data = result
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null
                ? NotFound(new { success = false, message = "Không tìm thấy loại điểm." })
                : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GradetypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                return BadRequest(new
                {
                    success = false,
                    message = "Dữ liệu không hợp lệ!",
                    errors
                });
            }

            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GradetypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                return BadRequest(new
                {
                    success = false,
                    message = "Dữ liệu không hợp lệ!",
                    errors
                });
            }

            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.DeleteAsync(id));
    }
}
