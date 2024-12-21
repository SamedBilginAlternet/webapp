using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication5.Models
{
    [Table("Package")]
    public class Package
    {
        [Key]
        public Guid PackageId { get; set; }

        [Required]
        public Guid FirmaId { get; set; }
        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set; }

        public bool? NormalPackage { get; set; }  // Nullable boolean
        public bool? PremiumPackage { get; set; } // Nullable boolean
        public bool? EconomicPackage { get; set; } // Nullable boolean
    }
}
