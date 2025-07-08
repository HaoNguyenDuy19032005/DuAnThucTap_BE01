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
    public class DepartmentleadersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentleadersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Departmentleaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departmentleader>>> GetDepartmentleaders()
        {
          if (_context.Departmentleaders == null)
          {
              return NotFound();
          }
            return await _context.Departmentleaders.ToListAsync();
        }

        // GET: api/Departmentleaders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departmentleader>> GetDepartmentleader(int id)
        {
          if (_context.Departmentleaders == null)
          {
              return NotFound();
          }
            var departmentleader = await _context.Departmentleaders.FindAsync(id);

            if (departmentleader == null)
            {
                return NotFound();
            }

            return departmentleader;
        }

        // PUT: api/Departmentleaders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentleader(int id, Departmentleader departmentleader)
        {
            if (id != departmentleader.Departmentleaderid)
            {
                return BadRequest();
            }

            _context.Entry(departmentleader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentleaderExists(id))
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

        // POST: api/Departmentleaders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Departmentleader>> PostDepartmentleader(Departmentleader departmentleader)
        {
          if (_context.Departmentleaders == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Departmentleaders'  is null.");
          }
            _context.Departmentleaders.Add(departmentleader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentleader", new { id = departmentleader.Departmentleaderid }, departmentleader);
        }

        // DELETE: api/Departmentleaders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentleader(int id)
        {
            if (_context.Departmentleaders == null)
            {
                return NotFound();
            }
            var departmentleader = await _context.Departmentleaders.FindAsync(id);
            if (departmentleader == null)
            {
                return NotFound();
            }

            _context.Departmentleaders.Remove(departmentleader);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentleaderExists(int id)
        {
            return (_context.Departmentleaders?.Any(e => e.Departmentleaderid == id)).GetValueOrDefault();
        }
    }
}
