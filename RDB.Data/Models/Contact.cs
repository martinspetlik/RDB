using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Kontakt")]
    public class Contact
    {
        #region Properties

        [Key]
        [Column("hodnota", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Value { get; set; }

        [Required]
        [Column("typ")]
        [ForeignKey("ContactType")]
        public String Type { get; set; }

        public virtual ContactType ContactType { get; set; }

        [Required]
        [Column("cislo_rp")]
        [ForeignKey("Driver")]
        public String DriverLicenseNumber { get; set; }

        public virtual Driver Driver { get; set; }

        #endregion
    }
}
