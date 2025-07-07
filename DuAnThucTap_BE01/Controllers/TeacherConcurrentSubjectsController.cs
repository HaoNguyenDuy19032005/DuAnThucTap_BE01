// Controllers/TeacherConcurrentSubjectsController.cs
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherConcurrentSubjectsController : ControllerBase
    {
        private readonly ITeacherConcurrentSubjectService _service;
        public TeacherConcurrentSubjectsController(ITeacherConcurrentSubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacherconcurrentsubject>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // Route cho khóa phức hợp
        [HttpGet("{teacherId}/{subjectId}/{schoolYearId}")]
        public async Task<ActionResult<Teacherconcurrentsubject>> GetById(Guid teacherId, Guid subjectId, Guid schoolYearId)
        {
            var item = await _service.GetByIdAsync(teacherId, subjectId, schoolYearId);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Teacherconcurrentsubject>> Create([FromBody] Teacherconcurrentsubject assignment)
        {
            try
            {
                var created = await _service.CreateAsync(assignment);
                // Trả về route để GetById
                return CreatedAtAction(nameof(GetById),
                    new
                    {
                        teacherId = created.Teacherid,
                        subjectId = created.Subjectid,
                        schoolYearId = created.Schoolyearid
                    }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // Route cho khóa phức hợp
        [HttpDelete("{teacherId}/{subjectId}/{schoolYearId}")]
        public async Task<IActionResult> Delete(Guid teacherId, Guid subjectId, Guid schoolYearId)
        {
            var success = await _service.DeleteAsync(teacherId, subjectId, schoolYearId);
            return !success ? NotFound() : NoContent();
        }
    }
}