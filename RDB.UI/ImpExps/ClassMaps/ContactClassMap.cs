using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class ContactClassMap : ClassMap<Contact>
    {
        #region Constructors

        public ContactClassMap()
        {
            Map(m => m.Value).Index(0);
            Map(m => m.Type).Index(1);
            Map(m => m.DriverLicenseNumber).Index(2);
        }

        #endregion
    }
}
