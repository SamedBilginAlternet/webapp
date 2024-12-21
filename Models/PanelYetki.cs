using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("PanelYetki")]
    public class PanelYetki
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? AdminId { get; set; }

        [Required]
        [StringLength(100)]
        public required string YetkiAdi { get; set; }

        public Guid? CreatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
