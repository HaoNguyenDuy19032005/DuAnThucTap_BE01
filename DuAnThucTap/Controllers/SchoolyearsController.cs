using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnThucTap.Data;
using DuAnThucTap.Model;

namespace DuAnThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolyearsController : ControllerBase
    {
        private readonly ISchoolyearService _service;

        public SchoolyearsController(ISchoolyearService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schoolyear>>> GetSchoolyears()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schoolyear>> GetSchoolyear(int id)
        {
            var schoolyear = await _service.GetByIdAsync(id);
            if (schoolyear == null) return NotFound();
            return Ok(schoolyear);
        }

        [HttpPost]
        public async Task<ActionResult<Schoolyear>> PostSchoolyear(Schoolyear schoolyear)
        {
            var created = await _service.CreateAsync(schoolyear);
            return CreatedAtAction(nameof(GetSchoolyear), new { id = created.Schoolyearid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolyear(int id, Schoolyear schoolyear)
        {
            var success = await _service.UpdateAsync(id, schoolyear);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolyear(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
