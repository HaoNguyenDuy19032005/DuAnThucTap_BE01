// Controllers/ClassesController.cs
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Mvc;
using DuAnThucTapNhom3.Iterface;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _service;

        public ClassesController(IClassService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Class>> Create(Class newClass)
        {
            var created = await _service.CreateAsync(newClass);
            return CreatedAtAction(nameof(GetById), new { id = created.Classid }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Class>> Update(int id, Class updatedClass)
        {
            var result = await _service.UpdateAsync(id, updatedClass);
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
