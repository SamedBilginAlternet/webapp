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
    public class IlceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IlceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ilce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetIlceler()
        {
            return await _context.Ilceler.ToListAsync();
        }

        // GET: api/Ilce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ilce>> GetIlce(Guid id)
        {
            var ilce = await _context.Ilceler.FindAsync(id);

            if (ilce == null)
            {
                return NotFound();
            }

            return ilce;
        }

        // POST: api/Ilce
        [HttpPost]
        public async Task<ActionResult<Ilce>> PostIlce(Ilce ilce)
        {
            _context.Ilceler.Add(ilce);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIlce), new { id = ilce.Id }, ilce);
        }

        // PUT: api/Ilce/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIlce(Guid id, Ilce ilce)
        {
            if (id != ilce.Id)
            {
                return BadRequest();
            }

            _context.Entry(ilce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IlceExists(id))
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

        // DELETE: api/Ilce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIlce(Guid id)
        {
            var ilce = await _context.Ilceler.FindAsync(id);
            if (ilce == null)
            {
                return NotFound();
            }

            _context.Ilceler.Remove(ilce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IlceExists(Guid id)
        {
            return _context.Ilceler.Any(e => e.Id == id);
        }
    }
}
