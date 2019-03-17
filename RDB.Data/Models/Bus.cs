using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Autobus")]
    public class Bus
    {
        #region Properties

        [Key]
        [Column("spz", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Plate { get; set; }

        [Required]
        [Column("znacka")]
        [ForeignKey("Model")]
        public String ModelName { get; set; }

        public virtual Model Model { get; set; }

        #endregion
    }
}
