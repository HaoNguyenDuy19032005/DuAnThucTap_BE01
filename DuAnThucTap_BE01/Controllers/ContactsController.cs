using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Interface;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DuAnThucTap_BE01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;
        public ContactsController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? searchQuery,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var data = await _service.GetAllAsync(searchQuery, pageNumber, pageSize);
            return Ok(new ApiResponse<PagedResponse<ContactDto>>(HttpStatusCode.OK.GetHashCode(), "Lấy danh sách thành công", data));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _service.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound(new ApiResponse<object>(HttpStatusCode.NotFound.GetHashCode(), $"Không tìm thấy liên hệ với ID = {id}", null));
            }
            return Ok(new ApiResponse<ContactDto>(HttpStatusCode.OK.GetHashCode(), "Lấy dữ liệu thành công", contact));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactRequestDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>(HttpStatusCode.BadRequest.GetHashCode(), "Dữ liệu không hợp lệ", ModelState));
            }

            var created = await _service.CreateAsync(contactDto);
            var response = new ApiResponse<Contact>(HttpStatusCode.Created.GetHashCode(), "Tạo mới thành công", created);
            return CreatedAtAction(nameof(GetById), new { id = created.Contactid }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactRequestDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>(HttpStatusCode.BadRequest.GetHashCode(), "Dữ liệu không hợp lệ", ModelState));
            }

            var result = await _service.UpdateAsync(id, contactDto);
            if (result == null)
            {
                return NotFound(new ApiResponse<object>(HttpStatusCode.NotFound.GetHashCode(), $"Không tìm thấy liên hệ với ID = {id}", null));
            }
            return Ok(new ApiResponse<Contact>(HttpStatusCode.OK.GetHashCode(), "Cập nhật thành công", result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound(new ApiResponse<object>(HttpStatusCode.NotFound.GetHashCode(), $"Không tìm thấy liên hệ với ID = {id}", null));
            }
            return Ok(new ApiResponse<object>(HttpStatusCode.OK.GetHashCode(), "Xóa thành công", null));
        }
    }
}