using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Jizda")]
    public class Drive
    {
        #region Properties

        [Key]
        [Column("cas", Order = 0, TypeName = "timestamp")]
        public DateTime Time { get; set; }

        [Required]
        [Column("spz")]
        [ForeignKey("Bus")]
        public String BusPlate { get; set; }

        public virtual Bus Bus { get; set; }

        [Required]
        [Column("cislo_rp")]
        [ForeignKey("Driver")]
        public String DriveLicenseNumber { get; set; }

        public virtual Driver Driver { get; set; }

        [Key]
        [Column("linka", Order = 1)]
        [ForeignKey("Route")]
        public String RouteNumber { get; set; }

        public virtual Route Route { get; set; }

        public String Pole { get; set; }

        #endregion
    }
}
