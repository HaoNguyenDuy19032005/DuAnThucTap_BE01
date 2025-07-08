using DuAnThucTapNhom3.Iterface;
using DuAnThucTapNhom3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DuAnThucTapNhom3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolYearController : ControllerBase
    {
        private readonly ISchoolYearService _service;

        public SchoolYearController(ISchoolYearService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _service.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSchoolYearDto dto)
        {
            if (dto == null) return BadRequest();

            var created = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Schoolyearid }, created);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Schoolyear model)
        {
            if (!_service.Update(id, model)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_service.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
