using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("VergiDairesi")]
    public class VergiDairesi
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public required string Ad { get; set; }

        public Guid? IlId { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
