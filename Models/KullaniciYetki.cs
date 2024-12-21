using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("KullaniciYetki")]
    public class KullaniciYetki
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? KullaniciId { get; set; }

        [Required]
        [StringLength(100)]
        public required string YetkiAdi { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public Guid? UpdatedBy { get; set; }

       
        public DateTime? UpdateDate { get; set; }
    }
}
