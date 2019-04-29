using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class RouteClassMap : ClassMap<Route>
    {
        #region Constructors

        public RouteClassMap()
        {
            Map(m => m.Number);
            Map(m => m.DepartureName);
            Map(m => m.ArrivalName);
        }

        #endregion
    }
}
