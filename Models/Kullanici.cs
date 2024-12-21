using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("Kullanici")]
    public class Kullanici
    {    
        [Key]
        public Guid Id { get; set; }

        public Guid? FirmaId { get; set; }

        public Guid? UnvanId { get; set; }

        [StringLength(100)]
        public  string KullaniciAdi { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(100)]
        public  string Sifre { get; set; }

        

        public Guid? CreatedBy { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}
