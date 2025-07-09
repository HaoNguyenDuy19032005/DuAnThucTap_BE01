using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamGradersController : ControllerBase
    {
        private readonly IExamGraderService _examGraderService;

        public ExamGradersController(IExamGraderService examGraderService)
        {
            _examGraderService = examGraderService;
        }

        // GET: api/ExamGraders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examgrader>>> GetAll()
        {
            var examGraders = await _examGraderService.GetAllAsync();
            return Ok(examGraders);
        }

        // GET: api/ExamGraders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Examgrader>> GetById(int id)
        {
            var examGrader = await _examGraderService.GetByIdAsync(id);
            if (examGrader == null)
            {
                return NotFound();
            }
            return Ok(examGrader);
        }

        // POST: api/ExamGraders
        [HttpPost]
        public async Task<ActionResult<Examgrader>> Create(Examgrader examGrader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdExamGrader = await _examGraderService.CreateAsync(examGrader);
            return CreatedAtAction(nameof(GetById), new { id = createdExamGrader.Examgraderid }, createdExamGrader);
        }

        // PUT: api/ExamGraders/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Examgrader>> Update(int id, Examgrader examGrader)
        {
            if (!ModelState.IsValid || id != examGrader.Examgraderid)
            {
                return BadRequest(ModelState);
            }

            var updatedExamGrader = await _examGraderService.UpdateAsync(id, examGrader);
            if (updatedExamGrader == null)
            {
                return NotFound();
            }
            return Ok(updatedExamGrader);
        }

        // DELETE: api/ExamGraders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _examGraderService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}