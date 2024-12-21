using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Models.Dtos;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KullaniciController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Kullanici
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanici>>> GetKullanicilar()
        {
            return await _context.Kullanicilar.ToListAsync();
        }

        // GET: api/Kullanici/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanici>> GetKullanici(Guid id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);

            if (kullanici == null)
            {
                return NotFound();
            }

            return kullanici;
        }

        // POST: api/Kullanici
        [HttpPost]
        public async Task<ActionResult<Kullanici>> PostKullanici(Kullanici kullanici)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    kullanici.Id = Guid.NewGuid();
                    _context.Kullanicilar.Add(kullanici);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetKullanici), new { id = kullanici.Id }, kullanici);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Kullanici/AddRole
        [HttpPost("AddRole")]
        public async Task<ActionResult> AddRole(KullaniciYetki kullaniciYetki)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.KullaniciYetkiler.Add(kullaniciYetki);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Kullanici/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullanici(Guid id, [FromBody] Kullanici kullanici)
        {
            // Log incoming data
            Console.WriteLine($"Received PUT request for ID: {id}");
            Console.WriteLine($"Received Kullanici: {JsonConvert.SerializeObject(kullanici)}");

            if (id != kullanici.Id)
            {
                return BadRequest();
            }

            // Check if username is unique
            var existingUser = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.KullaniciAdi == kullanici.KullaniciAdi && k.Id != id);

            if (existingUser != null)
            {
                return BadRequest(new { message = "This username is already in use" });
            }

            // Update the user with new values
            var userToUpdate = await _context.Kullanicilar.FindAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            // Map updated fields
            userToUpdate.KullaniciAdi = kullanici.KullaniciAdi;
            userToUpdate.Email = kullanici.Email;
            userToUpdate.Sifre = kullanici.Sifre;
            userToUpdate.IsActive = kullanici.IsActive;
            userToUpdate.FirmaId = kullanici.FirmaId;
            userToUpdate.UnvanId = kullanici.UnvanId;
            userToUpdate.UpdatedBy = kullanici.UpdatedBy;
            userToUpdate.UpdateDate = DateTime.Now;

            try
            {
                _context.Entry(userToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullaniciExists(id))
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

        // DELETE: api/Kullanici/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(Guid id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
            {
                return NotFound();
            }

            // Set isActive to false instead of deleting the user
            kullanici.IsActive = false;
            _context.Entry(kullanici).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullaniciExists(id))
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

        // POST: api/Kullanici/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] KullaniciLoginDto loginDto)
        {
            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.Email == loginDto.Email && k.Sifre == loginDto.Sifre);

            if (kullanici != null)
            {
                return Ok(new { success = true, message = "Login successful", id = kullanici.Id, isim = kullanici.KullaniciAdi });
            }

            var panelUser = await _context.PanelUsers
                .FirstOrDefaultAsync(p => p.Username == loginDto.Email && p.Password == loginDto.Sifre);

            if (panelUser != null)
            {
                return Ok(new { success = true, message = "Login successful", id = panelUser.Id });
            }

            return Unauthorized(new { success = false, message = "Invalid email or password" });
        }

        private bool KullaniciExists(Guid id)
        {
            return _context.Kullanicilar.Any(e => e.Id == id);
        }
    }
}
