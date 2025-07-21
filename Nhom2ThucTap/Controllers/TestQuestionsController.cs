using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom2ThucTap.Data;
using Nhom2ThucTap.DTO;
using Nhom2ThucTap.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhom2ThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestQuestionsController : ControllerBase
    {
        private readonly ITestQuestionService _service;
        private readonly AppDbContext _context;

        public TestQuestionsController(ITestQuestionService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        private List<string> ValidateDto(TestQuestionItemDto dto)
        {
            var errors = new List<string>();
            if (dto == null)
            {
                errors.Add("Dữ liệu gửi lên không hợp lệ hoặc bị để trống.");
                return errors;
            }
            if (dto.TestId <= 0)
                errors.Add("TestId không được để trống hoặc phải lớn hơn 0.");
            if (dto.DisplayOrder <= 0)
                errors.Add("Thứ tự hiển thị (DisplayOrder) phải lớn hơn 0.");
            if (string.IsNullOrWhiteSpace(dto.Content))
                errors.Add("Nội dung câu hỏi không được để trống.");
            return errors;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var questions = await _service.GetAllAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _service.GetByIdAsync(id);
            if (question == null)
                return NotFound(new { Loi = new[] { "Không tìm thấy câu hỏi với ID này." } });
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestQuestionItemDto dto)
        {
            var errors = ValidateDto(dto);
            if (errors.Any())
                return BadRequest(new { Loi = errors });

            bool testExists = await _context.TestHeaders.AnyAsync(t => t.TestId == dto.TestId);
            if (!testExists)
                return NotFound(new { Loi = new[] { "Không tìm thấy TestId tương ứng." } });

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.QuestionId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TestQuestionItemDto dto)
        {
            var errors = ValidateDto(dto);
            if (errors.Any())
                return BadRequest(new { Loi = errors });

            bool testExists = await _context.TestHeaders.AnyAsync(t => t.TestId == dto.TestId);
            if (!testExists)
                return NotFound(new { Loi = new[] { "Không tìm thấy TestId tương ứng." } });

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new { Loi = new[] { "Không tìm thấy câu hỏi với ID này." } });

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Loi = new[] { "Không tìm thấy câu hỏi với ID này." } });

            return NoContent();
        }
    }
}
