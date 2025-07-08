using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectsController(ISubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await _service.GetAllAsync();
            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var subject = await _service.GetByIdAsync(id);
            return subject == null ? NotFound() : Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Subject subject)
        {
            var created = await _service.CreateAsync(subject);
            return CreatedAtAction(nameof(Get), new { id = created.Subjectid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Subject subject)
        {
            var updated = await _service.UpdateAsync(id, subject);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
