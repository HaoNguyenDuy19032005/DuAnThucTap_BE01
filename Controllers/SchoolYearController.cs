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
            if (dto == null)
                return BadRequest("Invalid data.");

            try
            {
                var created = _service.Create(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = created.Schoolyearid },
                    created);
            }
            catch (Exception ex)
            {
                // Nếu lỗi là do tên trùng, trả 409
                if (ex.Message.Contains("đã tồn tại") || ex.Message.Contains("duplicate"))
                {
                    return Conflict(new { message = ex.Message });
                }

                return StatusCode(500, new { message = "Đã xảy ra lỗi.", detail = ex.Message });
            }
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
