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
    public class KullaniciYetkiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KullaniciYetkiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/KullaniciYetki
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KullaniciYetki>>> GetKullaniciYetkiler()
        {
            return await _context.KullaniciYetkiler.ToListAsync();
        }

        // GET: api/KullaniciYetki/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KullaniciYetki>> GetKullaniciYetki(Guid id)
        {
            var kullaniciYetki = await _context.KullaniciYetkiler.FindAsync(id);

            if (kullaniciYetki == null)
            {
                return NotFound();
            }

            return kullaniciYetki;
        }

        // POST: api/KullaniciYetki
        [HttpPost]
        public async Task<ActionResult<KullaniciYetki>> PostKullaniciYetki(KullaniciYetki kullaniciYetki)
        {
            kullaniciYetki.Id = Guid.NewGuid();
            _context.KullaniciYetkiler.Add(kullaniciYetki);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKullaniciYetki), new { id = kullaniciYetki.Id }, kullaniciYetki);
        }

        // PUT: api/KullaniciYetki/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullaniciYetki(Guid id, KullaniciYetki kullaniciYetki)
        {
            if (id != kullaniciYetki.Id)
            {
                return BadRequest();
            }

            _context.Entry(kullaniciYetki).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullaniciYetkiExists(id))
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

        // DELETE: api/KullaniciYetki/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullaniciYetki(Guid id)
        {
            var kullaniciYetki = await _context.KullaniciYetkiler.FindAsync(id);
            if (kullaniciYetki == null)
            {
                return NotFound();
            }

            _context.KullaniciYetkiler.Remove(kullaniciYetki);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KullaniciYetkiExists(Guid id)
        {
            return _context.KullaniciYetkiler.Any(e => e.Id == id);
        }
    }
}
