using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Trasy")]
    public class Route
    {
        #region Properties

        [Key]
        [Column("linka", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Number { get; set; }

        [Required]
        [Column("odkud")]
        [ForeignKey("DepartureLocation")]
        public String DepartureName { get; set; }

        public virtual Location DepartureLocation { get; set; }

        [Required]
        [Column("kam")]
        [ForeignKey("ArrivalLocation")]
        public String ArrivalName { get; set; }

        public virtual Location ArrivalLocation { get; set; }

        #endregion
    }
}
