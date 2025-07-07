using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var topic = await _service.GetByIdAsync(id);
            return topic == null ? NotFound() : Ok(topic);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicListDto topicList)
        {
            var created = await _service.CreateAsync(topicList);
            return CreatedAtAction(nameof(GetById), new { id = created.Topicid }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TopicListDto topicList)
        {
            var updated = await _service.UpdateAsync(id, topicList);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}