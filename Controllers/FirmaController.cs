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
    public class FirmaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FirmaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Firma
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Firma>>> GetFirmalar()
        {
            return await _context.Firmalar.ToListAsync();
        }

        // GET: api/Firma/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Firma>> GetFirma(Guid id)
        {
            var firma = await _context.Firmalar.FindAsync(id);

            if (firma == null)
            {
                return NotFound();
            }

            return firma;
        }

        // POST: api/Firma
        [HttpPost]
        public async Task<ActionResult<Firma>> PostFirma(Firma firma)
        {
            
            firma.CreateDate = DateTime.UtcNow;

            _context.Firmalar.Add(firma);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFirma), new { id = firma.Id }, firma);
        }

        // PUT: api/Firma/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFirma(Guid id, Firma firma)
        {
            if (id != firma.Id)
            {
                return BadRequest();
            }

            _context.Entry(firma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirmaExists(id))
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

        // DELETE: api/Firma/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFirma(Guid id)
        {
            var firma = await _context.Firmalar.FindAsync(id);
            if (firma == null)
            {
                return NotFound();
            }

            _context.Firmalar.Remove(firma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FirmaExists(Guid id)
        {
            return _context.Firmalar.Any(e => e.Id == id);
        }
    }
}
