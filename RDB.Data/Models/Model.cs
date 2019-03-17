using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("Znacka")]
    public class Model
    {
        #region Properties

        [Key]
        [Column("znacka", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Name { get; set; }

        public virtual ICollection<Bus> Buses { get; } = new HashSet<Bus>();

        #endregion
    }
}
