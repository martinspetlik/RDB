using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class LocationClassMap : ClassMap<Location>
    {
        #region Constructors

        public LocationClassMap()
        {
            Map(m => m.Name).Index(0);
        }

        #endregion
    }
}
