using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IlController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Il
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Il>>> GetIller()
        {
            return await _context.Iller.ToListAsync();
        }

        // GET: api/Il/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Il>> GetIl(Guid id)
        {
            var il = await _context.Iller.FindAsync(id);

            if (il == null)
            {
                return NotFound();
            }

            return il;
        }

        // POST: api/Il
        [HttpPost]
        public async Task<ActionResult<Il>> PostIl(Il il)
        {
            _context.Iller.Add(il);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIl), new { id = il.Id }, il);
        }

        // PUT: api/Il/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIl(Guid id, Il il)
        {
            if (id != il.Id)
            {
                return BadRequest();
            }

            _context.Entry(il).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IlExists(id))
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

        // DELETE: api/Il/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIl(Guid id)
        {
            var il = await _context.Iller.FindAsync(id);
            if (il == null)
            {
                return NotFound();
            }

            _context.Iller.Remove(il);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IlExists(Guid id)
        {
            return _context.Iller.Any(e => e.Id == id);
        }
    }
}
