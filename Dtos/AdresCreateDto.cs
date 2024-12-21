using System;

namespace WebApplication5.Models
{
    public class AdresCreateDto
    {
        public string Sokak { get; set; }
        public string PostaKodu { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public Guid CreatedBy { get; set; } // The ID of the user creating the address
        public Guid FirmaId { get; set; } // Foreign key reference to the Firma entity
        public Guid UlkeId { get; set; } // Foreign key reference to the Ulke entity
        public Guid IlId { get; set; } // Foreign key reference to the Il entity
        public Guid IlceId { get; set; } // Foreign key reference to the Ilce entity


    }
}
