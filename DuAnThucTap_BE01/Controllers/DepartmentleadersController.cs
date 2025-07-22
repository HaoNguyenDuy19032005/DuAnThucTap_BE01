using Microsoft.AspNetCore.Mvc;
using DuAnThucTap.Irepository;
using DuAnThucTap.Model;

namespace DuAnThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentleadersController : ControllerBase
    {
        private readonly IDepartmentleadersService _service;

        public DepartmentleadersController(IDepartmentleadersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationDto pagination)
        {
            var data = await _service.GetAllAsync(pagination);
            if (!data.Any())
            {
                return NotFound(new { message = "Không tìm thấy trưởng bộ môn nào." });
            }

            var result = data.Select(dl => new
            {
                DepartmentleaderID = dl.Departmentleaderid,
                ToBoMon = dl.Department?.Departmentname,
                TruongBoMon = dl.Teacher?.Fullname,
                dl.Startdate,
                dl.Enddate
            });

            return Ok(new
            {
                message = "Lấy danh sách trưởng bộ môn thành công",
                data = result
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound(new { message = "Không tìm thấy trưởng bộ môn." });

            var result = new 
            {
                ToBoMon = entity.Department?.Departmentname,
                TruongBoMon = entity.Teacher?.Fullname,
                entity.Startdate,
                entity.Enddate
            };

            return Ok(new
            {
                message = "Lấy thông tin trưởng bộ môn thành công",
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentleaderCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Departmentleaderid },
                    new { message = "Tạo trưởng bộ môn thành công", data = created });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentleaderCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _service.UpdateAsync(id, dto);
                if (!success)
                    return NotFound(new { message = "Không tìm thấy trưởng bộ môn để cập nhật." });

                var updated = await _service.GetByIdAsync(id); // optional: trả về object sau cập nhật
                return Ok(new
                {
                    message = "Cập nhật trưởng bộ môn thành công",
                    data = updated
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy trưởng bộ môn để xoá." });

            return Ok(new { message = "Xoá trưởng bộ môn thành công" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByDepartmentName([FromQuery] string keyword, [FromQuery] PaginationDto pagination)
        {
            var results = await _service.SearchByDepartmentNameAsync(keyword, pagination);
            if (!results.Any())
                return NotFound(new { message = "Không tìm thấy tổ bộ môn phù hợp." });

            var data = results.Select(dl => new
            {
                ToBoMon = dl.Department?.Departmentname,
                TruongBoMon = dl.Teacher?.Fullname,
                dl.Startdate,
                dl.Enddate
            });

            return Ok(new
            {
                message = "Tìm kiếm trưởng bộ môn theo tên tổ bộ môn thành công",
                data = data
            });
        }

    }
}