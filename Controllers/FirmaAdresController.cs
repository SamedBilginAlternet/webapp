using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication5.Dtos;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmaAdresController : ControllerBase
    {
        private readonly DataAccess _dataAccess;

        public FirmaAdresController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFirmaAndAdres([FromBody] CreateFirmaAndAdresRequest request)
        {

            request.CreatedBy = new Guid("01BC310A-6E36-459A-8FDD-62C41009EC39");
            if (Guid.Empty == request.CreatedBy)
            {
                return BadRequest("Bad CreatedBy");
            }
            try
            {
                await _dataAccess.InsertFirmaAndAdresAsync(
                    request.FirmaAdi,
                    request.VergiDairesiAd, // Updated property name
                    request.VergiNumarasi,
                    request.Telefon,
                    request.Email,
                    request.WebSitesi,
                    request.CreatedBy,
                    request.UlkeId,
                    request.IlId,
                    request.IlceId,
                    request.Sokak,
                    request.PostaKodu,
                    request.IsActive,
                    request.IsDefault
                );
                return Ok(new { message = "Firma and Address created successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, new { message = ex.ToString() });
            }
        }
    }

    public class CreateFirmaAndAdresRequest
    {
        public string FirmaAdi { get; set; }
        public string VergiDairesiAd { get; set; } // Updated property name
        public string VergiNumarasi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string WebSitesi { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UlkeId { get; set; }
        public Guid? IlId { get; set; }
        public Guid? IlceId { get; set; }
        public string Sokak { get; set; }
        public string PostaKodu { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
