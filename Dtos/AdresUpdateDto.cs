using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Dtos
{
    public class AdresUpdateDto
    {
        public Guid AdresId { get; set; }
        public string? Sokak { get; set; }
        public string? PostaKodu { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? FirmaAdi { get; set; }
        public string? VergiNumarasi { get; set; }
        public string? Telefon { get; set; }
        public string? FirmaEmail { get; set; }
        public string? WebSitesi { get; set; }

        // Added IDs for related entities
        public Guid? UlkeId { get; set; }
        public Guid? IlId { get; set; }
        public Guid? IlceId { get; set; }

        // Optional: If you want to include names for display purposes
        public string? UlkeAdi { get; set; }
        public string? IlAdi { get; set; }
        public string? IlceAdi { get; set; }
    }
}
