using System;
using System.Globalization;

namespace RDB.Data.Extensions
{
    public static class DateTimeExtension
    {
        #region Public methods

        public static String ToTimestamp (this DateTime date)
        {
            TimeSpan unixTime = date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (unixTime.TotalMilliseconds / 1000).ToString("0.0####", new CultureInfo("en-US"));
        }

        #endregion
    }
}
