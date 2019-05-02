using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using static RDB.Data.Extensions.DateTimeExtension;

namespace RDB.UI.ImpExps.Converters
{
    public class DateTimeTypeConverter<T> : DefaultTypeConverter
    {
        #region Public methods

        public override String ConvertToString(Object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value != null && value is DateTime)
                return ((DateTime)value).ToTimestamp();

            return "\\N";
        }

        #endregion
    }
}
