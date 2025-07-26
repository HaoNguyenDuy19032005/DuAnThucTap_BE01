using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _service;

        public ExamsController(IExamService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetById(int id)
        {
            var exam = await _service.GetByIdAsync(id);
            return exam == null ? NotFound() : Ok(exam);
        }

        [HttpPost]
        public async Task<ActionResult<Exam>> Create([FromBody] Exam exam)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(exam);
            return CreatedAtAction(nameof(GetById), new { id = created.Examid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Exam exam)
        {
            if (id != exam.Examid) return BadRequest("ID không khớp");
            var result = await _service.UpdateAsync(id, exam);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return !success ? NotFound() : NoContent();
        }
    }
}