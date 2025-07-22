using DuAnThucTap.Irepository;
using DuAnThucTap.Model;
using DuAnThucTap.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampusController : ControllerBase
    {
        private readonly ICampusService _service;

        public CampusController(ICampusService service)
        {
            _service = service;
        }

        // GET: api/Campus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campus>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        // GET: api/Campus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Campus>> Get(int id)
        {
            var campus = await _service.GetByIdAsync(id);
            if (campus == null) return NotFound();
            return Ok(campus);
        }

        // POST: api/Campus
        [HttpPost]
        public async Task<ActionResult<Campus>> Create(Campus campus)
        {
            var created = await _service.CreateAsync(campus);
            return CreatedAtAction(nameof(Get), new { id = created.Campusid }, created);
        }

        // PUT: api/Campus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Campus campus)
        {
            var ok = await _service.UpdateAsync(id, campus);
            if (!ok) return BadRequest();
            return NoContent();
        }

        // DELETE: api/Campus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }

        // POST: api/Campus/5/upload-image
        [HttpPost("{id}/upload-image")]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var (success, url, error) = await _service.UploadImageAsync(id, file);
            if (!success)
                return BadRequest(new { success = false, message = error });

            return Ok(new { success = true, campusId = id, Imageurl = url });
        }
    }
}
