using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherWorkStatusHistoriesController : ControllerBase
    {
        private readonly ITeacherWorkStatusHistoryService _service;
        public TeacherWorkStatusHistoriesController(ITeacherWorkStatusHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacherworkstatushistory>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        // Sửa Guid thành int
        public async Task<ActionResult<Teacherworkstatushistory>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Teacherworkstatushistory>> Create([FromBody] Teacherworkstatushistory history)
        {
            var created = await _service.CreateAsync(history);
            return CreatedAtAction(nameof(GetById), new { id = created.Historyid }, created);
        }

        [HttpPut("{id}")]
        // Sửa Guid thành int
        public async Task<IActionResult> Update(int id, [FromBody] Teacherworkstatushistory history)
        {
            if (id != history.Historyid) return BadRequest();
            var result = await _service.UpdateAsync(id, history);
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