using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _service;

        public SemesterController(ISemesterService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var semester = _service.GetById(id);
            if (semester == null)
                return NotFound();

            return Ok(semester);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Semester model)
        {
            if (model == null)
                return BadRequest();

            var created = _service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Semesterid }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Semester model)
        {
            var success = _service.Update(id, model);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
