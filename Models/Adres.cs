using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("Adres")]
    public class Adres
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? FirmaId { get; set; }
        public Guid? IlId { get; set; }
        public Guid? IlceId { get; set; }
        public Guid? UlkeId { get; set; }

        public string? Sokak { get; set; }
        public string? PostaKodu { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        public Firma Firma { get; set; } 

       
        public virtual Ulke? Ulke { get; set; }
        public virtual Il? Il { get; set; }
        public virtual Ilce? Ilce { get; set; }
    }
}
