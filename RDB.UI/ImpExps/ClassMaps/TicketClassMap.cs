using CsvHelper.Configuration;
using RDB.Data.Models;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class TicketClassMap : ClassMap<Ticket>
    {
        #region Constructors

        public TicketClassMap()
        {
            Map(m => m.DriveTime);
            Map(m => m.RouteNumber);
            Map(m => m.Number);
            Map(m => m.ClientEmail);
        }

        #endregion
    }
}
