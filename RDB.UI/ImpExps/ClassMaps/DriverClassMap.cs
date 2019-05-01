using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class DriverClassMap : ClassMap<Driver>
    {
        #region Constructors

        public DriverClassMap()
        {
            Map(m => m.LicenseNumber).Index(0);
            Map(m => m.Firstname).Index(1);
            Map(m => m.Surname).Index(2);
        }

        #endregion
    }
}
