using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap_BE01.Controllers
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
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        // Sửa Guid thành int
        public async Task<ActionResult<Teacher>> GetById(int id)
        {
            var teacher = await _service.GetByIdAsync(id);
            return teacher == null ? NotFound() : Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> Create([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(teacher);
            return CreatedAtAction(nameof(GetById), new { id = created.Teacherid }, created);
        }

        [HttpPut("{id}")]
        // Sửa Guid thành int
        public async Task<IActionResult> Update(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.Teacherid) return BadRequest("ID không khớp");
            var result = await _service.UpdateAsync(id, teacher);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        // Sửa Guid thành int
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return !success ? NotFound() : NoContent();
        }
    }
}