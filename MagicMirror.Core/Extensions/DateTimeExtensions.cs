using System;

namespace MagicMirror
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDayStart(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        public static DateTime ToDayEnd(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }

        public static bool IsToday(this DateTime dt)
        {
            return IsSameDay(dt, DateTime.Now);
        }

        public static bool IsSameDay(this DateTime dt, DateTime day)
        {
            return dt >= day.ToDayStart() && dt <= day.ToDayEnd();
        }
    }
}
