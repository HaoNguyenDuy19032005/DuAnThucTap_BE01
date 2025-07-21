//using Microsoft.AspNetCore.Mvc;
//using Nhom2ThucTap.DTO;
//using Nhom2ThucTap.Interface;
//using Nhom2ThucTap.Models;

//namespace Nhom2ThucTap.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TestController : ControllerBase
//    {
//        private readonly ITestService _service;
//        private readonly IWebHostEnvironment _env;

//        public TestController(ITestService service, IWebHostEnvironment env)
//        {
//            _service = service;
//            _env = env;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll([FromQuery] string? keyword = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
//        {
//            var result = await _service.GetAllAsync(keyword, page, pageSize);
//            return Ok(new { message = "Lấy danh sách bài kiểm tra thành công", data = result });
//        }

//        [HttpGet("upcoming")]
//        public async Task<IActionResult> GetUpcoming([FromQuery] int page = 1)
//        {
//            var result = await _service.GetUpcomingTestsAsync(page, 10);
//            return Ok(new { message = "Danh sách bài kiểm tra sắp tới", data = result });
//        }

//        [HttpGet("finished")]
//        public async Task<IActionResult> GetFinished([FromQuery] int page = 1)
//        {
//            var result = await _service.GetFinishedTestsAsync(page, 10);
//            return Ok(new { message = "Danh sách bài kiểm tra đã hoàn thành", data = result });
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var test = await _service.GetByIdAsync(id);
//            if (test == null) return NotFound(new { message = "Không tìm thấy bài kiểm tra" });
//            return Ok(new { message = "Lấy thông tin thành công", data = test });
//        }

//        [HttpPost]
//        [Consumes("multipart/form-data")]
//        public async Task<IActionResult> Create([FromForm] TestCreateDto dto)
//        {
//            var errors = new Dictionary<string, List<string>>();
//            void AddError(string field, string msg)
//            {
//                if (!errors.ContainsKey(field)) errors[field] = new();
//                errors[field].Add(msg);
//            }

//            if (string.IsNullOrWhiteSpace(dto.Title)) AddError("Title", "Tiêu đề không được để trống.");
//            if (!await _service.TeacherExistsAsync(dto.Teacherid)) AddError("Teacherid", "Giáo viên không tồn tại.");
//            if (dto.Starttime != null && dto.Endtime != null && dto.Endtime < dto.Starttime)
//                AddError("Endtime", "Thời gian kết thúc không được trước thời gian bắt đầu.");
//            if (dto.AttachmentFile != null && !dto.AttachmentFile.ContentType.StartsWith("image/"))
//                AddError("AttachmentFile", "Tệp đính kèm phải là hình ảnh.");
//            if (errors.Any()) return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

//            string? fileName = null;
//            if (dto.AttachmentFile != null)
//            {
//                fileName = Guid.NewGuid() + Path.GetExtension(dto.AttachmentFile.FileName);
//                var path = Path.Combine(_env.WebRootPath, "uploads", "tests", fileName);
//                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
//                using var stream = new FileStream(path, FileMode.Create);
//                await dto.AttachmentFile.CopyToAsync(stream);
//            }

//            var test = new Test
//            {
//                Teacherid = dto.Teacherid,
//                Title = dto.Title,
//                Testformat = dto.Testformat,
//                Durationinminutes = dto.Durationinminutes,
//                Starttime = dto.Starttime,
//                Endtime = dto.Endtime,
//                Description = dto.Description,
//                Classification = dto.Classification,
//                Attachmenturl = fileName,
//                Requirestudentattachment = dto.Requirestudentattachment
//            };

//            var created = await _service.CreateAsync(test);
//            return Ok(new { message = "Tạo bài kiểm tra thành công", data = created });
//        }

//        [HttpPut("{id}")]
//        [Consumes("multipart/form-data")]
//        public async Task<IActionResult> Update(int id, [FromForm] TestCreateDto dto)
//        {
//            var existing = await _service.GetByIdAsync(id);
//            if (existing == null) return NotFound(new { message = "Không tìm thấy bài kiểm tra" });

//            var errors = new Dictionary<string, List<string>>();
//            void AddError(string field, string msg)
//            {
//                if (!errors.ContainsKey(field)) errors[field] = new();
//                errors[field].Add(msg);
//            }

//            if (string.IsNullOrWhiteSpace(dto.Title)) AddError("Title", "Tiêu đề không được để trống.");
//            if (!await _service.TeacherExistsAsync(dto.Teacherid)) AddError("Teacherid", "Giáo viên không tồn tại.");
//            if (dto.Starttime != null && dto.Endtime != null && dto.Endtime < dto.Starttime)
//                AddError("Endtime", "Thời gian kết thúc không được trước thời gian bắt đầu.");
//            if (dto.AttachmentFile != null && !dto.AttachmentFile.ContentType.StartsWith("image/"))
//                AddError("AttachmentFile", "Tệp đính kèm phải là hình ảnh.");
//            if (errors.Any()) return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });

//            string? fileName = existing.Attachmenturl;
//            if (dto.AttachmentFile != null)
//            {
//                fileName = Guid.NewGuid() + Path.GetExtension(dto.AttachmentFile.FileName);
//                var path = Path.Combine(_env.WebRootPath, "uploads", "tests", fileName);
//                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
//                using var stream = new FileStream(path, FileMode.Create);
//                await dto.AttachmentFile.CopyToAsync(stream);
//            }

//            var updated = new Test
//            {
//                Testid = id,
//                Teacherid = dto.Teacherid,
//                Title = dto.Title,
//                Testformat = dto.Testformat,
//                Durationinminutes = dto.Durationinminutes,
//                Starttime = dto.Starttime,
//                Endtime = dto.Endtime,
//                Description = dto.Description,
//                Classification = dto.Classification,
//                Attachmenturl = fileName,
//                Requirestudentattachment = dto.Requirestudentattachment
//            };

//            var success = await _service.UpdateAsync(id, updated);
//            return success ? Ok(new { message = "Cập nhật thành công" }) : StatusCode(500, new { message = "Lỗi cập nhật" });
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var success = await _service.DeleteAsync(id);
//            return success ? Ok(new { message = "Xoá thành công" }) : NotFound(new { message = "Không tìm thấy bài kiểm tra" });
//        }
//    }

//}
