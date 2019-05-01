using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace RDB.UI.ImpExps.Converters
{
    public class NullTypeConverter<T> : DefaultTypeConverter
    {
        #region Public methods

        public override String ConvertToString(Object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
                return "\\N";

            ITypeConverter converter = row.Configuration.TypeConverterCache.GetConverter<T>();

            return converter.ConvertToString(value, row, memberMapData);
        }

        #endregion
    }
}
