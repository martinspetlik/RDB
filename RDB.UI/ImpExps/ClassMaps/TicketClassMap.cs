using CsvHelper.Configuration;
using RDB.Data.Models;
using RDB.UI.ImpExps.Converters;
using System;

namespace RDB.UI.ImpExps.ClassMaps
{
    public sealed class TicketClassMap : ClassMap<Ticket>
    {
        #region Constructors

        public TicketClassMap()
        {            
            Map(m => m.RouteNumber).Index(0);
            Map(m => m.ClientEmail).Index(1).TypeConverter<NullTypeConverter<String>>();
            Map(m => m.DriveTime).Index(2).TypeConverter<DateTimeTypeConverter<DateTime>>();
            Map(m => m.Number).Index(3);
        }

        #endregion
    }
}
