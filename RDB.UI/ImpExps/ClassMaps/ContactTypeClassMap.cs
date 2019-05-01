using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class ContactTypeClassMap : ClassMap<ContactType>
    {
        #region Constructors

        public ContactTypeClassMap()
        {
            Map(m => m.Type).Index(0);
        }

        #endregion
    }
}
