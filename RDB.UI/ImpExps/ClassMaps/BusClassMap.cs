using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class BusClassMap : ClassMap<Bus>
    {
        #region Constructors

        public BusClassMap()
        {
            Map(m => m.Plate).Index(0);
            Map(m => m.ModelName).Index(1);
        }

        #endregion
    }
}
