// Controllers/ClassTypesController.cs
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Mvc;
using DuAnThucTapNhom3.Iterface;

namespace DuAnThucTapNhom3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassTypesController : ControllerBase
    {
        private readonly IClassTypeService _service;

        public ClassTypesController(IClassTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classtype>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classtype>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Classtype>> Create(Classtype classType)
        {
            var created = await _service.CreateAsync(classType);
            return CreatedAtAction(nameof(GetById), new { id = created.Classtypeid }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Classtype>> Update(int id, Classtype classType)
        {
            var updated = await _service.UpdateAsync(id, classType);
            if (updated == null) return NotFound();
            return Ok(updated);
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
