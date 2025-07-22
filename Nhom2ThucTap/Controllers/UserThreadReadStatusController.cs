using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Models;
using Nhom2ThucTap.Services;

namespace Nhom2ThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserThreadReadStatusController : ControllerBase
    {
        private readonly IUserThreadReadStatusService _service;

        public UserThreadReadStatusController(IUserThreadReadStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data.Select(d => new
            {
                d.Userid,
                UserEmail = d.User?.Email,
                d.Threadid,
                ThreadTitle = d.Thread?.Title,
                d.Lastreadtimestamp
            }));
        }

        [HttpGet("{userId}/{threadId}")]
        public async Task<IActionResult> GetById(int userId, int threadId)
        {
            var record = await _service.GetByIdAsync(userId, threadId);
            if (record == null)
                return NotFound(new { Message = "Không tìm thấy bản ghi" });

            return Ok(new
            {
                record.Userid,
                UserEmail = record.User?.Email,
                record.Threadid,
                ThreadTitle = record.Thread?.Title,
                record.Lastreadtimestamp
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Userthreadreadstatus entity)
        {
            var (isSuccess, message) = await _service.CreateAsync(entity);
            if (!isSuccess)
                return BadRequest(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpPut("{userId}/{threadId}")]
        public async Task<IActionResult> Update(int userId, int threadId, [FromQuery] DateTime? lastRead = null)
        {
            var (isSuccess, message) = await _service.UpdateAsync(userId, threadId, lastRead);
            if (!isSuccess)
                return NotFound(new { Message = message });

            return Ok(new { Message = message });
        }

        [HttpDelete("{userId}/{threadId}")]
        public async Task<IActionResult> Delete(int userId, int threadId)
        {
            var (isSuccess, message) = await _service.DeleteAsync(userId, threadId);
            if (!isSuccess)
                return NotFound(new { Message = message });

            return Ok(new { Message = message });
        }
    }
}
