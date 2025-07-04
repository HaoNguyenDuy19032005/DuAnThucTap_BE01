using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Interface;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var topic = await _service.GetByIdAsync(id);
            return topic == null ? NotFound() : Ok(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TopicList topic)
        {
            var created = await _service.CreateAsync(topic);
            return CreatedAtAction(nameof(GetById), new { id = created.TopicID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TopicList topic)
        {
            var updated = await _service.UpdateAsync(id, topic);
            return updated == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
