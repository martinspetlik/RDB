using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class BusClassMap : ClassMap<Bus>
    {
        #region Constructors

        public BusClassMap()
        {
            Map(m => m.ModelName);
            Map(m => m.Plate);
        }

        #endregion
    }
}
