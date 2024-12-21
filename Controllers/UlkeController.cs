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
    public class UlkeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UlkeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ulke
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ulke>>> GetUlkeler()
        {
            return await _context.Ulkeler.ToListAsync();
        }

        // GET: api/Ulke/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ulke>> GetUlke(Guid id)
        {
            var ulke = await _context.Ulkeler.FindAsync(id);

            if (ulke == null)
            {
                return NotFound();
            }

            return Ok(ulke);
        }

        // POST: api/Ulke
        [HttpPost]
        public async Task<ActionResult<Ulke>> PostUlke(Ulke ulke)
        {
            _context.Ulkeler.Add(ulke);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUlke), new { id = ulke.Id }, ulke);
        }

        // PUT: api/Ulke/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUlke(Guid id, Ulke ulke)
        {
            if (id != ulke.Id)
            {
                return BadRequest();
            }

            _context.Entry(ulke).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UlkeExists(id))
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

        // DELETE: api/Ulke/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUlke(Guid id)
        {
            var ulke = await _context.Ulkeler.FindAsync(id);
            if (ulke == null)
            {
                return NotFound();
            }

            _context.Ulkeler.Remove(ulke);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UlkeExists(Guid id)
        {
            return _context.Ulkeler.Any(e => e.Id == id);
        }
    }
}
