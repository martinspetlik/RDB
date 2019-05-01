using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Jizdenka")]
    public class Ticket
    {
        #region Properties

        [Required]
        [ForeignKey("Drive")]
        [Column("linka", Order = 1)]
        public String RouteNumber { get; set; }

        [Column("email")]
        [ForeignKey("Client")]
        public String ClientEmail { get; set; }

        public virtual Client Client { get; set; }

        [Required]
        [ForeignKey("Drive")]
        [Column("cas", Order = 0, TypeName = "timestamp")]
        public DateTime DriveTime { get; set; }

        public virtual Drive Drive { get; set; }

        [Key]
        [Column("cislo", TypeName = "bigint")]
        public Int64 Number { get; set; }

        #endregion
    }
}
