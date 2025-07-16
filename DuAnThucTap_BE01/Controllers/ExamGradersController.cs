using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamGradersController : ControllerBase
    {
        private readonly IExamGraderService _service;

        public ExamGradersController(IExamGraderService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Examgrader>>> GetAll()
        {
            var examGraders = await _service.GetAllAsync();
            return Ok(examGraders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Examgrader>> GetById(int id)
        {
            var examGrader = await _service.GetByIdAsync(id);
            return examGrader == null ? NotFound() : Ok(examGrader);
        }

        [HttpPost]
        public async Task<ActionResult<Examgrader>> Create([FromBody] Examgrader examGrader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Trả về chi tiết lỗi xác thực
            }

            try
            {
                var created = await _service.CreateAsync(examGrader);
                return CreatedAtAction(nameof(GetById), new { id = created.Examgraderid }, created);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("General", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Examgrader examGrader)
        {
            if (id != examGrader.Examgraderid)
            {
                return BadRequest("ID trong URL không khớp với ID trong dữ liệu.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.UpdateAsync(id, examGrader);
                return result == null ? NotFound() : NoContent();
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("General", ex.Message);
                return BadRequest(ModelState);
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