// Controllers/GradeLevelsController.cs
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Mvc;
using DuAnThucTapNhom3.Iterface;
using System.Diagnostics;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeLevelsController : ControllerBase
    {
        private readonly IGradeLevelService _service;

        public GradeLevelsController(IGradeLevelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gradelevel>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Gradelevel>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Gradelevel>> Create(Gradelevel gradeLevel)
        {
            var created = await _service.CreateAsync(gradeLevel);
            return CreatedAtAction(nameof(GetById), new { id = created.Gradelevelid }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Gradelevel>> Update(int id, Gradelevel gradeLevel)
        {
            var updated = await _service.UpdateAsync(id, gradeLevel);
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
