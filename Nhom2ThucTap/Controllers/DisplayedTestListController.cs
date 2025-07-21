using Microsoft.AspNetCore.Mvc;
using Nhom2ThucTap.Interface;
using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisplayedTestListController : ControllerBase
    {
        private readonly IDisplayedTestListService _service;

        public DisplayedTestListController(IDisplayedTestListService service)
        {
            _service = service;
        }

        // GET: api/DisplayedTestList/paged?keyword=abc&page=1&pageSize=10
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] string? keyword, int page = 1, int pageSize = 10)
        {
            var result = await _service.GetAllAsync(keyword, page, pageSize);
            return Ok(new { message = "Lấy danh sách thành công", data = result });
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming(int page = 1, int pageSize = 10)
        {
            var result = await _service.GetUpcomingAsync(page, pageSize);
            return Ok(new { message = "Danh sách bài sắp tới", data = result });
        }

        [HttpGet("finished")]
        public async Task<IActionResult> GetFinished(int page = 1, int pageSize = 10)
        {
            var result = await _service.GetFinishedAsync(page, pageSize);
            return Ok(new { message = "Danh sách bài đã hoàn thành", data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound(new { message = "Không tìm thấy bài thi" });
            return Ok(new { message = "Thành công", data = item });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DisplayedTestList model)
        {
            var errors = ValidateModel(model);
            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            if (!await _service.SubjectExistsAsync(model.SubjectID))
                return NotFound(new { message = $"Không tìm thấy môn học với ID {model.SubjectID}" });

            if (!await _service.TeacherExistsAsync(model.TeacherID))
                return NotFound(new { message = $"Không tìm thấy giáo viên với ID {model.TeacherID}" });

            model.StartTime = DateTime.SpecifyKind(model.StartTime, DateTimeKind.Utc);

            var created = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.DisplayItemID }, new { message = "Tạo thành công", data = created });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DisplayedTestList model)
        {
            var errors = ValidateModel(model);
            if (errors.Any())
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

            if (!await _service.SubjectExistsAsync(model.SubjectID))
                return NotFound(new { message = $"Không tìm thấy môn học với ID {model.SubjectID}" });

            if (!await _service.TeacherExistsAsync(model.TeacherID))
                return NotFound(new { message = $"Không tìm thấy giáo viên với ID {model.TeacherID}" });

            // ✅ Phải ép thời gian về UTC trước khi lưu
            model.StartTime = DateTime.SpecifyKind(model.StartTime, DateTimeKind.Utc);

            var success = await _service.UpdateAsync(id, model);
            if (!success)
                return NotFound(new { message = "Không tìm thấy bài thi để cập nhật" });

            return Ok(new { message = "Cập nhật thành công" });
        }
    

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound(new { message = "Không tìm thấy bài thi để xóa" });

            return Ok(new { message = "Xóa thành công" });
        }

        // 🔍 Hàm kiểm tra lỗi thủ công
        private List<string> ValidateModel(DisplayedTestList model)
        {
            var errors = new List<string>();

            if (model.SubjectID <= 0)
                errors.Add("SubjectID không được để trống hoặc <= 0");

            if (model.TeacherID <= 0)
                errors.Add("TeacherID không được để trống hoặc <= 0");

            if (string.IsNullOrWhiteSpace(model.Title))
                errors.Add("Tiêu đề không được để trống");

            if (model.DurationInMinutes <= 0)
                errors.Add("Thời lượng phải lớn hơn 0");

            if (model.StartTime == default)
                errors.Add("Thời gian bắt đầu không hợp lệ");

            if (model.StartTime < DateTime.UtcNow)
                errors.Add("Thời gian bắt đầu phải lớn hơn thời gian hiện tại");

            return errors;
        }
    }
}
