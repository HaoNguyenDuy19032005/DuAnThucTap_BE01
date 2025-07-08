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
    public class GradetypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradetypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Gradetypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gradetype>>> GetGradetype()
        {
          if (_context.Gradetype == null)
          {
              return NotFound();
          }
            return await _context.Gradetype.ToListAsync();
        }

        // GET: api/Gradetypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gradetype>> GetGradetype(int id)
        {
          if (_context.Gradetype == null)
          {
              return NotFound();
          }
            var gradetype = await _context.Gradetype.FindAsync(id);

            if (gradetype == null)
            {
                return NotFound();
            }

            return gradetype;
        }

        // PUT: api/Gradetypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGradetype(int id, Gradetype gradetype)
        {
            if (id != gradetype.Gradetypeid)
            {
                return BadRequest();
            }

            _context.Entry(gradetype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradetypeExists(id))
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

        // POST: api/Gradetypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gradetype>> PostGradetype(Gradetype gradetype)
        {
          if (_context.Gradetype == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Gradetype'  is null.");
          }
            _context.Gradetype.Add(gradetype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradetype", new { id = gradetype.Gradetypeid }, gradetype);
        }

        // DELETE: api/Gradetypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGradetype(int id)
        {
            if (_context.Gradetype == null)
            {
                return NotFound();
            }
            var gradetype = await _context.Gradetype.FindAsync(id);
            if (gradetype == null)
            {
                return NotFound();
            }

            _context.Gradetype.Remove(gradetype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradetypeExists(int id)
        {
            return (_context.Gradetype?.Any(e => e.Gradetypeid == id)).GetValueOrDefault();
        }
    }
}
