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
    public class GradelevelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradelevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Gradelevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gradelevel>>> GetGradelevels()
        {
          if (_context.Gradelevels == null)
          {
              return NotFound();
          }
            return await _context.Gradelevels.ToListAsync();
        }

        // GET: api/Gradelevels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gradelevel>> GetGradelevel(int id)
        {
          if (_context.Gradelevels == null)
          {
              return NotFound();
          }
            var gradelevel = await _context.Gradelevels.FindAsync(id);

            if (gradelevel == null)
            {
                return NotFound();
            }

            return gradelevel;
        }

        // PUT: api/Gradelevels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGradelevel(int id, Gradelevel gradelevel)
        {
            if (id != gradelevel.Gradelevelid)
            {
                return BadRequest();
            }

            _context.Entry(gradelevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradelevelExists(id))
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

        // POST: api/Gradelevels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gradelevel>> PostGradelevel(Gradelevel gradelevel)
        {
          if (_context.Gradelevels == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Gradelevels'  is null.");
          }
            _context.Gradelevels.Add(gradelevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradelevel", new { id = gradelevel.Gradelevelid }, gradelevel);
        }

        // DELETE: api/Gradelevels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGradelevel(int id)
        {
            if (_context.Gradelevels == null)
            {
                return NotFound();
            }
            var gradelevel = await _context.Gradelevels.FindAsync(id);
            if (gradelevel == null)
            {
                return NotFound();
            }

            _context.Gradelevels.Remove(gradelevel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradelevelExists(int id)
        {
            return (_context.Gradelevels?.Any(e => e.Gradelevelid == id)).GetValueOrDefault();
        }
    }
}
