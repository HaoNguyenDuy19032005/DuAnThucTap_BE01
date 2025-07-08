using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicListController : ControllerBase
    {
        private readonly ITopicListService _service;

        public TopicListController(ITopicListService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topiclist>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Topiclist>> GetById(int id)
        {
            var topicList = await _service.GetByIdAsync(id);
            return topicList == null ? NotFound() : Ok(topicList);
        }

        [HttpPost]
        public async Task<ActionResult<Topiclist>> Create([FromBody] Topiclist topicList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                    Message = "Dữ liệu đầu vào không hợp lệ."
                });
            }

            try
            {
                var created = await _service.CreateAsync(topicList);
                return CreatedAtAction(nameof(GetById), new { id = created.Topicid }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Topiclist topicList)
        {
            if (id != topicList.Topicid)
                return BadRequest(new { Message = "ID không khớp" });

            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage),
                    Message = "Dữ liệu đầu vào không hợp lệ."
                });
            }

            try
            {
                var result = await _service.UpdateAsync(id, topicList);
                return result == null ? NotFound() : NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
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