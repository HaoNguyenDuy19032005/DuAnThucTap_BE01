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
    public class SchoolinformationsController : ControllerBase
    {
        private readonly ISchoolinformationService _service;

        public SchoolinformationsController(ISchoolinformationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schoolinformation>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schoolinformation>> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Schoolinformation>> Post(Schoolinformation school)
        {
            var created = await _service.CreateAsync(school);
            return CreatedAtAction(nameof(Get), new { id = created.Schoolinfoid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Schoolinformation school)
        {
            var updated = await _service.UpdateAsync(id, school);
            if (!updated) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
