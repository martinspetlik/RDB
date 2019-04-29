using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class ContactClassMap : ClassMap<Contact>
    {
        #region Constructors

        public ContactClassMap()
        {
            Map(m => m.Value);
            Map(m => m.Type);
            Map(m => m.DriverLicenseNumber);
        }

        #endregion
    }
}
