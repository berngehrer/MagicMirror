using System;
using System.Globalization;

namespace MagicMirror
{
    public static class DateHelper
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime TimeSpanToTodaysTime(TimeSpan ts)
        {
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, ts.Hours, ts.Minutes, ts.Seconds);
        }

        public static DateTime ParseExactDateTime(string value)
        {
            DateTime dt = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(value))
            {
                Func<string, bool> parse = (format) => DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                if (!parse("yyyyMMddTHHmmssZ"))
                    if (!parse("yyyyMMddTHHmmss"))
                        parse("yyyyMMdd");
            }
            return dt;
        }
    }
}
