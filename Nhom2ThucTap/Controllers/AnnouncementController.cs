using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementController(IAnnouncementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 10)
        {
            var (announcements, total) = await _service.GetPagedAsync(page, pageSize);
            var data = announcements.Select(a => new
            {
                a.Announcementid,
                a.Title,
                a.Body,
                a.Targetaudience,
                a.Url,
                a.Createdat,
                a.Creatorid,
                CreatorEmail = a.Creator?.Email
            });

            return Ok(new { TotalCount = total, Data = data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var a = await _service.GetByIdAsync(id);
            if (a == null)
                return NotFound(new { Message = "Không tìm thấy thông báo" });

            return Ok(new
            {
                a.Announcementid,
                a.Title,
                a.Body,
                a.Targetaudience,
                a.Url,
                a.Createdat,
                a.Creatorid,
                CreatorEmail = a.Creator?.Email
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Announcement model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(model.Title))
                return BadRequest(new { Message = "Tiêu đề không được để trống" });

            try
            {
                var (success, message) = await _service.CreateAsync(model);
                if (!success)
                    return BadRequest(new { Message = message });

                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi máy chủ", Detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Announcement model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(model.Title))
                return BadRequest(new { Message = "Tiêu đề không được để trống" });

            // Bảo vệ không cho update người tạo
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Không tìm thấy thông báo" });

            if (model.Creatorid != 0 && model.Creatorid != existing.Creatorid)
                return BadRequest(new { Message = "Không được cập nhật người tạo" });

            try
            {
                var (success, message) = await _service.UpdateAsync(id, model);
                if (!success)
                    return BadRequest(new { Message = message });

                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi máy chủ", Detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { Message = "Không tìm thấy thông báo" });

            try
            {
                var (success, message) = await _service.DeleteAsync(id);
                if (!success)
                    return BadRequest(new { Message = message });

                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi máy chủ", Detail = ex.Message });
            }
        }
    }
}
