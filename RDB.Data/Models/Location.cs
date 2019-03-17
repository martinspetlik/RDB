using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Lokalita")]
    public class Location
    {
        #region Properties

        [Key]
        [Column("nazev", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Name { get; set; }

        #endregion
    }
}
