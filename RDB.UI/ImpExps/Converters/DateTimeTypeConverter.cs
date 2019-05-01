using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;

namespace RDB.UI.ImpExps.Converters
{
    public class DateTimeTypeConverter<T> : DefaultTypeConverter
    {
        #region Public methods

        public override String ConvertToString(Object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value != null && value is DateTime)
            {
                var unixTime = ((DateTime)value).ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                
                return (unixTime.TotalMilliseconds / 1000).ToString("0.0####", new CultureInfo("en-US"));
            }

            return "\\N";
        }

        #endregion
    }
}
