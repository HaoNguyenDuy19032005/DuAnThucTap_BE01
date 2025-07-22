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
        [HttpPost("{id}/upload-logo")]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> UploadLogo(int id, IFormFile logo)
        {
            var (success, url, error) = await _service.UploadLogoAsync(id, logo);
            if (!success)
                return BadRequest(new { success = false, message = error });

            return Ok(new
            {
                success = true,
                schoolId = id,
                Logourl = url
            });
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
