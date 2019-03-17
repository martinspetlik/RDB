using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Mezizastavka")]
    public class Station
    {
        #region Properties

        [Key]
        [Column("nazev", Order = 0)]
        [ForeignKey("Location")]
        public String Name { get; set; }

        public virtual Location Location { get; set; }

        [Key]
        [Column("linka", Order = 1)]
        [ForeignKey("Route")]
        public String RouteNumber { get; set; }

        public virtual Route Route { get; set; }

        #endregion
    }
}
