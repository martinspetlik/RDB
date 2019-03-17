using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Klient")]
    public class Client
    {
        #region Properties

        [Key]
        [Column("email", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Email { get; set; }

        [Required]
        [Column("jmeno", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Firstname { get; set; }

        [Required]
        [Column("prijmeni", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Surname { get; set; }

        #endregion
    }
}
