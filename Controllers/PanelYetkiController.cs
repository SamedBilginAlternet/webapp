using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Dtos;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelYetkiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PanelYetkiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PanelYetki
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PanelYetki>>> GetPanelYetkiler()
        {
            return await _context.PanelYetkiler.ToListAsync();
        }

        // GET: api/PanelYetki/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PanelYetki>> GetPanelYetki(Guid id)
        {
            var panelYetki = await _context.PanelYetkiler.FindAsync(id);

            if (panelYetki == null)
            {
                return NotFound();
            }

            return panelYetki;
        }

        // POST: api/PanelYetki
        [HttpPost]
        public async Task<ActionResult<PanelYetki>> PostPanelYetki(PanelYetki panelYetki)
        {
            panelYetki.Id = Guid.NewGuid();
            _context.PanelYetkiler.Add(panelYetki);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPanelYetki), new { id = panelYetki.Id }, panelYetki);
        }

        // PUT: api/PanelYetki/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanelYetki(Guid id, PanelYetki panelYetki)
        {
            if (id != panelYetki.Id)
            {
                return BadRequest();
            }

            _context.Entry(panelYetki).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PanelYetkiExists(id))
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

        // DELETE: api/PanelYetki/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanelYetki(Guid id)
        {
            var panelYetki = await _context.PanelYetkiler.FindAsync(id);
            if (panelYetki == null)
            {
                return NotFound();
            }

            _context.PanelYetkiler.Remove(panelYetki);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PanelYetkiExists(Guid id)
        {
            return _context.PanelYetkiler.Any(e => e.Id == id);
        }

        [HttpGet("getYetki")]
        // GET /api/Yetki/getYetki/id=00000000-0000-0000-0000-000000000000

        public async Task<IActionResult> GetYetki([FromQuery] Guid id)
        {
            var panelYetki = await _context.PanelYetkiler
                .Where(y => y.AdminId == id)
                .Select(y => new YetkiDto
                {
                    Id = y.Id,
                    YetkiAdi = y.YetkiAdi
                })
                .ToListAsync();

            var kullaniciYetki = await _context.KullaniciYetkiler
                .Where(y => y.KullaniciId == id)
                .Select(y => new YetkiDto
                {
                    Id = y.Id,
                    YetkiAdi = y.YetkiAdi
                })
                .ToListAsync();

            var combinedYetki = panelYetki.Concat(kullaniciYetki).ToList();

            if (combinedYetki == null || !combinedYetki.Any())
            {
                return NotFound(new { success = false, message = "No Yetki found for this Id" });
            }

            return Ok(new { success = true, data = combinedYetki });
        }

    }
}
