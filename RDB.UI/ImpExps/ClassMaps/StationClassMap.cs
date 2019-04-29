using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class StationClassMap : ClassMap<Station>
    {
        #region Constructors

        public StationClassMap()
        {
            Map(m => m.Name);
            Map(m => m.RouteNumber);
        }

        #endregion
    }
}
