using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Ridic")]
    public class Driver
    {
        #region Properties

        [Key]
        [Column("cislo_rp", TypeName = "nvarchar")]
        [StringLength(50)]
        public String LicenseNumber { get; set; }

        [Required]
        [Column("jmeno", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Firstname { get; set; }

        [Required]
        [Column("prijmeni", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Surname { get; set; }

        public virtual ICollection<Contact> Contacts { get; } = new HashSet<Contact>();

        #endregion
    }
}
