using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("Il")]
    public class Il
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? UlkeId { get; set; }

        [Required]
        public required string Ad { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Adres>? Adresler { get; set; }

    }
}
