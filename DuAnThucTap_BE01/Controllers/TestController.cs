using DuAnThucTap_BE01.Iterface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITests _service;

        public TestController(ITests service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tests = await _service.GetAllAsync();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var test = await _service.GetByIdAsync(id);
            return test == null ? NotFound() : Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tests test)
        {
            var created = await _service.CreateAsync(test);
            return CreatedAtAction(nameof(Get), new { id = created.TestId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tests test)
        {
            var updated = await _service.UpdateAsync(id, test);
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