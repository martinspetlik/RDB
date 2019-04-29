using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class DriverClassMap : ClassMap<Driver>
    {
        #region Constructors

        public DriverClassMap()
        {
            Map(m => m.LicenseNumber);
            Map(m => m.Firstname);
            Map(m => m.Surname);
        }

        #endregion
    }
}
