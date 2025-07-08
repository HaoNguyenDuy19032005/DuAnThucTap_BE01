using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SubjectTypesController : ControllerBase
    {
        private readonly ISubjectTypeService _service;

        public SubjectTypesController(ISubjectTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Subjecttype model)
        {
            var created = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(Get), new { id = created.Subjecttypeid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Subjecttype model)
        {
            var updated = await _service.UpdateAsync(id, model);
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
