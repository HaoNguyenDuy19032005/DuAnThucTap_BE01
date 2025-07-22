using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DuAnThucTap.Data;
using DuAnThucTap.Model;

namespace DuAnThucTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasstypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClasstypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL + Lọc theo trạng thái hoạt động
        // GET: api/Classtypes?isActive=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classtype>>> GetClasstypes([FromQuery] bool? isActive)
        {
            if (_context.Classtypes == null)
                return NotFound("Không tìm thấy dữ liệu Classtypes.");

            var query = _context.Classtypes.AsQueryable();

            if (isActive != null)
                query = query.Where(c => c.Isactive == isActive);

            return await query.ToListAsync();
        }

        // ✅ GET ONE
        // GET: api/Classtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classtype>> GetClasstype(int id)
        {
            if (_context.Classtypes == null)
                return NotFound();

            var classtype = await _context.Classtypes.FindAsync(id);

            if (classtype == null)
                return NotFound();

            return classtype;
        }

        // ✅ PUT: Cập nhật Classtype (Check tên trùng trừ chính nó)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasstype(int id, Classtype classtype)
        {
            if (id != classtype.Classtypeid)
                return BadRequest("ID không khớp.");

            var normalizedName = classtype.Classtypename.Trim().ToLower();

            var exists = await _context.Classtypes
                .AnyAsync(c => c.Classtypeid != id && c.Classtypename.Trim().ToLower() == normalizedName);

            if (exists)
                return BadRequest(new { message = "Tên lớp đã tồn tại!" });

            var existing = await _context.Classtypes.FindAsync(id);
            if (existing == null)
                return NotFound("Không tìm thấy Classtype để cập nhật.");

            existing.Classtypename = classtype.Classtypename;
            existing.Isactive = classtype.Isactive;
            existing.Updatedat = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Cập nhật thành công", data = existing });
        }


        // ✅ POST: Tạo mới Classtype (Check tên trùng)
        [HttpPost]
        public async Task<ActionResult<Classtype>> PostClasstype(Classtype classtype)
        {
            if (_context.Classtypes == null)
                return Problem("Entity set 'ApplicationDbContext.Classtypes' is null.");

            var normalizedName = classtype.Classtypename.Trim().ToLower();

            var exists = await _context.Classtypes
                .AnyAsync(c => c.Classtypename.Trim().ToLower() == normalizedName);

            if (exists)
                return BadRequest(new { message = "Tên lớp đã tồn tại!" });

            classtype.Createdat = DateTime.UtcNow;
            classtype.Updatedat = DateTime.UtcNow;

            _context.Classtypes.Add(classtype);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClasstype), new { id = classtype.Classtypeid }, classtype);
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasstype(int id)
        {
            if (_context.Classtypes == null)
                return NotFound();

            var classtype = await _context.Classtypes.FindAsync(id);
            if (classtype == null)
                return NotFound();

            _context.Classtypes.Remove(classtype);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Xoá thành công" });
        }

        // ✅ Helper
        private bool ClasstypeExists(int id)
        {
            return _context.Classtypes.Any(e => e.Classtypeid == id);
        }
    }
}
