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
    public class UnvanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnvanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Unvan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unvan>>> GetUnvanlar()
        {
            return await _context.Unvanlar.ToListAsync();
        }

        // GET: api/Unvan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unvan>> GetUnvan(Guid id)
        {
            var unvan = await _context.Unvanlar.FindAsync(id);

            if (unvan == null)
            {
                return NotFound();
            }

            return unvan;
        }

        // POST: api/Unvan
        [HttpPost]
        public async Task<ActionResult<Unvan>> PostUnvan(Unvan unvan)
        {
            _context.Unvanlar.Add(unvan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUnvan), new { id = unvan.Id }, unvan);
        }

        // PUT: api/Unvan/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnvan(Guid id, Unvan unvan)
        {
            if (id != unvan.Id)
            {
                return BadRequest();
            }

            _context.Entry(unvan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnvanExists(id))
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

        // DELETE: api/Unvan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnvan(Guid id)
        {
            var unvan = await _context.Unvanlar.FindAsync(id);
            if (unvan == null)
            {
                return NotFound();
            }

            _context.Unvanlar.Remove(unvan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnvanExists(Guid id)
        {
            return _context.Unvanlar.Any(e => e.Id == id);
        }
    }
}
