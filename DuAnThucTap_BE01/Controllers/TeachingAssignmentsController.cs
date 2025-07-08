using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachingAssignmentsController : ControllerBase
    {
        private readonly ITeachingAssignmentService _service;

        public TeachingAssignmentsController(ITeachingAssignmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teachingassignment>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teachingassignment>> GetById(int id)
        {
            var assignment = await _service.GetByIdAsync(id);
            return assignment == null ? NotFound() : Ok(assignment);
        }

        [HttpPost]
        public async Task<ActionResult<Teachingassignment>> Create([FromBody] Teachingassignment teachingAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                    Message = "Dữ liệu đầu vào không hợp lệ."
                });
            }

            try
            {
                var created = await _service.CreateAsync(teachingAssignment);
                return CreatedAtAction(nameof(GetById), new { id = created.Assignmentid }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Teachingassignment teachingAssignment)
        {
            if (id != teachingAssignment.Assignmentid)
                return BadRequest(new { Message = "ID không khớp" });

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                    Message = "Dữ liệu đầu vào không hợp lệ."
                });
            }

            try
            {
                var result = await _service.UpdateAsync(id, teachingAssignment);
                return result == null ? NotFound() : NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return !success ? NotFound() : NoContent();
        }
    }
}