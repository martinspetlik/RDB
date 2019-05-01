using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class StationClassMap : ClassMap<Station>
    {
        #region Constructors

        public StationClassMap()
        {
            Map(m => m.Name).Index(0);
            Map(m => m.RouteNumber).Index(1);
        }

        #endregion
    }
}
