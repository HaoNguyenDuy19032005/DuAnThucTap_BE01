using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using DuAnThucTap_BE01.Interface;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _service;

        public ExamsController(IExamService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Exam>> Create(Exam newExam)
        {
            if (newExam == null) return BadRequest();
            var created = await _service.CreateAsync(newExam);
            return CreatedAtAction(nameof(GetById), new { id = created.ExamID }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Exam>> Update(int id, Exam updatedExam)
        {
            if (updatedExam == null || id != updatedExam.ExamID) return BadRequest();
            var result = await _service.UpdateAsync(id, updatedExam);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}