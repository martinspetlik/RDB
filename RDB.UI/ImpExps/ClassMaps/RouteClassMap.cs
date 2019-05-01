using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class RouteClassMap : ClassMap<Route>
    {
        #region Constructors

        public RouteClassMap()
        {
            Map(m => m.Number).Index(0);
            Map(m => m.DepartureName).Index(1);
            Map(m => m.ArrivalName).Index(2);
        }

        #endregion
    }
}
