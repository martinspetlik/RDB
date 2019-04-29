using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class DriveClassMap : ClassMap<Drive>
    {
        #region Constructors

        public DriveClassMap()
        {
            Map(m => m.RouteNumber);
            Map(m => m.BusPlate);
            Map(m => m.DriveLicenseNumber);
            Map(m => m.Time);
        }

        #endregion
    }
}
