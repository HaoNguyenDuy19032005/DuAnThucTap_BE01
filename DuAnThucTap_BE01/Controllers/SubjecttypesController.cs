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
    public class SubjecttypesController : ControllerBase
    {
        private readonly ISubjecttypeService _service;

        public SubjecttypesController(ISubjecttypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subjecttype>>> GetSubjecttypes()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subjecttype>> GetSubjecttype(int id)
        {
            var subjecttype = await _service.GetByIdAsync(id);
            if (subjecttype == null) return NotFound();
            return Ok(subjecttype);
        }

        [HttpPost]
        public async Task<ActionResult<Subjecttype>> PostSubjecttype(Subjecttype subjecttype)
        {
            var created = await _service.CreateAsync(subjecttype);
            return CreatedAtAction(nameof(GetSubjecttype), new { id = created.Subjecttypeid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjecttype(int id, Subjecttype subjecttype)
        {
            var updated = await _service.UpdateAsync(id, subjecttype);
            if (!updated) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjecttype(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
