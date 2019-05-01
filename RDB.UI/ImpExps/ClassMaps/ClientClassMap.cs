using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class ClientClassMap : ClassMap<Client>
    {
        #region Constructors

        public ClientClassMap()
        {
            Map(m => m.Email).Index(0);
            Map(m => m.Firstname).Index(1);
            Map(m => m.Surname).Index(2); 
        }

        #endregion
    }
}
