using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class ModelClassMap : ClassMap<Model>
    {
        #region Constructors

        public ModelClassMap()
        {
            Map(m => m.Name).Index(0);
        }

        #endregion
    }
}
