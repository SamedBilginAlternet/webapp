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
    public class VergiDairesiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VergiDairesiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VergiDairesi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VergiDairesi>>> GetVergiDairesi()
        {
            return await _context.VergiDairesi.ToListAsync();
        }

        // GET: api/VergiDairesi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VergiDairesi>> GetVergiDairesi(Guid id)
        {
            var vergiDairesi = await _context.VergiDairesi.FindAsync(id);

            if (vergiDairesi == null)
            {
                return NotFound();
            }

            return Ok(vergiDairesi);
        }

        // POST: api/VergiDairesi
        [HttpPost]
        public async Task<ActionResult<VergiDairesi>> PostVergiDairesi(VergiDairesi vergiDairesi)
        {
            _context.VergiDairesi.Add(vergiDairesi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVergiDairesi), new { id = vergiDairesi.Id }, vergiDairesi);
        }

        // PUT: api/VergiDairesi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVergiDairesi(Guid id, VergiDairesi vergiDairesi)
        {
            if (id != vergiDairesi.Id)
            {
                return BadRequest();
            }

            _context.Entry(vergiDairesi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VergiDairesiExists(id))
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

        // DELETE: api/VergiDairesi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVergiDairesi(Guid id)
        {
            var vergiDairesi = await _context.VergiDairesi.FindAsync(id);
            if (vergiDairesi == null)
            {
                return NotFound();
            }

            _context.VergiDairesi.Remove(vergiDairesi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VergiDairesiExists(Guid id)
        {
            return _context.VergiDairesi.Any(e => e.Id == id);
        }
    }
}
