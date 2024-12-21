using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("Unvan")]
    public class Unvan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UnvanAdi { get; set; }

        public string Aciklama { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
