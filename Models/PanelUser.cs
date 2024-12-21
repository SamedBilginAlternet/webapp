using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


    [Table("PanelUser")]
    public class PanelUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public required string Username { get; set; }
        [Required]
        [StringLength(255)]
        public required string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? UpdatedBy { get; set; }
       public DateTime? UpdateDate { get; set; }
    }

