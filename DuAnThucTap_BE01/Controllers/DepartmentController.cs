using Microsoft.AspNetCore.Mvc;
using DuAnThucTap.Model;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> Create(Department newDepartment)
        {
            if (newDepartment == null) return BadRequest();
            var created = await _service.CreateAsync(newDepartment);
            return CreatedAtAction(nameof(GetById), new { id = created.Departmentid }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> Update(int id, Department updatedDepartment)
        {
            if (updatedDepartment == null || id != updatedDepartment.Departmentid) return BadRequest();
            var result = await _service.UpdateAsync(id, updatedDepartment);
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
