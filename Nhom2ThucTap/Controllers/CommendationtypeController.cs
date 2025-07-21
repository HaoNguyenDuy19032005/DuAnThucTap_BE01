using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;
using System;
using System.Threading.Tasks;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommendationtypeController : ControllerBase
    {
        private readonly ICommendationtypeService _commendationtypeService;
        private readonly ILogger<CommendationtypeController> _logger;

        public CommendationtypeController(ICommendationtypeService service, ILogger<CommendationtypeController> logger)
        {
            _commendationtypeService = service;
            _logger = logger;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "page và pageSize phải lớn hơn 0" });

            try
            {
                var (items, total) = await _commendationtypeService.GetPagedAsync(page, pageSize);
                return Ok(new
                {
                    message = "Lấy danh sách phân trang thành công",
                    data = items,
                    totalItems = total,
                    currentPage = page,
                    pageSize,
                    totalPages = (int)Math.Ceiling((double)total / pageSize)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi phân trang loại khen thưởng.");
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var types = await _commendationtypeService.GetAllAsync();
                return Ok(new { message = "Lấy danh sách thành công", data = types });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách loại khen thưởng.");
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var type = await _commendationtypeService.GetByIdAsync(id);
                if (type == null)
                    return NotFound(new { message = "Không tìm thấy loại khen thưởng." });

                return Ok(new { message = "Lấy bản ghi thành công", data = type });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi lấy loại khen thưởng ID {id}.");
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commendationtype commendationtype)
        {
            if (commendationtype == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _commendationtypeService.AddAsync(commendationtype);
                _logger.LogInformation("Tạo loại khen thưởng: {Id} - {Name}", created.Commendationtypeid, created.Typename);
                return Ok(new { message = "Tạo bản ghi thành công", data = created });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Lỗi CSDL khi thêm loại khen thưởng.");
                return StatusCode(400, $"Không thể thêm: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm loại khen thưởng.");
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Commendationtype commendationtype)
        {
            if (commendationtype == null || !ModelState.IsValid || id != commendationtype.Commendationtypeid)
                return BadRequest(new { message = "ID không khớp hoặc dữ liệu không hợp lệ." });

            try
            {
                var updated = await _commendationtypeService.UpdateAsync(id, commendationtype);
                if (updated == null)
                    return NotFound(new { message = "Không tìm thấy bản ghi." });

                return Ok(new { message = "Cập nhật thành công", data = updated });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Lỗi CSDL khi cập nhật ID {id}.");
                return StatusCode(400, $"Không thể cập nhật: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi cập nhật ID {id}.");
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _commendationtypeService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { message = "Không tìm thấy bản ghi." });

                return Ok(new { message = "Xoá bản ghi thành công" });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Lỗi CSDL khi xoá ID {id}.");
                return StatusCode(400, new { message = "Không thể xoá do liên quan dữ liệu." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi khi xoá ID {id}.");
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }
    }
}
