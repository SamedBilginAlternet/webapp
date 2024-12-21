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
    public class AdresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Adres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdresUpdateDto>>> GetAdresInfos()
        {
            var adresInfos = await _context.Adresler
                .Include(a => a.Firma)
                .Include(a => a.Ulke)
                .Include(a => a.Il)
                .Include(a => a.Ilce)
                .Select(a => new AdresUpdateDto
                {
                    AdresId = a.Id,
                    FirmaAdi = a.Firma != null ? a.Firma.FirmaAdi : "N/A",
                    UlkeAdi = a.Ulke != null ? a.Ulke.Ad : "N/A",
                    IlAdi = a.Il != null ? a.Il.Ad : "N/A",
                    IlceAdi = a.Ilce != null ? a.Ilce.Ad : "N/A",
                    Sokak = a.Sokak ?? "N/A",
                    PostaKodu = a.PostaKodu ?? "N/A",
                    IsActive = a.IsActive,
                    IsDefault = a.IsDefault,
                    CreatedBy = a.CreatedBy.HasValue ? a.CreatedBy.Value : Guid.Empty,
                    CreateDate = a.CreateDate,
                    UpdatedBy = null,
                    UpdateDate = a.UpdateDate,
                    VergiNumarasi = a.Firma != null ? a.Firma.VergiNumarasi : "N/A",
                    Telefon = a.Firma != null ? a.Firma.Telefon : "N/A",
                    FirmaEmail = a.Firma != null ? a.Firma.Email : "N/A",
                    WebSitesi = a.Firma != null ? a.Firma.WebSitesi : "N/A"
                })
                .ToListAsync();

            return Ok(adresInfos);
        }

        // GET: api/Adres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdresUpdateDto>> GetAdres(Guid id)
        {
            var adres = await _context.Adresler
                .Include(a => a.Firma)
                .Include(a => a.Ulke)
                .Include(a => a.Il)
                .Include(a => a.Ilce)
                .Where(a => a.Id == id)
                .Select(a => new AdresUpdateDto
                {
                    AdresId = a.Id,
                    FirmaAdi = a.Firma != null ? a.Firma.FirmaAdi : "N/A",
                    UlkeAdi = a.Ulke != null ? a.Ulke.Ad : "N/A",
                    IlAdi = a.Il != null ? a.Il.Ad : "N/A",
                    IlceAdi = a.Ilce != null ? a.Ilce.Ad : "N/A",
                    Sokak = a.Sokak ?? "N/A",
                    PostaKodu = a.PostaKodu ?? "N/A",
                    IsActive = a.IsActive,
                    IsDefault = a.IsDefault,
                    CreatedBy = a.CreatedBy.HasValue ? a.CreatedBy.Value : Guid.Empty,
                    CreateDate = a.CreateDate,
                    UpdatedBy = null,
                    UpdateDate = a.UpdateDate,
                    VergiNumarasi = a.Firma != null ? a.Firma.VergiNumarasi : "N/A",
                    Telefon = a.Firma != null ? a.Firma.Telefon : "N/A",
                    FirmaEmail = a.Firma != null ? a.Firma.Email : "N/A",
                    WebSitesi = a.Firma != null ? a.Firma.WebSitesi : "N/A"
                })
                .FirstOrDefaultAsync();

            if (adres == null)
            {
                return NotFound();
            }

            return Ok(adres);
        }

        // POST: api/Adres
        [HttpPost]
        
        public async Task<ActionResult<AdresUpdateDto>> PostAdresInfo([FromBody] AdresCreateDto adresCreateDto)
        {
            if (adresCreateDto == null)
            {
                return BadRequest("AdresCreateDto cannot be null.");
            }

            // Map the DTO to the entity
            var adres = new Adres
            {
                Id = Guid.NewGuid(), // Assuming Id is generated server-side
                Sokak = adresCreateDto.Sokak,
                PostaKodu = adresCreateDto.PostaKodu,
                IsActive = adresCreateDto.IsActive,
                IsDefault = adresCreateDto.IsDefault,
                CreatedBy = adresCreateDto.CreatedBy,
                CreateDate = DateTime.UtcNow,
                UpdatedBy = null,
                UpdateDate = null,
                FirmaId = adresCreateDto.FirmaId,
                UlkeId = adresCreateDto.UlkeId,
                IlId = adresCreateDto.IlId,
                IlceId = adresCreateDto.IlceId
            };

            _context.Adresler.Add(adres);
            await _context.SaveChangesAsync();

            // Map the entity to DTO
            var adresDto = new AdresUpdateDto
            {
                AdresId = adres.Id,
                FirmaAdi = adres.Firma != null ? adres.Firma.FirmaAdi : "N/A",
                UlkeAdi = adres.Ulke != null ? adres.Ulke.Ad : "N/A",
                IlAdi = adres.Il != null ? adres.Il.Ad : "N/A",
                IlceAdi = adres.Ilce != null ? adres.Ilce.Ad : "N/A",
                Sokak = adres.Sokak ?? "N/A",
                PostaKodu = adres.PostaKodu ?? "N/A",
                IsActive = adres.IsActive,
                IsDefault = adres.IsDefault,
                CreatedBy = adres.CreatedBy.HasValue ? adres.CreatedBy.Value : Guid.Empty,
                CreateDate = adres.CreateDate,
                UpdatedBy = null,
                UpdateDate = adres.UpdateDate,
                VergiNumarasi = adres.Firma != null ? adres.Firma.VergiNumarasi : "N/A",
                Telefon = adres.Firma != null ? adres.Firma.Telefon : "N/A",
                FirmaEmail = adres.Firma != null ? adres.Firma.Email : "N/A",
                WebSitesi = adres.Firma != null ? adres.Firma.WebSitesi : "N/A"
            };

            return CreatedAtAction(nameof(GetAdresInfos), new { id = adres.Id }, adresDto);
        }



        // PUT: api/Adres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdres(Guid id, [FromBody] AdresUpdateDto adresDto)
        {
            if (id != adresDto.AdresId)
            {
                return BadRequest("ID mismatch between route parameter and payload.");
            }

            var adres = await _context.Adresler
                .Include(a => a.Ulke)
                .Include(a => a.Il)
                .Include(a => a.Ilce)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (adres == null)
            {
                return NotFound();
            }

            // Update properties
            adres.Sokak = adresDto.Sokak;
            adres.PostaKodu = adresDto.PostaKodu;
            adres.IsActive = adresDto.IsActive;
            adres.IsDefault = adresDto.IsDefault;

            // Update related entities if their IDs are provided
            if (adresDto.UlkeId.HasValue)
            {
                var ulke = await _context.Ulkeler.FindAsync(adresDto.UlkeId.Value);
                if (ulke != null)
                {
                    adres.Ulke = ulke;
                }
            }

            if (adresDto.IlId.HasValue)
            {
                var il = await _context.Iller.FindAsync(adresDto.IlId.Value);
                if (il != null)
                {
                    adres.Il = il;
                }
            }

            if (adresDto.IlceId.HasValue)
            {
                var ilce = await _context.Ilceler.FindAsync(adresDto.IlceId.Value);
                if (ilce != null)
                {
                    adres.Ilce = ilce;
                }
            }

            adres.UpdateDate = DateTime.UtcNow; // Set the current date and time

            // Save changes
            try
            {
                _context.Entry(adres).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresExists(id))
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


        // DELETE: api/Adres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdres(Guid id)
        {
            var adres = await _context.Adresler.FindAsync(id);
            if (adres == null)
            {
                return NotFound();
            }

            _context.Adresler.Remove(adres);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdresExists(Guid id)
        {
            return _context.Adresler.Any(e => e.Id == id);
        }
    }
}
