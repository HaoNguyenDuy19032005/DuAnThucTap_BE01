using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamSchedulesController : ControllerBase
    {
        private readonly IExamScheduleService _service;

        public ExamSchedulesController(IExamScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examschedule>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Examschedule>> GetById(int id)
        {
            var examSchedule = await _service.GetByIdAsync(id);
            return examSchedule == null ? NotFound() : Ok(examSchedule);
        }

        [HttpPost]
        public async Task<ActionResult<Examschedule>> Create([FromBody] Examschedule examSchedule)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _service.CreateAsync(examSchedule);
            return CreatedAtAction(nameof(GetById), new { id = created.Examscheduleid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Examschedule examSchedule)
        {
            if (id != examSchedule.Examscheduleid) return BadRequest("ID không khớp");
            var result = await _service.UpdateAsync(id, examSchedule);
            return result == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return !success ? NotFound() : NoContent();
        }
    }
}