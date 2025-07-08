using DuAnThucTap_BE01.Interface; // Sửa "Iterface" thành "Interface" nếu cần
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAssignmentController : ControllerBase
    {
        private readonly ITestAssignment _service;

        public TestAssignmentController(ITestAssignment service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var testAssignments = await _service.GetAllAsync();
            return Ok(testAssignments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var testAssignment = await _service.GetByIdAsync(id);
            return testAssignment == null ? NotFound() : Ok(testAssignment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Testassignment testAssignment)
        {
            var created = await _service.CreateAsync(testAssignment);
            return CreatedAtAction(nameof(Get), new { id = created.Assignmentid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Testassignment testAssignment)
        {
            var updated = await _service.UpdateAsync(id, testAssignment);
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