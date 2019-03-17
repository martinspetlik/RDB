using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.Data.Models
{
    [Table("TypKontaktu")]
    public class ContactType
    {
        #region Properties

        [Key]
        [Column("typ", TypeName = "nvarchar")]
        [StringLength(50)]
        public String Type { get; set; }

        public virtual ICollection<Contact> Contacts { get; } = new HashSet<Contact>();

        #endregion
    }
}
