using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class ClientClassMap : ClassMap<Client>
    {
        #region Constructors

        public ClientClassMap()
        {
            Map(m => m.Email);
            Map(m => m.Firstname);
            Map(m => m.Surname); 
        }

        #endregion
    }
}
