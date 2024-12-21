using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;


[Table("Firma")]
public class Firma
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string FirmaAdi { get; set; }

    [Required]
    public Guid VergiDairesiId { get; set; }

    [Required]
    public string VergiNumarasi { get; set; }

    public string? Telefon { get; set; }

    public string? Email { get; set; }

    public string? WebSitesi { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }


    public VergiDairesi VergiDairesi { get; set; }  // Navigation property
    public virtual ICollection<Adres>? Adresler { get; set; }
}
