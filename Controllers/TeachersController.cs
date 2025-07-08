using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _service;

        public TeachersController(ITeacherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _service.GetAllAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var teacher = await _service.GetByIdAsync(id);
            return teacher == null ? NotFound() : Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Teacher teacher)
        {
            var created = await _service.CreateAsync(teacher);
            return CreatedAtAction(nameof(Get), new { id = created.Teacherid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Teacher teacher)
        {
            var updated = await _service.UpdateAsync(id, teacher);
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
