using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamGradersController : ControllerBase
    {
        private readonly IExamGraderService _service;

        // Fix for CS0111: Removed duplicate constructor
        public ExamGradersController(IExamGraderService service)
        {
            // Fix for CS8618: Ensuring '_service' is initialized
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examgrader>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Examgrader>> GetById(int id)
        {
            var examGrader = await _service.GetByIdAsync(id);
            return examGrader == null ? NotFound() : Ok(examGrader);
        }

        [HttpPost]
        public async Task<ActionResult<Examgrader>> Create([FromBody] Examgrader examGrader)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(examGrader);
            return CreatedAtAction(nameof(GetById), new { id = created.Examgraderid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Examgrader examGrader)
        {
            if (id != examGrader.Examgraderid) return BadRequest("ID không khớp");
            var result = await _service.UpdateAsync(id, examGrader);
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