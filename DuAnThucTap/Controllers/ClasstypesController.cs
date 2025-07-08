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
    [Route("api/[controller]")]
    [ApiController]
    public class ClasstypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClasstypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Classtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classtype>>> GetClasstypes()
        {
          if (_context.Classtypes == null)
          {
              return NotFound();
          }
            return await _context.Classtypes.ToListAsync();
        }

        // GET: api/Classtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classtype>> GetClasstype(int id)
        {
          if (_context.Classtypes == null)
          {
              return NotFound();
          }
            var classtype = await _context.Classtypes.FindAsync(id);

            if (classtype == null)
            {
                return NotFound();
            }

            return classtype;
        }

        // PUT: api/Classtypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasstype(int id, Classtype classtype)
        {
            if (id != classtype.Classtypeid)
            {
                return BadRequest();
            }

            _context.Entry(classtype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClasstypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Classtypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Classtype>> PostClasstype(Classtype classtype)
        {
          if (_context.Classtypes == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Classtypes'  is null.");
          }
            _context.Classtypes.Add(classtype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClasstype", new { id = classtype.Classtypeid }, classtype);
        }

        // DELETE: api/Classtypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasstype(int id)
        {
            if (_context.Classtypes == null)
            {
                return NotFound();
            }
            var classtype = await _context.Classtypes.FindAsync(id);
            if (classtype == null)
            {
                return NotFound();
            }

            _context.Classtypes.Remove(classtype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClasstypeExists(int id)
        {
            return (_context.Classtypes?.Any(e => e.Classtypeid == id)).GetValueOrDefault();
        }
    }
}
